﻿using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Workers.Application
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
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
            catch (Exception ex)
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
    }
}
