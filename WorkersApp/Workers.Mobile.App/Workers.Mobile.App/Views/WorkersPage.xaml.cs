using System;
using System.Threading.Tasks;
using Workers.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using z3r0.Utils;

namespace Workers.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorkersPage : ContentPage, IUserInteraction
    {
        public WorkersPage()
        {
            InitializeComponent();
        }

        public void NotifyError(string caption, string text)
        {
            throw new NotImplementedException();
        }

        public void NotifyError(string text)
        {
            throw new NotImplementedException();
        }

        public void NotifyError(string caption, string text, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void NotifyError(string caption, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void NotifyError(Exception exception)
        {
            throw new NotImplementedException();
        }

        public void NotifyInformation(string caption, string text)
        {
            throw new NotImplementedException();
        }

        public void NotifyInformation(string text)
        {
            throw new NotImplementedException();
        }

        public UserAnswer NotifyQuestion(string caption, string text, UserOptions options)
        {
            throw new NotImplementedException();
        }

        public UserAnswer NotifyQuestion(string text, UserOptions options)
        {
            throw new NotImplementedException();
        }

        public async Task<UserAnswer> NotifyQuestionAsync(string caption, string text, UserOptions options)
        {
            try
            {
                var result = await DisplayAlert(caption, text, "Yes", "No");
                return result ? UserAnswer.Yes : UserAnswer.No;
            }
            catch (Exception)
            {
                
            }

            return UserAnswer.No;
        }

        public async Task<UserAnswer> NotifyQuestionAsync(string text, UserOptions options)
        {
            return await NotifyQuestionAsync(string.Empty, text, options);
        }

        public void NotifyWarning(string caption, string text)
        {
            throw new NotImplementedException();
        }

        public void NotifyWarning(string text)
        {
            throw new NotImplementedException();
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