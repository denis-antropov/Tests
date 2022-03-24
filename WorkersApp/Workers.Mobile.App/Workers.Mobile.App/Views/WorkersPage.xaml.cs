namespace Workers.Views
{
    using System;
    using System.Threading.Tasks;
    using Workers.ViewModels;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    using z3r0.Utils;

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

    public class UserInteractionBasedOnPage : IUserInteraction
    {
        private readonly Page _sourcePage;

        public UserInteractionBasedOnPage(Page sourcePage)
        {
            _sourcePage = sourcePage;
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
            UserAnswer answer = UserAnswer.Cancel;
            bool result;

            switch (options)
            {
                case UserOptions.OkCancel:
                    result = await _sourcePage.DisplayAlert(caption, text, "Ok", "Cancel");
                    answer = result ? UserAnswer.Ok : UserAnswer.Cancel;
                    break;
                case UserOptions.YesNo:
                    result = await _sourcePage.DisplayAlert(caption, text, "Yes", "No");
                    answer = result ? UserAnswer.Yes : UserAnswer.No;
                    break;
                case UserOptions.YesNoCancel:
                    throw new NotSupportedException();
            }

            return answer;
        }

        public async Task<UserAnswer> NotifyQuestionAsync(string text, UserOptions options)
        {
            return await NotifyQuestionAsync("Question", text, options);
        }

        public void NotifyWarning(string caption, string text)
        {
            throw new NotImplementedException();
        }

        public void NotifyWarning(string text)
        {
            throw new NotImplementedException();
        }
    }
}