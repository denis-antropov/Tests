namespace UnitTests.BusinessLogic
{
    using System;
    using Moq;
    using NUnit.Framework;
    using Workers.BusinessLogic;

    [TestFixture]
    public class WorkerTests
    {
        private Mock<ILifeCycleService<IWorker>> _lifyCycle;
        private Worker _worker;

        [SetUp]
        public void Initializtion()
        {
            _lifyCycle = new Mock<ILifeCycleService<IWorker>>();
            _worker = new Worker(_lifyCycle.Object);
        }

        [Test]
        public void ThrowsOnNullLifeCycleService()
        {
            Assert.Catch<ArgumentNullException>(() => new Worker(null));
        }

        [Test]
        public void ThrowsOnNullChangingName()
        {
            Assert.Catch<ArgumentException>(() => _worker.Name = string.Empty);
        }

        [Test]
        public void ThrowsOnNullChangingSurname()
        {
            Assert.Catch<ArgumentException>(() => _worker.Surname = string.Empty);
        }

        [Test]
        public void UpdatesPropertyCorrectly()
        {
            _worker.Name = "Petya";
            Assert.AreEqual("Petya", _worker.Name);

            _worker.Surname = "Petrov";
            Assert.AreEqual("Petrov", _worker.Surname);
        }

        [Test]
        public void InteractWithLifeCycleWhenSave()
        {
            var saveCalled = false;
            _lifyCycle.Setup(l => l.Save(It.IsAny<IWorker>())).Callback(() => saveCalled = true);

            _worker.Save();
            Assert.IsTrue(saveCalled);
        }

        [Test]
        public void InteractWithLifeCycleWhenDelete()
        {
            var deleteCalled = false;
            _lifyCycle.Setup(l => l.Delete(It.IsAny<IWorker>())).Callback(() => deleteCalled = true);

            _worker.Delete();
            Assert.IsTrue(deleteCalled);
        }

        [Test]
        public void InteractWithLifeCycleWhenRollback()
        {
            var rollbackCalled = false;
            _lifyCycle.Setup(l => l.Rollback(It.IsAny<IWorker>())).Callback(() => rollbackCalled = true);

            _worker.Rollback();
            Assert.IsTrue(rollbackCalled);
        }
    }
}
