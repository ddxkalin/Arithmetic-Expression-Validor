using System;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Please enter an expresion: ");

                try
                {
                    Console.WriteLine(Parser.Eval(Console.ReadLine()));
                }
                catch (ParsingException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Number is too");
                }
            }
        }
    }
}

