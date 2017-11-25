using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Workers.BusinessLogic;
using Workers.DataLayer;
using Workers.ViewModels;
using Workers.Views;
using Xamarin.Forms;

namespace Workers.Mobile.App
{
    public partial class App : Application
    {
        /// <summary>
        /// Instance of repository
        /// </summary>
        private static WorkersDataAccess _repository;

        public App()
        {
            InitializeComponent();

            WorkersPage page = new WorkersPage();
            page.BindingContext = GetWorkerList();

            MainPage = new NavigationPage(page);
        }

        /// <summary>
        /// Returns WorkerListViewModel instance
        /// </summary>
        /// <returns>WorkerListViewModel instance</returns>
        public static WorkerListViewModel GetWorkerList()
        {
            _repository = new WorkersDataAccess();
            var workersService = new WorkerService(_repository);
            var workerModifier = new WorkerModifier();
            var workerList = new WorkerListViewModel(workersService, workerModifier);

            return workerList;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
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
                //WorkerPage workerWindow = new WorkerPage();
                //workerWindow.BindingContext = new WorkerViewModel(worker);

                //Application.Current.MainPage.Navigation.PushAsync(workerWindow);

                return false;
            }
        }
    }
}
