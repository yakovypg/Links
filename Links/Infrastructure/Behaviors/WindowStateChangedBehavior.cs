using System;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Controls;
using Microsoft.Xaml.Behaviors;

namespace Links.Infrastructure.Behaviors
{
    internal class WindowStateChangedBehavior : Behavior<MainWindow>
    {
        protected override void OnAttached()
        {
            AssociatedObject.SizeChanged += WindowStateChanged;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.SizeChanged -= WindowStateChanged;
        }

        private void WindowStateChanged(object sender, EventArgs e)
        {
            MainWindow window = AssociatedObject;
            IntPtr handle = new WindowInteropHelper(window).Handle;
            var containerBorder = window.Template.FindName("PART_Container", window) as Border;

            if (window.WindowState == WindowState.Maximized)
            {
                var screen = System.Windows.Forms.Screen.FromHandle(handle);

                if (screen.Primary)
                {
                    containerBorder.Padding = new Thickness(
                        SystemParameters.WorkArea.Left + 7,
                        SystemParameters.WorkArea.Top + 7,
                        SystemParameters.PrimaryScreenWidth - SystemParameters.WorkArea.Right + 7,
                        SystemParameters.PrimaryScreenHeight - SystemParameters.WorkArea.Bottom + 5);
                }
            }
            else
            {
                containerBorder.Padding = new Thickness(7, 7, 7, 5);
            }
        }
    }
}
