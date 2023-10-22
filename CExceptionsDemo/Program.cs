using System.Diagnostics;

namespace CExceptionsDemo
{
    internal class Program
    {
        static void MyBadMethod()
        {
            Console.WriteLine("MyBadMethod starts.");
            int a = 0; int b = 10 / a;
            Console.WriteLine("MyBadMethod ends.");
        }
        static void MyAwesomeMethod()
        {
            Console.WriteLine("MyAwesomeMethod starts.");
            MyBadMethod();
            Console.WriteLine("MyAwesomeMethod ends.");
        }

        static void DoSomething()
        {
            Console.WriteLine("DoSomething starts.");
            MyAwesomeMethod();
            Console.WriteLine("DoSomething end.");
        }
        static void Main(string[] args)
        {
            DoSomething();
        }
    }
}