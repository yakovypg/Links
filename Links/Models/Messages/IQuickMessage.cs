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

        public MessageBoxResult GetEmptyResult();
        public MessageBoxResult GetErrorResult();
        public MessageBoxResult GetWarningResult();
        public MessageBoxResult GetWarningResult(MessageBoxButton msgButton);
        public MessageBoxResult GetQuestionResult();
        public MessageBoxResult GetInformationResult();
    }
}
