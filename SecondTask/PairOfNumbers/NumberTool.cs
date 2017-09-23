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
        public static IEnumerable<ValuesPair> MatchSumOfPairs(IEnumerable<int> inputCollection, int x)
        {
            if (inputCollection == null)
                throw new ArgumentNullException(nameof(inputCollection));

            var inputArray = inputCollection.ToArray();
            List<ValuesPair> pairs = new List<ValuesPair>();

            for (int i = 0; i < inputArray.Length; i++)
            {
                for (int j = i + 1; j < inputArray.Length; j++)
                {
                    try
                    {
                        checked
                        {
                            if (inputArray[i] + inputArray[j] == x)
                            {
                                pairs.Add(new ValuesPair { Value1 = inputArray[i], Value2 = inputArray[j] });
                            }
                        }
                    }
                    catch (OverflowException)
                    {
                        // Here we could throw ArgumentException,
                        // But anyway x is Int32 therefore sum of this pair will be false
                    }
                }
            }

            return pairs.Distinct();
        }
    }
}