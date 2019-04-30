using System;
using Demo.Demo;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            #region DemoService
            DemoService demoService = new DemoService();
            demoService.Start();
            #endregion

            Console.ReadKey();
        }
    }
}
