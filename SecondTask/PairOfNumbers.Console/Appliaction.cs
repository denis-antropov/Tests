namespace PairOfNumbers.Console
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents main application
    /// </summary>
    internal class Appliaction
    {
        private readonly IUserInteraction _userInteraction;

        /// <summary>
        /// Initializes a new instance of the Appliaction class
        /// </summary>
        /// <exception cref="ArgumentNullException">userInteraction is null</exception>
        internal Appliaction(IUserInteraction userInteraction)
        {
            if (userInteraction == null)
                throw new ArgumentNullException(nameof(userInteraction));

            _userInteraction = userInteraction;
        }

        /// <summary>
        /// Runs application
        /// </summary>
        internal void Run()
        {
            _userInteraction.DisplayInfo("This application displays pairs from an imput list. Sum of these pairs equal specific value X");
            _userInteraction.DisplayInfo(string.Empty);

            _userInteraction.DisplayInfo("Enter an input list of numbers (Int32) separated by space ' ':");
            _userInteraction.DisplayInfo("Example: 1 3 5 6");
            var line = _userInteraction.ReadUserInput();

            if (string.IsNullOrEmpty(line))
            {
                _userInteraction.DisplayInfo("The line is empty.");
                return;
            }

            var stringList = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            IEnumerable<int> numberList;
            try
            {
                numberList = ConvertToIntArray(stringList);
            }
            catch (ArgumentException ex)
            {
                _userInteraction.DisplayInfo(ex.Message);
                return;
            }

            _userInteraction.DisplayInfo("Enter X - sum of pair:");
            line = _userInteraction.ReadUserInput();
            if (string.IsNullOrEmpty(line))
            {
                _userInteraction.DisplayInfo("The line is empty.");
                return;
            }

            int x;
            if (!int.TryParse(line, out x))
            {
                _userInteraction.DisplayInfo("The X is invalid.");
                return;
            }

            var pairs = NumberTool.MatchSumOfPairs(numberList, x);

            DisplayPairs(pairs);
        }

        /// <summary>
        /// Convert string list to number list
        /// </summary>
        /// <param name="stringList">The string list</param>
        /// <returns>The number list</returns>
        /// <exception cref="ArgumentException">Unable to convert string list to number list</exception>
        private IEnumerable<int> ConvertToIntArray(string[] stringList)
        {
            var numberList = new int[stringList.Length];
            for (int i = 0; i < stringList.Length; i++)
            {
                int number;
                if (!int.TryParse(stringList[i], out number))
                {
                    throw new ArgumentException($"Unable to convert value '{stringList[i]}' to Int32");
                }

                numberList[i] = number;
            }

            return numberList;
        }

        /// <summary>
        /// Displays pairs
        /// </summary>
        /// <param name="pairs">Source pairs</param>
        private void DisplayPairs(IEnumerable<ValuesPair> pairs)
        {
            foreach (var item in pairs)
            {
                _userInteraction.DisplayInfo($"Pair: {item.Value1} - {item.Value2}");
            }
        }
    }
}
