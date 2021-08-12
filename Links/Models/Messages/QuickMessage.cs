using Links.Models.Localization;
using System.Windows;

namespace Links.Models.Messages
{
    internal class QuickMessage : IQuickMessage
    {
        public ILocale MessageLocale { get; }

        private string _message;
        public string Message
        {
            get => _message;
            set => _message = value ?? string.Empty;
        }

        public QuickMessage(string message = null, ILocale locale = null)
        {
            Message = message;
            MessageLocale = locale ?? Locale.English;
        }

        public MessageBoxResult GetEmptyResult()
        {
            return MessageBox.Show(Message, string.Empty, MessageBoxButton.OK, MessageBoxImage.None);
        }

        public MessageBoxResult GetErrorResult()
        {
            return MessageBox.Show(Message, MessageLocale.Error, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public MessageBoxResult GetWarningResult()
        {
            return MessageBox.Show(Message, MessageLocale.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        public MessageBoxResult GetWarningResult(MessageBoxButton msgButton)
        {
            return MessageBox.Show(Message, MessageLocale.Warning, msgButton, MessageBoxImage.Warning);
        }

        public MessageBoxResult GetQuestionResult()
        {
            return MessageBox.Show(Message, MessageLocale.Question, MessageBoxButton.YesNo, MessageBoxImage.Question);
        }

        public MessageBoxResult GetInformationResult()
        {
            return MessageBox.Show(Message, MessageLocale.Information, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void ShowEmpty()
        {
            GetEmptyResult();
        }

        public void ShowError()
        {
            GetErrorResult();
        }

        public void ShowWarning()
        {
            GetWarningResult();
        }

        public void ShowQuestion()
        {
            GetQuestionResult();
        }

        public void ShowInformation()
        {
            GetInformationResult();
        }
    }
}
