using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Threading.Tasks;
using Microsoft.Xaml.Behaviors;
using Links.Infrastructure.Extensions.Windows;

namespace Links.Infrastructure.Behaviors
{
    internal class MovePlaceMouseLeftButtonDownBehavior : Behavior<Grid>
    {
        private DateTime lastMouseLeftButtonDownTime = new DateTime(1, 1, 1);

        protected override void OnAttached()
        {
            AssociatedObject.MouseLeftButtonDown += MovePlaceMouseLeftButtonDown;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.MouseLeftButtonDown -= MovePlaceMouseLeftButtonDown;
        }

        private void MovePlaceMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!(AssociatedObject.GetParent(3) is Window window))
                return;

            DateTime currentTime = DateTime.Now;

            if (currentTime.Subtract(lastMouseLeftButtonDownTime).TotalMilliseconds <= 300)
            {
                if (window.WindowState == WindowState.Maximized)
                    SystemCommands.RestoreWindow(window);
                else
                    SystemCommands.MaximizeWindow(window);

                lastMouseLeftButtonDownTime = currentTime;
                return;
            }

            lastMouseLeftButtonDownTime = currentTime;

            if (window.ActualHeight == SystemParameters.PrimaryScreenHeight + 14)
            {
                var firstCursorPos = System.Windows.Forms.Cursor.Position;
                var mouse = Mouse.PrimaryDevice;

                _ = Task.Run(delegate
                {
                    while (mouse.LeftButton == MouseButtonState.Pressed)
                    {
                        System.Threading.Thread.Sleep(40);
                        var secondCursorPos = System.Windows.Forms.Cursor.Position;

                        if (!firstCursorPos.Equals(secondCursorPos))
                        {
                            window.Dispatcher.Invoke(delegate
                            {
                                int y = (int)(window.Left + window.Width / 2) + 7;

                                window.Top = 0;
                                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(y, 16);
                                window.WindowState = WindowState.Normal;

                                _ = Task.Run(() => window.Dispatcher.Invoke(() => window.DragMove()));
                            });

                            break;
                        }
                    }
                });
            }
            else
            {
                window.DragMove();
            }
        }
    }
}
