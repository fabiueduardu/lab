using System;
using System.Configuration;
using System.Diagnostics;
using System.Linq;

namespace JSpank.Test.Helpers.MainApp
{
    public class Program
    {
        static int ProcessOpen
        {

            get
            {
                var value = ConfigurationManager.AppSettings["ProcessOpen"] as string;
                if (string.IsNullOrEmpty(value))
                    return 1;

                return int.Parse(value);
            }
        }

        static void Main(string[] args)
        {
            if (args == null || !args.Any(a => a.Equals("off")))
                MainMenuMulti();

            ConsoleKeyInfo info = MainMenu();
            do
            {
                switch (info.Key)
                {
                    case ConsoleKey.NumPad1:
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad2:
                    case ConsoleKey.D2:
                        Console.WriteLine("\n#### executing option {0}\n", info.Key);
                        break;
                    default:
                        Console.WriteLine(" \n# invalid {0}\n", info.Key);
                        break;
                }

                if (info.Key.Equals(ConsoleKey.D0) || info.Key.Equals(ConsoleKey.NumPad0))
                    break;

                info = MainMenu();
            } while (true);
        }

        static void MainMenuMulti()
        {
            Console.WriteLine("\t # 1 instance?");
            var key = Console.ReadLine();
            var instances = 1;
            if (!int.TryParse(key, out instances))
                instances = 1;

            for (var i = 0; i < instances - 1; i++)
                Process.Start(Process.GetCurrentProcess().MainModule.FileName.ToLower().Replace(".vshost", string.Empty), "off");
        }

        static ConsoleKeyInfo MainMenu()
        {

            Console.WriteLine("\tOption:");
            Console.WriteLine("\t\t1.lorem ipsum");
            Console.WriteLine("\t\t2.lorem ipsum");
            Console.WriteLine("\t\t0.exit");

            return Console.ReadKey(true);
        }
    }
}
