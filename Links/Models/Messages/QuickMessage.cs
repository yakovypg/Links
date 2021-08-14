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

        public MessageBoxResult GetEmptyResult(MessageBoxButton msgButton = MessageBoxButton.OK)
        {
            return MessageBox.Show(Message, string.Empty, msgButton, MessageBoxImage.None);
        }

        public MessageBoxResult GetErrorResult(MessageBoxButton msgButton = MessageBoxButton.OK)
        {
            return MessageBox.Show(Message, MessageLocale.Error, msgButton, MessageBoxImage.Error);
        }

        public MessageBoxResult GetWarningResult(MessageBoxButton msgButton = MessageBoxButton.OK)
        {
            return MessageBox.Show(Message, MessageLocale.Warning, msgButton, MessageBoxImage.Warning);
        }

        public MessageBoxResult GetQuestionResult(MessageBoxButton msgButton = MessageBoxButton.YesNo)
        {
            return MessageBox.Show(Message, MessageLocale.Question, msgButton, MessageBoxImage.Question);
        }

        public MessageBoxResult GetInformationResult(MessageBoxButton msgButton = MessageBoxButton.OK)
        {
            return MessageBox.Show(Message, MessageLocale.Information, msgButton, MessageBoxImage.Information);
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
