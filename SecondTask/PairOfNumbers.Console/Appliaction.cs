namespace PairOfNumbers.Console
{
    using System;
    using System.Collections.Generic;

    internal class Appliaction
    {
        private readonly IUserInteraction _userInteraction;

        internal Appliaction(IUserInteraction userInteraction)
        {
            _userInteraction = userInteraction ?? throw new ArgumentNullException(nameof(userInteraction));
        }

        internal void Run()
        {
            _userInteraction.DisplayInfo("This application displays pairs from an input list. Sum of these pairs equal specific value X");
            _userInteraction.DisplayInfo(string.Empty);

            var numberList = GetNumberList();
            var sumOfPairs = GetSummOfPairs();

            try
            {
                var pairs = NumberTool.MatchSumOfPairs(numberList, sumOfPairs);

                DisplayPairs(pairs);
            }
            catch (ArgumentException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        private IReadOnlyCollection<int> GetNumberList()
        {
            _userInteraction.DisplayInfo("Enter an input list of numbers (Int32) separated by space ' ':");
            _userInteraction.DisplayInfo("Example: 1 3 5 6");
            var line = _userInteraction.ReadUserInput();

            if (string.IsNullOrEmpty(line))
            {
                throw new ApplicationException("The line is empty.");
            }

            var stringList = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return ConvertToIntArray(stringList);
        }

        private int GetSummOfPairs()
        {
            _userInteraction.DisplayInfo("Enter X - sum of pair:");
            var line = _userInteraction.ReadUserInput();
            if (string.IsNullOrEmpty(line))
            {
                throw new ApplicationException("The line is empty.");
            }

            if (!int.TryParse(line, out int x))
            {
                throw new ApplicationException("The X is invalid.");
            }

            return x;
        }

        private IReadOnlyCollection<int> ConvertToIntArray(string[] stringList)
        {
            var numberList = new int[stringList.Length];
            for (int i = 0; i < stringList.Length; i++)
            {
                if (!int.TryParse(stringList[i], out int number))
                {
                    throw new ApplicationException($"Unable to convert value '{stringList[i]}' to Int32");
                }

                numberList[i] = number;
            }

            return numberList;
        }

        private void DisplayPairs(IEnumerable<ValuesPair> pairs)
        {
            foreach (var item in pairs)
            {
                _userInteraction.DisplayInfo($"Pair: {item.Value1} - {item.Value2}");
            }
        }
    }
}
