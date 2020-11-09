using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceDice
{
    class Program
    {
        public static int quan;
        public static int[] eyes;
        public static Random rnd = new Random();

        static void Main()
        {
        Start:
            Console.WriteLine("Welcome to NiceDice! Follow the instructions to get rolling!");
            Console.WriteLine(@"                  _____________                            ");
            Console.WriteLine(@"                 /            /\                           ");
            Console.WriteLine(@"                /  O     O   /  \                          ");
            Console.WriteLine(@"               /     O      /  O \________________         ");
            Console.WriteLine(@"              /  O     O   /      \           |   |        ");
            Console.WriteLine(@"             /____________/        \      O   |  )|        ");
            Console.WriteLine(@"             \            \        /          |   |        ");
            Console.WriteLine(@"              \   O        \      /O      O   |   |        ");
            Console.WriteLine(@"               \     O      \  O /            |   |        ");
            Console.WriteLine(@"                \        O   \  /  O      O   | ) |        ");
            Console.WriteLine(@"                 \______ _____\/|_____________|___|        ");
            Console.WriteLine(@"                                                           ");
            Console.WriteLine("How many dice do you need?");

            quan = Convert.ToInt32(Console.ReadLine());
            eyes = new int[quan];

            if (quan <= 3)
            {
                AllDifferent();
            }

            if (quan > 3)
            {
                Console.WriteLine("Do you want all dice to have the same number of eyes? Y/N");
                ConsoleKeyInfo similarDice = Console.ReadKey(true);

                if (similarDice.Key == ConsoleKey.Escape)
                {
                    Exit();
                }

                if (similarDice.Key == ConsoleKey.R)
                {
                    Console.Clear();
                    goto Start;
                }

                if (similarDice.Key == ConsoleKey.Y)
                {
                    AllSame();
                }

                if (similarDice.Key == ConsoleKey.N)
                {
                    Console.WriteLine("-----------------------------------------------------------");
                    Console.WriteLine("Do you want all dice to be different? Y/N");
                    ConsoleKeyInfo noSimilar = Console.ReadKey(true);

                    if (noSimilar.Key == ConsoleKey.N)
                    {
                        SomeSame();
                    }

                    if (noSimilar.Key == ConsoleKey.Y)
                    {
                        AllDifferent();
                    }

                    else
                    {
                        Console.Clear();
                        goto Start;
                    }
                }

                else
                {
                    Console.Clear();
                    goto Start;
                }
            }
        }

        static void AllSame()
        {
            Console.Clear();
            Console.Write("Select the number of eyes for your Dice: ");
            int readEyes = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < quan; i++)
            {
                eyes[i] = readEyes;
            }

            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Press 'Space' to roll your dice!");
            Console.WriteLine("You can always press - 'R' to Start over, - 'T' to clear the display or - 'Esc' to exit.");
            Console.WriteLine();

            do
            {
                ConsoleKeyInfo input = Console.ReadKey(true);
                Console.WriteLine("------");
                
                if (input.Key == ConsoleKey.Escape)
                {
                    Exit();
                }

                if (input.Key == ConsoleKey.R)
                {
                    Console.Clear();
                    Main();
                }

                if (input.Key == ConsoleKey.T)
                {
                    Console.Clear();
                    Console.WriteLine("Press 'R' to start over, 'T' to clear console or 'Esc' to exit.");
                }

                for (int i = 0; i < quan; i++)
                {
                    Console.WriteLine("D" + eyes[i] + ": " + rnd.Next(1, eyes[i]+1));
                }

                Console.WriteLine();

            } while (1 == 1);
        }

        static void SomeSame()
        {
            Console.Clear();

            int total = 0;
            int readEyeQuan = 0;
            int cycle = 1;
            int readEyes;

            do
            {
                Console.WriteLine("Select the " + cycle + ". desired number of eyes.");
                readEyes = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("How many dice should share this number of eyes?");
                readEyeQuan = Convert.ToInt32(Console.ReadLine());
                
                for (int i = total; i < quan; i++)
                {
                    eyes[i] = readEyes;
                }

                total += readEyeQuan;
                Console.WriteLine();
                cycle++;

                if (quan - total > 0)
                {
                    Console.WriteLine("you have " + (quan - total) + " remaining");
                }

                Console.WriteLine();
                Console.WriteLine("-----------------------------------------------");

            } while (total < quan);

            do
            {
                Console.WriteLine("Press 'space to roll your dice - 'R' to start over - 'T' to clear display - 'Esc' to exit");
                ConsoleKeyInfo input = Console.ReadKey(true);
                Console.WriteLine("------");

                if (input.Key == ConsoleKey.Escape)
                {
                    Exit();
                }

                if (input.Key == ConsoleKey.R)
                {
                    Console.Clear();
                    Main();
                }

                if (input.Key == ConsoleKey.T)
                {
                    Console.Clear();
                }

                else
                {
                    for (int i = 0; i < quan; i++)
                    {
                        Console.Write("D" + eyes[i] + ": ");
                        Console.WriteLine(rnd.Next(1, eyes[i] + 1));
                    }
                }
            } while (1 == 1);
        }

        static void AllDifferent()
        {
            Console.Clear();
            Console.WriteLine("Select number of eyes for each individual die!");

            for (int i = 0; i < quan; i++)
            {
                Console.Write((i + 1) + ". Die: ");
                eyes[i] = Convert.ToInt32(Console.ReadLine());
            }

            Console.WriteLine();
            Console.WriteLine("Your dice are ready! Press 'Space' to roll your dice.");
            Console.WriteLine("you can always press - 'R' to start over - 'T' to clear the console - or 'Esc' to exit.");
            Console.WriteLine("---------------------------------------------------------------------------------------");

            do
            {

                ConsoleKeyInfo input = Console.ReadKey(true);
                Console.WriteLine("------");

                if (input.Key == ConsoleKey.Escape)
                {
                    Exit();
                }

                if (input.Key == ConsoleKey.R)
                {
                    Console.Clear();
                    Main();
                }

                if (input.Key == ConsoleKey.T)
                {
                    Console.Clear();
                    Console.WriteLine("Press 'Space' to roll your dice - 'R' to start over - 'T' to clear the console - or 'Esc' to exit.");
                }

                else
                {
                    for (int i = 0; i < quan; i++)
                    {
                        Console.Write("D" + eyes[i] + ": ");
                        Console.WriteLine(rnd.Next(1, eyes[i]+1));
                    }
                }

                Console.WriteLine();

            } while (1 == 1);
        }

        static void Exit()
        {
            System.Environment.Exit(0);
        }
    }
}
