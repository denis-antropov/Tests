namespace UnitTests.ViewModels
{
    using System;
    using Moq;
    using NUnit.Framework;
    using Workers.ViewModels;
    using System.Collections.Generic;
    using System.Linq;
    using Workers.BusinessLogic;
    using Workers.ViewModels.Interfaces;
    using z3r0.Utils;

    [TestFixture]
    public class WorkerListViewModelTests
    {
        private WorkerListViewModel _workerList;
        private Mock<IWorkersService> _workerService;
        private Mock<IWorkerModifier> _workerModifier;

        [SetUp]
        public void Initializtion()
        {
            _workerService = new Mock<IWorkersService>();
            _workerService.Setup(r => r.GetWorkers()).Returns(GetMockWorkers);

            _workerModifier = new Mock<IWorkerModifier>();

            _workerList = new WorkerListViewModel(
                _workerService.Object, _workerModifier.Object, new WorkerItemFactory(), new Mock<IUserInteraction>().Object);
        }

        private IEnumerable<IWorker> GetMockWorkers()
        {
            var worker1 = new Mock<IWorker>();
            worker1.SetupGet(w => w.Id).Returns(() => 5);
            worker1.SetupGet(w => w.Name).Returns(() => "Vasya");
            worker1.SetupGet(w => w.Surname).Returns(() => "Ivanov");

            var worker2 = new Mock<IWorker>();
            worker2.SetupGet(w => w.Id).Returns(() => 6);
            worker2.SetupGet(w => w.Name).Returns(() => "Addard");
            worker2.SetupGet(w => w.Surname).Returns(() => "Stark");

            return new List<IWorker>
                {
                    worker1.Object,
                    worker2.Object
                };
        }

        [Test]
        public void ThrowsOnNullWorkerService()
        {
            IWorkersService service = null;
            Assert.Catch<ArgumentNullException>(() => new WorkerListViewModel
            (service, _workerModifier.Object, new WorkerItemFactory(), new Mock<IUserInteraction>().Object));
        }

        [Test]
        public void ThrowsOnNullWorkerModifier()
        {
            Assert.Catch<ArgumentNullException>(() => new WorkerListViewModel(
                _workerService.Object, null, new WorkerItemFactory(), new Mock<IUserInteraction>().Object));
        }

        [Test]
        public void ReturnsListOfWorkers()
        {
            var workers = _workerList.Workers;

            CollectionAssert.AreEquivalent(
                _workerService.Object.GetWorkers().Select(w => w.Id),
                workers.Select(w => w.Id));
        }

        [Test]
        public void CannotExecuteDeleteIfSelectedWorkerIsNull()
        {
            _workerList.SelectedWorker = null;

            Assert.IsFalse(_workerList.DeleteWorkerCommand.CanExecute(null));
        }

        [Test]
        public void CanExecuteDeleteIfSelectedWorkerIsNotNull()
        {
            _workerList.SelectedWorker = _workerList.Workers.First();

            Assert.IsTrue(_workerList.DeleteWorkerCommand.CanExecute(null));
        }

        [Test]
        public void DeletesSelectedWorker()
        {
            var workerToDelete = new Mock<IWorker>();
            var deleteCalled = false;
            workerToDelete.Setup(w => w.Delete()).Callback(() => deleteCalled = true);
            var workerItemToDelete = new WorkerItem(workerToDelete.Object);
            _workerList.Workers.Add(workerItemToDelete);

            _workerList.SelectedWorker = workerItemToDelete;
            _workerList.DeleteWorkerCommand.Execute(null);

            CollectionAssert.DoesNotContain(_workerList.Workers, workerToDelete);
            Assert.IsTrue(deleteCalled);
            Assert.IsNull(_workerList.SelectedWorker);
        }

        [Test]
        public void CreatesNewWorker()
        {
            var newWorker = new Mock<IWorker>();
            newWorker.SetupGet(w => w.IsNew).Returns(() => true);
            _workerService.Setup(s => s.CreateNew()).Returns(() => newWorker.Object);
            IWorker modifiedWorker = null;
            _workerModifier.Setup(m => m.Modify(It.IsAny<IWorker>()))
                .Callback<IWorker>(w => modifiedWorker = w);

            _workerList.CreateWorkerCommand.Execute(null);

            Assert.IsNotNull(modifiedWorker);
            Assert.IsTrue(modifiedWorker.IsNew);
        }

        [Test]
        public void AddsCreatedWorkerIfWorkerModifierReturnsTrue()
        {
            var newWorker = new Mock<IWorker>();
            newWorker.SetupGet(w => w.Id).Returns(() => 55);
            _workerService.Setup(s => s.CreateNew()).Returns(() => newWorker.Object);

            _workerList.CreateWorkerCommand.Execute(null);
            _workerModifier.Raise(
                m => m.ModificationFinished += null, 
                new ModificationStateEventArgs(true, newWorker.Object));

            CollectionAssert.Contains(_workerList.Workers.Select(w => w.Id), 55);
            Assert.AreEqual(55, _workerList.SelectedWorker.Id);
        }

        [Test]
        public void NotAddsCreatedWorkerIfWorkerModifierReturnsTrue()
        {
            var newWorker = new Mock<IWorker>();
            newWorker.SetupGet(w => w.Id).Returns(() => 55);
            _workerService.Setup(s => s.CreateNew()).Returns(() => newWorker.Object);

            _workerList.CreateWorkerCommand.Execute(null);
            _workerModifier.Raise(
                m => m.ModificationFinished += null,
                new ModificationStateEventArgs(false, newWorker.Object));

            CollectionAssert.DoesNotContain(_workerList.Workers.Select(w => w.Id), 55);
        }

        [Test]
        public void SelectedWorkerNotifiesIfChanged()
        {
            var selectedWorkerChangedCalled = false;
            _workerList.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(_workerList.SelectedWorker))
                    selectedWorkerChangedCalled = true;
            };
            _workerList.SelectedWorker = _workerList.Workers.First();

            Assert.IsTrue(selectedWorkerChangedCalled);
        }

        [Test]
        public void CannotExecuteEditIfSelectedWorkerIsNull()
        {
            _workerList.SelectedWorker = null;

            Assert.IsFalse(_workerList.EditWorkerCommand.CanExecute(null));
        }

        [Test]
        public void CanExecuteEditIfSelectedWorkerIsNotNull()
        {
            _workerList.SelectedWorker = _workerList.Workers.First();

            Assert.IsTrue(_workerList.EditWorkerCommand.CanExecute(null));
        }

        [Test]
        public void EditWorkerUsingWorkerModifier()
        {
            IWorker modifiedWorker = null;
            _workerModifier.Setup(m => m.Modify(It.IsAny<IWorker>()))
                .Callback<IWorker>(w => modifiedWorker = w);

            _workerList.SelectedWorker = _workerList.Workers.First();
            _workerList.EditWorkerCommand.Execute(null);

            Assert.IsNotNull(modifiedWorker);
        }

        [Test]
        public void RefreshsEditedWorkerIfWorkerModifierReturnsTrue()
        {
            var workerToEdit = new Mock<IWorker>();
            var workerItemToEdit = new WorkerItem(workerToEdit.Object);
            _workerList.Workers.Add(workerItemToEdit);
            var notificationCalled = false;
            workerItemToEdit.PropertyChanged += (s, e) => notificationCalled = true;

            _workerList.SelectedWorker = workerItemToEdit;
            _workerList.EditWorkerCommand.Execute(null);
            _workerModifier.Raise(
                m => m.ModificationFinished += null,
                new ModificationStateEventArgs(true, workerToEdit.Object));

            Assert.IsTrue(notificationCalled);
        }

        [Test]
        public void NotRefreshsEditedWorkerIfWorkerModifierReturnsTrue()
        {
            var workerToEdit = new Mock<IWorker>();
            var workerItemToEdit = new WorkerItem(workerToEdit.Object);
            _workerList.Workers.Add(workerItemToEdit);
            var notificationCalled = false;
            workerItemToEdit.PropertyChanged += (s, e) => notificationCalled = true;

            _workerList.SelectedWorker = workerItemToEdit;
            _workerList.EditWorkerCommand.Execute(null);
            _workerModifier.Raise(
                m => m.ModificationFinished += null,
                new ModificationStateEventArgs(false, workerToEdit.Object));

            Assert.IsFalse(notificationCalled);
        }
    }
}
