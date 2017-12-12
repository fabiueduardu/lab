using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("ConsoleApp: {0:dd/MM HH:mm:ss}", DateTime.Now);
            new HelloWorldNetStandard.HelloWorldService().Hello("testProgram");
            Console.ReadKey();
        }
    }
}
