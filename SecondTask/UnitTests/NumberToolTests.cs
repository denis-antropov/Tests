namespace UnitTests
{
    using NUnit.Framework;
    using PairOfNumbers;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [TestFixture]
    public class NumberToolTests
    {
        [Test]
        public void ReturnsEmptyCollectionIfInputCollectionIsEmpty()
        {
            var inputCollection = new List<int>();
            var x = 20;

            var outputCollection = NumberTool.MatchSumOfPairs(inputCollection, x);

            CollectionAssert.IsEmpty(outputCollection);
        }

        [Test]
        public void ReturnsEmptyCollectionOfPairsIfNoMatchSum()
        {
            var inputCollection = new List<int> { 1, 7, 8, 15 };
            var x = 20;

            var outputCollection = NumberTool.MatchSumOfPairs(inputCollection, x);

            CollectionAssert.IsEmpty(outputCollection);
        }

        [Test]
        public void ReturnsOnePairIfOnlyOneMatchSum()
        {
            var inputCollection = new List<int> { 1, 7 };
            var x = 8;

            var outputCollection = NumberTool.MatchSumOfPairs(inputCollection, x);

            CollectionAssert.IsNotEmpty(outputCollection);
            Assert.IsTrue(outputCollection.Count() == 1);
            Assert.AreEqual(x, outputCollection.Single().Value1 + outputCollection.Single().Value2);
        }

        [Test]
        public void ReturnsThreePairsIfThreeMatchSum()
        {
            var inputCollection = new List<int> { 1, 7, 2, 6, 4 };
            var x = 8;

            var outputCollection = NumberTool.MatchSumOfPairs(inputCollection, x);

            CollectionAssert.IsNotEmpty(outputCollection);
            Assert.IsTrue(outputCollection.Count() == 2);
            Assert.IsTrue(outputCollection.All(p => p.Value1 + p.Value2 == x));
        }

        [Test]
        public void ThrowsNullExceptionIfInputCollectionIsNull()
        {
            var x = 8;

            Assert.Catch<ArgumentNullException>(() => NumberTool.MatchSumOfPairs(null, x));
        }

        [Test]
        public void ThrowsOnPairsInCaseOfOverflowSum()
        {
            var inputCollection = new List<int> { 1, 7, 2, int.MaxValue };
            var x = 4;

            Assert.Catch<ArgumentException>(() => NumberTool.MatchSumOfPairs(inputCollection, x));
        }

        [Test]
        public void ReturnsOnePairIfALotOfTheSameMatchSum()
        {
            var inputCollection = new List<int> { 1, 1, 1, 1, 1 };
            var x = 2;

            var outputCollection = NumberTool.MatchSumOfPairs(inputCollection, x);

            CollectionAssert.IsNotEmpty(outputCollection);
            Assert.IsTrue(outputCollection.Count() == 1);
            Assert.AreEqual(x, outputCollection.Single().Value1 + outputCollection.Single().Value2);
        }

        [Test]
        public void ReturnsOnePairIfTwoTheSameMatchSumWithDifferentOrder()
        {
            var inputCollection = new List<int> { 1, 7, 1 };
            var x = 8;

            var outputCollection = NumberTool.MatchSumOfPairs(inputCollection, x);

            CollectionAssert.IsNotEmpty(outputCollection);
            Assert.IsTrue(outputCollection.Count() == 1);
            Assert.AreEqual(x, outputCollection.Single().Value1 + outputCollection.Single().Value2);
        }
    }
}
