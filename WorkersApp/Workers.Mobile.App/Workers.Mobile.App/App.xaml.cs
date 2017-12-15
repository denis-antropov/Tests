using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Workers.BusinessLogic;
using Workers.DataLayer;
using Workers.ViewModels;
using Workers.ViewModels.Interfaces;
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
            page.BindingContext = GetWorkerList(page);

            MainPage = new NavigationPage(page);
        }

        /// <summary>
        /// Returns WorkerListViewModel instance
        /// </summary>
        /// <returns>WorkerListViewModel instance</returns>
        public WorkerListViewModel GetWorkerList(Page mainPage)
        {
            var databasePath = DependencyService.Get<IDbFilePathProvider>().GetFilePath();
            _repository = new WorkersRepository(databasePath);
            var workersService = new WorkerService(_repository);
            var workerModifier = new WorkerModifier(mainPage);
            var workerList = new WorkerListViewModel(workersService, workerModifier, new DetailedWorkerItemFactory());

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
            private readonly Page _mainPage;
            private IWorker _worker;

            public WorkerModifier(Page mainPage)
            {
                _mainPage = mainPage;
            }

            public event EventHandler<ModificationStateEventArgs> ModificationFinished;

            /// <summary>
            /// Modifies worker instance
            /// </summary>
            /// <param name="worker">Worker instance</param>
            /// <returns>True, if worker is modified and saved; otherwise - false</returns>
            public void Modify(IWorker worker)
            {
                _worker = worker;
                WorkerPage workerWindow = new WorkerPage();
                workerWindow.BindingContext = new WorkerViewModel(worker);
                workerWindow.Disappearing += WorkerPageDisappearing;

                var task = _mainPage.Navigation.PushAsync(workerWindow).GetAwaiter();
            }
 
            private void WorkerPageDisappearing(object sender, EventArgs e)
            {
                var workerPage = (WorkerPage)sender;
                workerPage.Disappearing -= WorkerPageDisappearing;

                ModificationFinished?.Invoke(
                    this, 
                    new ModificationStateEventArgs(workerPage.DialogResult, _worker));

            }
        }
    }
}
