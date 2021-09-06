using Links.Models.Localization;
using System.Windows;

namespace Links.Models.Messages
{
    internal interface IQuickMessage
    {
        ILocale MessageLocale { get; }
        string Message { get; set; }

        void ShowEmpty();
        void ShowError();
        void ShowWarning();
        void ShowQuestion();
        void ShowInformation();

        MessageBoxResult GetEmptyResult(MessageBoxButton msgButton = MessageBoxButton.OK);
        MessageBoxResult GetErrorResult(MessageBoxButton msgButton = MessageBoxButton.OK);
        MessageBoxResult GetWarningResult(MessageBoxButton msgButton = MessageBoxButton.OK);
        MessageBoxResult GetQuestionResult(MessageBoxButton msgButton = MessageBoxButton.YesNo);
        MessageBoxResult GetInformationResult(MessageBoxButton msgButton = MessageBoxButton.OK);
    }
}
