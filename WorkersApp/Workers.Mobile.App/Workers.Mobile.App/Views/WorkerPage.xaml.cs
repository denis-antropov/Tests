using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Workers.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorkerPage : ContentPage
    {
        public WorkerPage()
        {
            InitializeComponent();

            DialogResult = false;
        }

        public bool DialogResult { get; private set; }

        /// <summary>
        /// Event handler of SaveButton
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">Event arguments</param>
        private async void SaveButton_Click(object sender, EventArgs e)
        {
            DialogResult = true;

            await Navigation.PopAsync();
        }

        /// <summary>
        /// Event handler of Cancel
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">Event arguments</param>
        private async void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = false;

            await Navigation.PopAsync();
        }
    }
}