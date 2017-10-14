namespace Workers.Application
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        /// <summary>
        /// Event handler of Exit event
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The args</param>
        private void Application_Exit(object sender, System.Windows.ExitEventArgs e)
        {
            Bootstrapper.Dispsose();
        }
    }
}
