using System;
using System.Windows.Input;
using Links.Models.Themes;
using Links.ViewModels.Base;
using Links.Infrastructure.Commands;

namespace Links.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        private string _title = "Links";
        public string Title { get => _title; set => SetValue(ref _title, value); }

        private IWindowTheme _theme = WindowTheme.Dark;
        public IWindowTheme Theme { get => _theme; set => SetValue(ref _theme, value); }

        public ICommand MinimizeWindowCommand { get; }
        public ICommand MaximizeWindowCommand { get; }
        public ICommand CloseWindowCommand { get; }

        public MainWindowViewModel()
        {
            MinimizeWindowCommand = new MinimizeWindowCommand();
            MaximizeWindowCommand = new MaximizeWindowCommand();
            CloseWindowCommand = new CloseWindowCommand();
        }
    }
}
