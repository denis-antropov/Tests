namespace UnitTests.ViewModels
{
    using System;
    using Moq;
    using NUnit.Framework;
    using Workers.BusinessLogic.Interfaces;
    using Workers.ViewModels;

    [TestFixture]
    public class WorkerViewModelTests
    {
        private WorkerViewModel _workerViewModel;
        private Mock<IWorker> _worker;

        [SetUp]
        public void Initializtion()
        {
            _worker = new Mock<IWorker>();
            ConfigureWorker();

            _worker.Setup(w => w.Rollback()).Callback(ConfigureWorker);
            
            _workerViewModel = new WorkerViewModel(_worker.Object);
        }

        private void ConfigureWorker()
        {
            _worker.SetupGet(w => w.Name).Returns(() => "Vasya");
            _worker.SetupGet(w => w.Surname).Returns(() => "Ivanov");

            _worker.SetupSet(w => w.Name = It.IsAny<string>())
                .Callback<string>(value => _worker.SetupGet(w => w.Name).Returns(() => value));

            _worker.SetupSet(w => w.Surname = It.IsAny<string>())
                .Callback<string>(value => _worker.SetupGet(w => w.Surname).Returns(() => value));

            _worker.SetupGet(w => w.Birthday).Returns(() => new DateTime(1995, 4, 10));
            _worker.SetupGet(w => w.Sex).Returns(() => Sex.Male);
            _worker.SetupGet(w => w.HasChildren).Returns(() => true);
        }

        [Test]
        public void ThrowsOnNullWorker()
        {
            Assert.Catch<ArgumentNullException>(() => new WorkerViewModel(null));
        }

        [Test]
        public void PropertiesTheSameAsInSourceWorker()
        {
            Assert.AreEqual(_worker.Object.Name, _workerViewModel.Name);
            Assert.AreEqual(_worker.Object.Surname, _workerViewModel.Surname);
            Assert.AreEqual(_worker.Object.Birthday, _workerViewModel.Birthday);
            Assert.AreEqual(_worker.Object.Sex, _workerViewModel.Sex);
            Assert.AreEqual(_worker.Object.HasChildren, _workerViewModel.HasChildren);
        }

        [Test]
        public void UpdatesPropertyCorrectly()
        {
            _workerViewModel.Name = "Petya";
            Assert.AreEqual("Petya", _workerViewModel.Name);

            _workerViewModel.Surname = "Petrov";
            Assert.AreEqual("Petrov", _workerViewModel.Surname);
        }

        [Test]
        public void NotifiesIfChangedProperty()
        {           
            var calledPropertyName = string.Empty;
            _workerViewModel.PropertyChanged += (o, e) => calledPropertyName = e.PropertyName;

            _workerViewModel.Name = "Petya";
            Assert.AreEqual(nameof(_workerViewModel.Name), calledPropertyName);

            _workerViewModel.Surname = "Petrov";
            Assert.AreEqual(nameof(_workerViewModel.Surname), calledPropertyName);

            _workerViewModel.Birthday = new DateTime(1990, 4, 10);
            Assert.AreEqual(nameof(_workerViewModel.Birthday), calledPropertyName);

            _workerViewModel.Sex = Sex.Female;
            Assert.AreEqual(nameof(_workerViewModel.Sex), calledPropertyName);

            _workerViewModel.HasChildren = false;
            Assert.AreEqual(nameof(_workerViewModel.HasChildren), calledPropertyName);
        }

        [Test]
        public void NotNotifiesIfChangedPropertyTheSame()
        {
            var wasCalled = false;
            _workerViewModel.PropertyChanged += (o, e) => wasCalled = true;

            _workerViewModel.Name = _workerViewModel.Name;
            Assert.IsFalse(wasCalled);

            _workerViewModel.Surname = _workerViewModel.Surname;
            Assert.IsFalse(wasCalled);

            _workerViewModel.Birthday = _workerViewModel.Birthday;
            Assert.IsFalse(wasCalled);

            _workerViewModel.Sex = _workerViewModel.Sex;
            Assert.IsFalse(wasCalled);

            _workerViewModel.HasChildren = _workerViewModel.HasChildren;
            Assert.IsFalse(wasCalled);
        }

        [Test]
        public void AllowsToSaveWithDefaultMockValues()
        {
            Assert.IsTrue(_workerViewModel.SaveCommand.CanExecute(null));

            var saveCalled = false;

            _worker.Setup(w => w.Save()).Callback(() => saveCalled = true);
            _workerViewModel.SaveCommand.Execute(null);

            Assert.IsTrue(saveCalled);
        }

        [Test]
        public void AllowsToCancelWithDefaultMockValues()
        {
            Assert.IsTrue(_workerViewModel.CancelCommand.CanExecute(null));
        }

        [Test]
        public void CancelsAllModifiedProperties()
        {
            _workerViewModel.Name = "Petya";
            _workerViewModel.Surname = "Petrov";

            _workerViewModel.CancelCommand.Execute(null);

            Assert.AreEqual("Vasya", _workerViewModel.Name);
            Assert.AreEqual("Ivanov", _workerViewModel.Surname);
        }

        [Test]
        public void NotAllowsToSaveIfNameAreInvalid()
        {
            _workerViewModel.Name = string.Empty;

            Assert.IsFalse(_workerViewModel.SaveCommand.CanExecute(null));
        }

        [Test]
        public void NotAllowsToSaveIfSurnameAreInvalid()
        {
            _workerViewModel.Surname = string.Empty;

            Assert.IsFalse(_workerViewModel.SaveCommand.CanExecute(null));
        }

        [Test]
        public void AllowsToSaveIfNameAreValid()
        {
            _workerViewModel.Name = "Grisha";

            Assert.IsTrue(_workerViewModel.SaveCommand.CanExecute(null));
        }

        [Test]
        public void AllowsToSaveIfSurnameAreValid()
        {
            _workerViewModel.Surname = "Sidorov";

            Assert.IsTrue(_workerViewModel.SaveCommand.CanExecute(null));
        }
    }
}
