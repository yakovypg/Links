using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Collections.ObjectModel;
using Links.Models.Localization;
using Links.Models.Collections;
using Links.Models.Themes;
using Links.ViewModels.Base;
using Links.Infrastructure.Commands;

namespace Links.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        private string _title = "Links";
        public string Title
        {
            get => _title;
            set => SetValue(ref _title, value);
        }

        private IWindowTheme _theme = WindowTheme.Dark;
        public IWindowTheme Theme
        {
            get => _theme;
            set => SetValue(ref _theme, value);
        }

        private ILocale _locale = Locale.English;
        public ILocale CurrentLocale
        {
            get => _locale;
            set => SetValue(ref _locale, value);
        }

        public ICommand MinimizeWindowCommand { get; }
        public ICommand MaximizeWindowCommand { get; }
        public ICommand CloseWindowCommand { get; }

        public ICommand ShowSettingsPageCommand { get; }
        public ICommand ShowGroupCreatorCommand { get; }
        public ICommand ShowLinkCreatorCommand { get; }

        public MainWindowViewModel()
        {
            MinimizeWindowCommand = new MinimizeWindowCommand();
            MaximizeWindowCommand = new MaximizeWindowCommand();
            CloseWindowCommand = new CloseWindowCommand();

            ShowSettingsPageCommand = new RelayCommand(delegate { }, t => true);
            ShowGroupCreatorCommand = new RelayCommand(delegate { }, t => true);
            ShowLinkCreatorCommand = new RelayCommand(delegate { }, t => true);
        }
    }
}
