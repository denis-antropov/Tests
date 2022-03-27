namespace Workers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Threading;
    using Workers.Application;

    class Program
    {
        private static App _app;

        private event EventHandler ExitRequested;

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            Bootstrapper.Dispsose();
        }

        [STAThread]
        private static void Main()
        {
            _app = new App();
            _app.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            SynchronizationContext.SetSynchronizationContext(
               new DispatcherSynchronizationContext());

            var program = new Program();
            program.ExitRequested += Program_ExitRequested;
            var programTask = program.StartAsync();

            SafeStartup(programTask);
            _app.Run();
        }

        private static async void SafeStartup(Task task)
        {
            try
            {
                await task;
            }
            catch (Exception ex)
            {
                new UserInteraction().NotifyInformation(ex.Message);
                _app.Shutdown();
            }
        }

        private static void Program_ExitRequested(object sender, EventArgs e)
        {
            _app.Shutdown();
        }

        private async Task StartAsync()
        {
            var screen = new SplashScreen("SplashScreen.png");

            screen.Show(false);

            var viewModel = await Bootstrapper.GetWorkerListAsync();
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

        private void ViewModel_CloseRequested(object sender, EventArgs e)
        {
            Bootstrapper.Dispsose();
        }
    }
}
