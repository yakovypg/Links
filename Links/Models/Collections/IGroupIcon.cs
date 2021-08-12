using System.Windows.Media;

namespace Links.Models.Collections
{
    internal interface IGroupIcon
    {
        string DisplayName { get; }
        string ColorName { get; }

        Brush Foreground { get; }
        GroupIcon.Colors ForegroundColor { get; set; }
        int ColorIndex { get; set; }
    }
}
