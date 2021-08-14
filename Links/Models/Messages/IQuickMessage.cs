using Links.Models.Localization;
using System.Windows;

namespace Links.Models.Messages
{
    internal interface IQuickMessage
    {
        public ILocale MessageLocale { get; }
        public string Message { get; set; }

        public void ShowEmpty();
        public void ShowError();
        public void ShowWarning();
        public void ShowQuestion();
        public void ShowInformation();

        public MessageBoxResult GetEmptyResult(MessageBoxButton msgButton = MessageBoxButton.OK);
        public MessageBoxResult GetErrorResult(MessageBoxButton msgButton = MessageBoxButton.OK);
        public MessageBoxResult GetWarningResult(MessageBoxButton msgButton = MessageBoxButton.OK);
        public MessageBoxResult GetQuestionResult(MessageBoxButton msgButton = MessageBoxButton.OK);
        public MessageBoxResult GetInformationResult(MessageBoxButton msgButton = MessageBoxButton.OK);
    }
}
