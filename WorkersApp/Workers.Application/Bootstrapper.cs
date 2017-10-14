namespace Workers
{
    using System;
    using Workers.BusinessLogic;
    using Workers.BusinessLogic.Interfaces;
    using Workers.DataLayer;
    using Workers.ViewModels;
    using Workers.Views;

    /// <summary>
    /// Represents a bootstrapper of application
    /// </summary>
    public static class Bootstrapper
    {
        /// <summary>
        /// Instance of repository
        /// </summary>
        private static WorkersRepository _repository; 

        /// <summary>
        /// Returns WorkerListViewModel instance
        /// </summary>
        /// <returns>WorkerListViewModel instance</returns>
        public static WorkerListViewModel GetWorkerList()
        {
            _repository = new WorkersRepository();
            var workersService = new WorkerService(_repository);
            var workerModifier = new WorkerModifier();
            var workerList = new WorkerListViewModel(workersService, workerModifier);

            return workerList;
        }

        /// <summary>
        /// Disposes repository
        /// </summary>
        internal static void Dispsose()
        {
            if(_repository != null)
            {
                _repository.Dispose();
            }
        }

        /// <summary>
        /// Simple implementation of worker modified, based on WorkerWindow
        /// </summary>
        private class WorkerModifier : IWorkerModifier
        {
            /// <summary>
            /// Modifies worker instance
            /// </summary>
            /// <param name="worker">Worker instance</param>
            /// <returns>True, if worker is modified and saved; otherwise - false</returns>
            public bool Modify(IWorker worker)
            {
                WorkerWindow workerWindow = new WorkerWindow();
                workerWindow.DataContext = new WorkerViewModel(worker);

                var dialogResult = workerWindow.ShowDialog();

                return dialogResult.HasValue && dialogResult.Value;
            }
        }
    }
}
