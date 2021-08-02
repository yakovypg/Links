using System.Windows.Media;

namespace Links.Models.Collections
{
    internal interface IGroupIcon
    {
        string DisplayName { get; }
        Brush Foreground { get; }
        int ColorIndex { get; set; }
    }
}
