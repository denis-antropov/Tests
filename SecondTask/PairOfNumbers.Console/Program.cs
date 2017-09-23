namespace PairOfNumbers.Console
{
    using System;

    /// <summary>
    /// Entry class
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry method
        /// </summary>
        static void Main()
        {
            Appliaction app = new Appliaction(new ConsoleUserInteraction());
            app.Run();
        }

        /// <summary>
        /// Represents iplementation of IUserInteraction interface using console
        /// </summary>
        class ConsoleUserInteraction : IUserInteraction
        {
            /// <summary>
            /// Displays the message
            /// </summary>
            /// <param name="message">The message to display</param>
            public void DisplayInfo(string message)
            {
                Console.WriteLine(message);
            }

            /// <summary>
            /// Reads user input
            /// </summary>
            /// <returns>The user text input</returns>
            public string ReadUserInput()
            {
                return Console.ReadLine();
            }
        }
    }
}
