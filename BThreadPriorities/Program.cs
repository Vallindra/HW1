namespace BThreadPriorities
{
    internal class Program
    {
        static void ThreadWorker()
        {
            var startTime = DateTime.Now;
            while (DateTime.Now.Subtract(startTime).TotalSeconds < 20)
            {
                ;
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine($"Running {Environment.ProcessorCount} " +
                $"low-priority threads.  Observe TaskManager.");
            Thread[] workers = new Thread[Environment.ProcessorCount];
            for (int i = 0; i < workers.Length; i++)
            {
                workers[i] = new Thread(ThreadWorker);
                workers[i].Priority = ThreadPriority.Lowest;
                workers[i].Start();
            }
            foreach (var w in workers) w.Join();
            Console.WriteLine("Press ENTER to quit.");
            Console.ReadLine();
        }
    }
}