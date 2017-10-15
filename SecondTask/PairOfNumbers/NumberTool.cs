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
        /// <param name="x">Required sum of pairs</param>
        /// <returns>Sum of pairs</returns>
        /// <exception cref="ArgumentNullException">inputCollection is null</exception>
        /// <exception cref="ArgumentException">inputCollection has invalid numbers</exception>
        public static IEnumerable<ValuesPair> MatchSumOfPairs(IEnumerable<int> inputCollection, int x)
        {
            if (inputCollection == null)
                throw new ArgumentNullException(nameof(inputCollection));

            var inputArray = inputCollection.ToArray();
            Array.Sort(inputArray);
            List<ValuesPair> pairs = new List<ValuesPair>();

            int lastIndex = inputArray.Length - 1;
            int firstIndex = 0;
            while (firstIndex < lastIndex)
            {
                int sum = 0;
                var firstItem = inputArray[firstIndex];
                var lastItem = inputArray[lastIndex];

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
                
                if (sum == x)
                {
                    pairs.Add(new ValuesPair { Value1 = firstItem, Value2 = lastItem });
                    firstIndex++;
                    lastIndex--;
                }
                else
                {
                    if (sum < x)
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
            }

            // Remove duplicates
            return pairs.Distinct();
        }
    }
}