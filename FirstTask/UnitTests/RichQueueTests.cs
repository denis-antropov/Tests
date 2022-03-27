namespace UnitTests
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using NUnit.Framework;
    using Queue;

    [TestFixture]
    public class RichQueueTests
    {
        [Test]
        public void PushIncreasesCount()
        {
            using (var richQueue = new RichQueue<int>())
            {
                richQueue.Push(5);
                richQueue.Push(7);

                Assert.AreEqual(2, richQueue.Count);
            }
        }

        [Test]
        public void PopReturnsPushedItem()
        {
            using (var richQueue = new RichQueue<int>())
            {
                richQueue.Push(5);
                var actualValue = richQueue.Pop();

                Assert.AreEqual(5, actualValue);
            }
        }

        [Test]
        public void PopDecreasesCount()
        {
            using (var richQueue = new RichQueue<int>())
            {
                richQueue.Push(5);
                var countBeforePop = richQueue.Count;

                richQueue.Pop();

                Assert.AreEqual(countBeforePop - 1, richQueue.Count);
            }
        }

        [Test]
        public void PopTwiseReturnsTwoPushedItem()
        {
            using (var richQueue = new RichQueue<int>())
            {
                richQueue.Push(5);
                richQueue.Push(7);

                var actualValue1 = richQueue.Pop();
                var actualValue2 = richQueue.Pop();

                Assert.AreEqual(5, actualValue1);
                Assert.AreEqual(7, actualValue2);
            }
        }

        [Test]
        public void PopWaitsWhenItemWillBeAvailable()
        {
            using (var richQueue = new RichQueue<int>())
            {
                using (var resetEvent = new ManualResetEvent(false))
                {
                    var task = Task.Run(() =>
                    {
                        resetEvent.Set();

                        return richQueue.Pop();
                    });

                    // Wait when Pop task begin
                    resetEvent.WaitOne();
                    richQueue.Push(5);

                    Assert.AreEqual(5, task.Result);
                }
            }
        }

        [Test]
        public void TwoPopsWaitWhenItemsWillBeAvailable()
        {
            using (var richQueue = new RichQueue<int>())
            {
                using (var resetEvent = new AutoResetEvent(false))
                {
                    var task1 = Task.Run(() =>
                    {
                        resetEvent.Set();
                        return richQueue.Pop();
                    });

                    // Wait when Pop task begin
                    resetEvent.WaitOne();

                    var task2 = Task.Run(() =>
                    {
                        resetEvent.Set();
                        return richQueue.Pop();
                    });

                    // Wait when Pop task begin
                    resetEvent.WaitOne();

                    richQueue.Push(5);
                    richQueue.Push(7);

                    Assert.AreEqual(5, task1.Result);
                    Assert.AreEqual(7, task2.Result);
                }
            }
        }

        [Test]
        public void PushThrowsOnDisposedObject()
        {
            var richQueue = new RichQueue<int>();
            richQueue.Dispose();

            Assert.Catch<ObjectDisposedException>(() => richQueue.Push(5));
        }

        [Test]
        public void PopThrowsOnDisposedObject()
        {
            var richQueue = new RichQueue<int>();
            richQueue.Push(5);
            richQueue.Dispose();

            Assert.Catch<ObjectDisposedException>(() => richQueue.Pop());
        }

        [Test]
        public void CountThrowsOnDisposedObject()
        {
            var richQueue = new RichQueue<int>();
            richQueue.Push(5);
            richQueue.Dispose();

            Assert.Catch<ObjectDisposedException>(() =>
            {
                var count = richQueue.Count;
            });
        }

        [Test]
        public void SeveralDisposeCallsNotThrowsException()
        {
            var richQueue = new RichQueue<int>();
            richQueue.Dispose();
            richQueue.Dispose();
        }

        [Test]
        public void DisposeResetsWaitingPopsAndThrowsException()
        {
            using (var resetEvent = new ManualResetEvent(false))
            {
                var richQueue = new RichQueue<int>();
                var task = Task.Run(() =>
                {
                    resetEvent.Set();
                    return richQueue.Pop();
                });

                // Wait when Pop task begin
                resetEvent.WaitOne();

                richQueue.Dispose();

                Assert.Throws<AggregateException>(() => task.Wait());
                Assert.IsInstanceOf<ObjectDisposedException>(task.Exception.InnerException);
            }
        }
    }
}
