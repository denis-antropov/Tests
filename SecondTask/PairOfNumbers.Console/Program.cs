namespace PairOfNumbers.Console
{
    using System;

    internal class Program
    {
        private static void Main()
        {
            var userInteraction = new ConsoleUserInteraction();
            Appliaction app = new Appliaction(userInteraction);
            try
            {
                app.Run();
            }
            catch (ApplicationException ex)
            {
                userInteraction.DisplayInfo(ex.Message);
                Environment.ExitCode = 1;
            }
        }

        private class ConsoleUserInteraction : IUserInteraction
        {
            public void DisplayInfo(string message)
            {
                Console.WriteLine(message);
            }

            public string ReadUserInput()
            {
                return Console.ReadLine();
            }
        }
    }
}
