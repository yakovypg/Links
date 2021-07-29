using System.Windows.Media;
using System.Collections.ObjectModel;

namespace Links.Models.Collections
{
    internal interface IGroup
    {
        string Name { get; set; }
        SolidColorBrush IconForeground { get; set; }
        ObservableCollection<LinkInfo> Links { get; }
    }
}
