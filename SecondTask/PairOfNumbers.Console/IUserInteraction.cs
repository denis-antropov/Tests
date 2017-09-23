namespace PairOfNumbers.Console
{
    /// <summary>
    /// Represents an interface for user interaction
    /// </summary>
    internal interface IUserInteraction
    {
        /// <summary>
        /// Displays the message
        /// </summary>
        /// <param name="message">The message to display</param>
        void DisplayInfo(string message);

        /// <summary>
        /// Reads user input
        /// </summary>
        /// <returns>The user text input</returns>
        string ReadUserInput();
    }
}
