using Links.Infrastructure.Extensions.Windows;
using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Links.Infrastructure.Behaviors
{
    internal class MovePlaceMouseWheelBehavior : Behavior<Grid>
    {
        protected override void OnAttached()
        {
            AssociatedObject.MouseWheel += MovePlaceMouseWheel;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.MouseWheel -= MovePlaceMouseWheel;
        }

        private void MovePlaceMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (!(AssociatedObject.GetParent(3) is Window window))
                return;

            double width = window.Width + e.Delta;
            double height = window.Height + e.Delta;

            if (height > SystemParameters.FullPrimaryScreenHeight)
                height = SystemParameters.FullPrimaryScreenHeight;
            if (height < window.MinHeight)
                height = window.MinHeight;

            if (width > SystemParameters.FullPrimaryScreenWidth)
                width = SystemParameters.FullPrimaryScreenWidth;
            if (width < window.MinWidth)
                width = window.MinWidth;

            window.Width = width;
            window.Height = height;
        }
    }
}
