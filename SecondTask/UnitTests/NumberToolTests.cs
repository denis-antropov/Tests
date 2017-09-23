namespace UnitTests
{
    using NUnit.Framework;
    using PairOfNumbers;
    using System.Collections.Generic;

    [TestFixture]
    public class NumberToolTests
    {
        [Test]
        public void ReturnsEmptyCollectionOfPairsIfNoMatchSum()
        {
            var inputCollection = new List<int> { 1, 7, 8, 15 };
            NumberTool pairOfNumbers = new NumberTool();

            var outputCollection = pairOfNumbers.MatchSumOfPairs(inputCollection);

            CollectionAssert.IsEmpty(outputCollection);
        }
    }
}
