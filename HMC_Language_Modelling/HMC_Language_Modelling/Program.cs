using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMC_Language_Modelling.src;

namespace HMC_Language_Modelling
{
    class Program
    {
        private static MarkovChain HMC;
        static void Main(string[] args)
        {
             HMC = new MarkovChain();
            
            //start loading data
            Console.WriteLine("Start loading test file...");
            if (!System.IO.File.Exists("heiseticker-text.txt"))
            {
                Console.WriteLine("Could not find file: heiseticker-text.txt");
            }
            else
            {
                try
                {
                    IEnumerable<string> lines = System.IO.File.ReadLines("heiseticker-text.txt");
                    IEnumerator<string> lineItr = lines.GetEnumerator();
                    if (lineItr.MoveNext())
                    {
                        string current = lineItr.Current;
                        while (lineItr.MoveNext())
                        {
                            HMC.increaseMarkovStatePropability(current, lineItr.Current);
                            current = lineItr.Current;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            Console.WriteLine("Finished...");
            string command = "";
            while (command != "q" && command != "quit")
            {
                command = Console.ReadLine();
                string[] arguments = command.Split(' ');
                if (arguments.Length > 0)
                {
                    switch (arguments[0].ToLower())
                    {
                        case "p":
                        case "prop":
                        case "propabillity":
                            handlePropCommand(arguments);
                            break;
                        case "g":
                        case "generate":
                            handleGenerateCommand(arguments);
                            break;
                        case "h":
                        case "help":
                            handleHelpCommand();
                            break;
                        default:
                            Console.WriteLine("Unknown command. Try help.");
                            break;
                    }
                }
            }
        }

        private static void handleHelpCommand()
        {
            Console.WriteLine("Use q to quit the application.");
            Console.WriteLine("Use p word word to get the propabillity between 2 words.");
            Console.WriteLine("Use g int to generate a sentence with a given length.");
        }

        private static void handleGenerateCommand(string[] arguments)
        {
            if (arguments.Length == 3)
            {
                int count = 0;
                if (int.TryParse(arguments[1], out count))
                {
                    StringBuilder sb = new StringBuilder();
                    string current = arguments[2];
                    for (int i = 0; i < count; i++)
                    {
                        sb.Append(current + " ");
                        current = (string)HMC.perfomStep(current);
                    }
                    Console.WriteLine(sb.ToString());
                }
                else
                {
                    Console.WriteLine("Could not convert parameter 1 to Integer. Required g int word");
                }
            }
            else
                Console.WriteLine("Wrong argument count. Required g int word");
        }

        private static void handlePropCommand(string[] arguments)
        {
            if(arguments.Length == 3)
            {
                Console.WriteLine(String.Format("Propabillity from \"{0}\" to \"{1}\" is {2:0.######}",
                    arguments[1], arguments[2], HMC.getPropabillity(arguments[1], arguments[2])));
            }
            else
                Console.WriteLine("Wrong argument count. Required: p word word");
        }
    }
}
