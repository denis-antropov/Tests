namespace PairOfNumbers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a class for match sum of pairs
    /// </summary>
    public static class NumberTool
    {
        /// <summary>
        /// Matches sum of pairs of input collection.
        /// </summary>
        /// <param name="inputCollection">Input collection for matches</param>
        /// <param name="sumOfPairs">Required sum of pairs</param>
        /// <returns>Sum of pairs</returns>
        /// <exception cref="ArgumentNullException">inputCollection is null</exception>
        /// <exception cref="ArgumentException">inputCollection has invalid numbers</exception>
        public static IEnumerable<ValuesPair> MatchSumOfPairs(IEnumerable<int> inputCollection, int sumOfPairs)
        {
            if (inputCollection == null)
                throw new ArgumentNullException(nameof(inputCollection));

            var inputArray = inputCollection.ToArray();
            Array.Sort(inputArray);
            var pairs = new List<ValuesPair>();

            var lastIndex = inputArray.Length - 1;
            var firstIndex = 0;

            while (firstIndex < lastIndex)
            {
                var firstItem = inputArray[firstIndex];
                var lastItem = inputArray[lastIndex];

                int sum;
                try
                {
                    checked
                    {
                        sum = firstItem + lastItem;
                    }
                }
                catch (OverflowException ex)
                {
                    throw new ArgumentException(
                        string.Format(Localization.strSumIsTooBig,
                            firstIndex, firstItem, lastIndex, lastItem, int.MaxValue),
                        ex);
                }

                if (sum == sumOfPairs)
                {
                    // Exclude duplicates
                    if (pairs.Count == 0 || pairs.Last().Value1 != firstItem)
                    {
                        var pair = new ValuesPair { Value1 = firstItem, Value2 = lastItem };
                        pairs.Add(pair);
                    }
                    
                    firstIndex++;
                    lastIndex--;
                }
                else if (sum < sumOfPairs)
                {
                    // Current lastItem cannot be bigger, therefore for current firstItem there is no pair
                    firstIndex++;
                }
                else
                {
                    // Current firstItem cannot be smaller, therefore for current lastItem there is no pair
                    lastIndex--;
                }
            }

            return pairs;
        }
    }
}