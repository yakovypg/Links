using Links.Infrastructure.Commands;
using Links.Models.Collections;
using Links.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Links.ViewModels
{
    internal class SettingsViewModel : CustomizedViewModel
    {
        public int LinkPresenterGridWidth
        {
            get => _linkPresenterGridWidth;
            set => SetValue(ref _linkPresenterGridWidth, value);
        }

        public int LinkPresenterGridHeight
        {
            get => _linkPresenterGridHeight;
            set => SetValue(ref _linkPresenterGridHeight, value);
        }

        #region Commands

        public ICommand ChangeGroupsSortingCommand { get; }

        #endregion

        #region ButtonCommands

        public ICommand CloseSettingsPageCommand { get; }
        public ICommand ResetSettingsCommand { get; }
        public ICommand ImportSettingsCommand { get; }
        public ICommand ExportSettingsCommand { get; }
        public ICommand RestoreRecycleBinItemCommand { get; }
        public ICommand RemoveRecycleBinItemCommand { get; }
        public ICommand EmptyRecycleBinCommand { get; }

        #endregion

        public ObservableCollection<Group> RecycleBin { get; private set; }

        public SettingsViewModel()
        {
            ChangeGroupsSortingCommand = new RelayCommand(delegate { }, t => true);

            CloseSettingsPageCommand = new RelayCommand(delegate { }, t => true);
            ResetSettingsCommand = new RelayCommand(delegate { }, t => true);
            ImportSettingsCommand = new RelayCommand(delegate { }, t => true);
            ExportSettingsCommand = new RelayCommand(delegate { }, t => true);
            RestoreRecycleBinItemCommand = new RelayCommand(delegate { }, t => true);
            RemoveRecycleBinItemCommand = new RelayCommand(delegate { }, t => true);
            EmptyRecycleBinCommand = new RelayCommand(delegate { }, t => true);
        }
    }
}
