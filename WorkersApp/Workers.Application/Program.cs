﻿namespace Workers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Threading;
    using Workers.Application;

    class Program
    {
        static App _app;

        /// <summary>
        /// Event handler of Exit event
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The args</param>
        private void Application_Exit(object sender, System.Windows.ExitEventArgs e)
        {
            Bootstrapper.Dispsose();
        }

        [STAThread]
        static void Main()
        {
            _app = new App();
            _app.ShutdownMode = System.Windows.ShutdownMode.OnExplicitShutdown;

            SynchronizationContext.SetSynchronizationContext(
               new DispatcherSynchronizationContext());

            var program = new Program();
            program.ExitRequested += program_ExitRequested;
            var programTask = program.StartAsync();

            HandleException(programTask);
            _app.Run();
        }

        static async void HandleException(Task task)
        {
            try
            {
                await task;
            }
            catch (Exception)
            {
                // фиксируем исключение
                // показываем ошибку пользователю
                _app.Shutdown();
            }
        }

        static void program_ExitRequested(object sender, EventArgs e)
        {
            _app.Shutdown();
        }

        public event EventHandler ExitRequested;

        public async Task StartAsync()
        {
            SplashScreen screen = new SplashScreen("SplashScreen.png");

            screen.Show(false);

            var viewModel = await Bootstrapper.GetWorkerListAsync();
            // viewModel.CloseRequested += viewModel_CloseRequested;
            // await viewModel.InitializeAsync();

            var mainWindow = new MainWindow();
            mainWindow.DataContext = viewModel;
            mainWindow.Closed += (s, e) =>
            {
                // viewModel.RequestClose();
                // Instead of Shutdown we can use RequestClose
                ExitRequested?.Invoke(this, EventArgs.Empty);
            };

            screen.Close(TimeSpan.FromMilliseconds(0));
            mainWindow.Show();
        }

        private void viewModel_CloseRequested(object sender, EventArgs e)
        {
            Bootstrapper.Dispsose();
        }
    }
}
