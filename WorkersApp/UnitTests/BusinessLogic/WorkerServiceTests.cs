﻿namespace UnitTests.BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Moq;
    using NUnit.Framework;
    using Workers.BusinessLogic;
    using Workers.DataLayer;

    [TestFixture]
    public class WorkerServiceTests
    {
        private Mock<IRepository<WorkerEntity>> _repository;
        private WorkerService _workerService;
        private List<WorkerEntity> _entitiesMock;

        [SetUp]
        public void Initializtion()
        {
            _entitiesMock = new List<WorkerEntity>
                {
                    new WorkerEntity {Id = 5, Name= "Vasya", Surname = "Ivanov"},
                    new WorkerEntity {Id = 6, Name= "Addard", Surname = "Stark"}
                };

            _repository = new Mock<IRepository<WorkerEntity>>();
            _repository.Setup(r => r.GetEntities()).Returns(() => _entitiesMock.AsQueryable());
            _repository.Setup(r => r.Add(It.IsAny<WorkerEntity>()))
                .Callback<WorkerEntity>(e => _entitiesMock.Add(e));
            _repository.Setup(r => r.Delete(It.IsAny<WorkerEntity>()))
                .Callback<WorkerEntity>(e => _entitiesMock.RemoveAll(es => es.Id == e.Id));

            _workerService = new WorkerService(_repository.Object);
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
        public void DeleteThrowsOnNonExistentInStore()
        {
            var worker = _workerService.CreateNew();

            Assert.Catch<InvalidOperationException>(() => worker.Delete());
        }

        [Test]
        public void ReturnListOfWorkers()
        {
            var workers = _workerService.GetWorkers();

            CollectionAssert.AreEquivalent(
                GetMockEntities().Select(e => e.Id), workers.Select(w => w.Id));
            Assert.IsFalse(workers.Any(w => w.IsNew));
        }

        [Test]
        public void SaveResetsNewIdFlagAndSavesInStore()
        {
            var worker = _workerService.CreateNew();
            worker.Name = "Simple name";
            worker.Surname = "Surname";
            worker.Save();
                        
            Assert.IsFalse(worker.IsNew);
            CollectionAssert.Contains(_workerService.GetWorkers().Select(w => w.Id), worker.Id);
            CollectionAssert.Contains(_entitiesMock.Select(e => e.Id), worker.Id);
        }

        [Test]
        public void SaveMapsDataToExistentEntity()
        {
            WorkerEntity entity = null;
            _repository.Setup(r => r.Save(It.IsAny<WorkerEntity>())).Callback<WorkerEntity>(e => entity = e);

            var worker = _workerService.GetWorkers().First();
            worker.Name = "Olya";
            worker.Surname = "Sidorova";
            worker.Sex = Sex.Female;

            worker.Save();

            Assert.IsNotNull(entity);
            Assert.AreEqual(worker.Name, entity.Name);
            Assert.AreEqual(worker.Surname, entity.Surname);
            Assert.AreEqual((long)worker.Sex, entity.Sex);
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

        [Test]
        public void ProhibitsSaveWorkerWhichIdChangedUsingReflection()
        {
            var worker = _workerService.CreateNew();
            var propertyId = worker.GetType().GetProperty(nameof(worker.Id));
            propertyId.SetValue(worker, 26);

            Assert.Catch<InvalidOperationException>(() => worker.Save());
        }

        [Test]
        public void RemoveThrowsOnNewWorker()
        {
            var worker = _workerService.CreateNew();

            Assert.Catch<InvalidOperationException>(() => worker.Delete());
        }

        [Test]
        public void RemovesWorkerFromStore()
        {
            var worker = _workerService.GetWorkers().First();
            worker.Delete();

            CollectionAssert.DoesNotContain(_workerService.GetWorkers().Select(w => w.Id), worker.Id);
        }

        [Test]
        public void ProhibitsDeleteWorkerWhichIdChangedUsingReflection()
        {
            var worker = _workerService.CreateNew();
            var propertyId = worker.GetType().GetProperty(nameof(worker.Id));
            propertyId.SetValue(worker, 26);

            Assert.Catch<InvalidOperationException>(() => worker.Delete());
        }

        [Test]
        public void RollbackThrowsOnNewWorker()
        {
            var worker = _workerService.CreateNew();

            Assert.Catch<InvalidOperationException>(() => worker.Rollback());
        }

        [Test]
        public void RollbacksWorkerFromStore()
        {
            var worker = _workerService.GetWorkers().First();
            worker.Name = "Zina";
            worker.Rollback();

            Assert.AreNotEqual("Zina", worker.Name);
        }

        private List<WorkerEntity> GetMockEntities()
        {
            return new List<WorkerEntity>
                {
                    new WorkerEntity {Id = 5, Name= "Vasya", Surname = "Ivanov"},
                    new WorkerEntity {Id = 6, Name= "Addard", Surname = "Stark"}
                };
        }
    }
}
