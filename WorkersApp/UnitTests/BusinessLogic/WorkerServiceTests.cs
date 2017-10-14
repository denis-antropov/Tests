namespace UnitTests.BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Moq;
    using NUnit.Framework;
    using Workers.BusinessLogic;
    using Workers.BusinessLogic.Interfaces;
    using Workers.DataLayer;

    [TestFixture]
    public class WorkerServiceTests
    {
        private Mock<IRepository<WorkerEntity>> _repository;
        private WorkerService _workerService;

        [SetUp]
        public void Initializtion()
        {
            _repository = new Mock<IRepository<WorkerEntity>>();
            _repository.Setup(r => r.GetEntities()).Returns(GetMockEntities);

            _workerService = new WorkerService(_repository.Object);
        }

        private IEnumerable<WorkerEntity> GetMockEntities()
        {
            return new List<WorkerEntity>
                {
                    new WorkerEntity {Id = 5, Name= "Vasya", Surname = "Ivanov"},
                    new WorkerEntity {Id = 6, Name= "Addard", Surname = "Stark"}
                };
        }

        [Test]
        public void ThrowsOnNullRepository()
        {
            Assert.Catch<ArgumentNullException>(() => new WorkerService(null));
        }

        [Test]
        public void CreatesWorkerWithInvalidId()
        {
            var worker = _workerService.CreateNew();

            Assert.IsTrue(worker.IsNew);
        }

        [Test]
        public void RollbackThrowsOnNonExistentInStore()
        {
            var worker = _workerService.CreateNew();

            Assert.Catch<ArgumentException>(() => worker.Rollback());
        }

        [Test]
        public void DeleteThrowsOnNonExistentInStore()
        {
            var worker = _workerService.CreateNew();

            Assert.Catch<ArgumentException>(() => worker.Delete());
        }

        [Test]
        public void ReturnListOfWorkers()
        {
            var workers = _workerService.GetWorkers();

            CollectionAssert.AreEquivalent(
                GetMockEntities().Select(e => e.Id), workers.Select(w => w.Id));
        }

        [Test]
        public void SaveResetsNewIdFlagAndSavesInStore()
        {
            bool saveCalled = false;
            _repository.Setup(r => r.Save(It.IsAny<WorkerEntity>())).Callback(() => saveCalled = true);

            var worker = _workerService.CreateNew();
            worker.Save();
                        
            Assert.IsFalse(worker.IsNew);
            CollectionAssert.Contains(_workerService.GetWorkers().Select(w => w.Id), worker.Id);
            Assert.IsTrue(saveCalled);
        }

        [Test]
        public void TwoSavesIndicatesTwoIndexes()
        {
            var worker1 = _workerService.CreateNew();
            worker1.Save();

            var worker2 = _workerService.CreateNew();
            worker2.Save();

            Assert.IsTrue(worker1.Id != worker2.Id);
        }
    }
}
