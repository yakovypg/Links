using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Links.Models.Collections.Creators
{
    internal sealed class GroupCreator : IGroupCreator, INotifyPropertyChanged
    {
        private string _name = string.Empty;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private GroupIcon _icon = new GroupIcon();
        public GroupIcon Icon
        {
            get => _icon;
            set
            {
                _icon = value;
                OnPropertyChanged(nameof(Icon));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public Group CreateGroup()
        {
            string name = Name;
            GroupIcon icon = Icon;

            Name = string.Empty;
            Icon = new GroupIcon();

            return new Group(name, icon, null);
        }
    }
}
