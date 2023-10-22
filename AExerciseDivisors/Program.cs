using System.ComponentModel;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AExerciseDivisors
{
    internal class Program
    {
        //For sequential execution
        static readonly (int Start, int End) Range = (10, 5_000_000);

        //For the parallel one.
        static (int Number, int DivisorsCount)[] Results;
        static (int Start, int End)[] Ranges;

        static int CountDivisors(int num)
        {
            int boundary = Convert.ToInt32(Math.Sqrt(num));
            int result = 0;
            for (int i = 1; i <= boundary; i++)
            {
                if (num % i == 0)
                {
                    result++;
                } 
            }
            return result;
        }

        static void FindMaxDivisorsSequential()
        {
            int number = 0;
            int max = 0;
            for (int i = Range.Start; i < Range.End; i++)
            {
                int currentMax = CountDivisors(i);
                if (currentMax > max)
                {
                    max = currentMax;
                    number = i;
                }
            }
            Console.WriteLine($"{number} has most divisors: {max}");
        }

        static void FindMaxDivisorsForRange(object indexObj)
        {
            int rangeIndex = (int)indexObj;
            int max = 0;
            int number = 0;
            for (
                int i = Ranges[rangeIndex].Start; 
                i < Ranges[rangeIndex].End; 
                i++)
            {
                int currentMax = CountDivisors(i);
                if (currentMax > max)
                {
                    number = i;
                    max = currentMax;
                }
            }
            Results[rangeIndex].Number = number;
            Results[rangeIndex].DivisorsCount = max;
        }

        static void FindMaxDivisorsParallel (int numThreads)
        {
            Results = new (int Number, int DivisorsCount)[numThreads];
            Ranges = new (int Start, int End)[numThreads];
            Thread[] workers = new Thread[numThreads];

            int rangeSize = (Range.End - Range.Start) / numThreads;
            for (int i = 0; i < numThreads; i++)
            {
                Ranges[i] = (
                    Start: i * rangeSize,
                    End: (i + 1) * rangeSize
                    );
                workers[i] = new Thread(FindMaxDivisorsForRange);
                workers[i].Start(i);
            }
            foreach (var w in workers) w.Join();
            var max = Results.MaxBy(r => r.DivisorsCount);
            Console.WriteLine($"{max.Number} has most divisors: {max.DivisorsCount}");
        }

        static void Main(string[] args)
        {
            Stopwatch sw = Stopwatch.StartNew();

            //Console.WriteLine("Sequential:");
            //FindMaxDivisorsSequential();
            //sw.Stop();
            //Console.WriteLine($"Sequential execution time: {sw.ElapsedMilliseconds} ms.");
            //Console.WriteLine();

            Console.WriteLine("Parallel:");
            //Console.Write("How many threads to run? ");
            //int numThreads = Int32.Parse(Console.ReadLine());

            int numThreads = 2;
            for (int i = 0; i < 10; i++)
            {
                sw.Restart();
                FindMaxDivisorsParallel(numThreads);
                sw.Stop();
                Console.WriteLine($"Parallel execution time for {numThreads} threads: {sw.ElapsedMilliseconds} ms.");

                numThreads += numThreads;
            }

            Console.WriteLine();

            Console.WriteLine("Press ENTER to quit.");
            Console.ReadLine();
        }
    }
}