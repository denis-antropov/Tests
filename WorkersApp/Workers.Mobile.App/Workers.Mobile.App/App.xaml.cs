using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Workers.BusinessLogic;
using Workers.DataLayer;
using Workers.ViewModels;
using Workers.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Workers.Mobile.App
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class App : Application
    {
        /// <summary>
        /// Instance of repository
        /// </summary>
        private WorkersRepository _repository;

        public App()
        {
            WorkersPage page = new WorkersPage();
            page.BindingContext = GetWorkerList();

            MainPage = new NavigationPage(page);
        }

        /// <summary>
        /// Returns WorkerListViewModel instance
        /// </summary>
        /// <returns>WorkerListViewModel instance</returns>
        public WorkerListViewModel GetWorkerList()
        {
            var databasePath = DependencyService.Get<IDbFilePathProvider>().GetFilePath();
            _repository = new WorkersRepository(databasePath);
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
