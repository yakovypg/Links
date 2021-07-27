using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using Microsoft.Xaml.Behaviors;
using Links.Infrastructure.Extensions.Windows;

namespace Links.Infrastructure.Behaviors
{
    internal class TitleMouseLeftButtonDownBehavior : Behavior<TextBlock>
    {
        protected override void OnAttached()
        {
            AssociatedObject.MouseLeftButtonDown += TitleMouseLeftButtonDown;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.MouseLeftButtonDown -= TitleMouseLeftButtonDown;
        }

        private void TitleMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!(AssociatedObject.GetParent(3) is Window window))
                return;
        }
    }
}
