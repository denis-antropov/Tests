namespace Workers
{
    using System;
    using System.Threading.Tasks;
    using System.Windows;
    using z3r0.Utils;

    internal class UserInteraction : IUserInteraction
    {
        public void NotifyError(string caption, string text)
        {
            MessageBox.Show(text, caption);
        }

        public void NotifyError(string text)
        {
            MessageBox.Show(text);
        }

        public void NotifyError(string caption, string text, Exception exception)
        {
            MessageBox.Show(text, caption);
        }

        public void NotifyError(string caption, Exception exception)
        {
            MessageBox.Show(exception.Message, caption);
        }

        public void NotifyError(Exception exception)
        {
            MessageBox.Show(exception.Message);
        }

        public void NotifyInformation(string caption, string text)
        {
            MessageBox.Show(text, caption);
        }

        public void NotifyInformation(string text)
        {
            MessageBox.Show(text);
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
            var answer = UserAnswer.Cancel;
            bool result;

            switch (options)
            {
                case UserOptions.OkCancel:
                    result = MessageBox.Show(text, caption, MessageBoxButton.OKCancel) == MessageBoxResult.OK;
                    answer = result ? UserAnswer.Ok : UserAnswer.Cancel;
                    break;
                case UserOptions.YesNo:
                    result = MessageBox.Show(text, caption, MessageBoxButton.YesNo) == MessageBoxResult.Yes;
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
            MessageBox.Show(text, caption);
        }

        public void NotifyWarning(string text)
        {
            MessageBox.Show(text);
        }
    }
}
