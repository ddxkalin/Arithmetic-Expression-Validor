namespace ArithmeticExpressionValidator
{
    internal static class Presenter
    {
        private static void Main()
        {
            using var solver = new Solver();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("**********************************************************");
            Console.WriteLine("==========================================================");
            Console.WriteLine("***      Arithmetic Expression has been launched       ***");
            Console.WriteLine("===____________________________________________________===");
            Console.WriteLine("*** To evaluate an expression, write it to the console ***");
            Console.WriteLine("===                  and press Enter.                  ===");
            Console.WriteLine("***----------------------------------------------------***");
            Console.WriteLine("===      If you want to close Arithmetic Expression,   ===");
            Console.WriteLine("***      write 'q' to the console, and press Enter     ***");
            Console.WriteLine("==========================================================");
            Console.WriteLine("**********************************************************");
            Console.WriteLine("                                                          ");
            Console.ResetColor();

            //TODO: discusting but can't think of something to replace this while(true)....
            while (true)
            {
                Console.Write("Enter Expression: ");
                var request = Console.ReadLine();
                if (request == "q")
                {
                    Console.ResetColor();
                    break;
                }

                var (answer, expression, errors) = solver.SendAndEval(request);

                if (errors.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Calculated Result: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(answer);
                    Console.WriteLine();
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("Invalid Expression !!!");
                    Console.ResetColor();
                }

                Console.WriteLine();
                Console.WriteLine("**********************************************************");
            }
        }
    }
}