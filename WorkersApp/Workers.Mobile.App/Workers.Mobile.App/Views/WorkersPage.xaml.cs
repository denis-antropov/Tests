using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Workers.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Workers.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorkersPage : ContentPage
    {
        public WorkersPage()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WorkerPage());
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var context = (WorkerListViewModel)((BindableObject)sender).BindingContext;
            context.EditWorkerCommand.Execute(null);
        }
    }
}