namespace Workers.Views
{
    using System.Windows;

    /// <summary>
    /// Interaction logic for WorkerControl.xaml
    /// </summary>
    public partial class WorkerWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the WorkerWindow class
        /// </summary>
        public WorkerWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Event handler of SaveButton
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">Event arguments</param>
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        /// <summary>
        /// Event handler of Cancel
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">Event arguments</param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
