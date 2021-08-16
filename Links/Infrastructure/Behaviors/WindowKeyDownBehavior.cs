using Links.ViewModels;
using Microsoft.Xaml.Behaviors;
using System.Windows.Input;

namespace Links.Infrastructure.Behaviors
{
    internal class WindowKeyDownBehavior : Behavior<MainWindow>
    {
        protected override void OnAttached()
        {
            AssociatedObject.KeyDown += WindowKeyDown;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.KeyDown -= WindowKeyDown;
        }

        private void WindowKeyDown(object sender, KeyEventArgs e)
        {
            if (!(AssociatedObject.DataContext is MainWindowViewModel vm))
                return;

            switch (e.Key)
            {
                case Key.LeftShift:
                    if (vm.SettingsFieldVisibility == System.Windows.Visibility.Hidden)
                    {
                        vm.LinkCollectionVM.CheckAllLinksCommand?.Execute(null);
                    }
                    else
                    {
                        vm.SettingsVM.CheckAllLinksCommand?.Execute(null);
                    }
                    break;

                case Key.RightShift:
                    if (vm.SettingsFieldVisibility == System.Windows.Visibility.Hidden)
                    {
                        vm.LinkCollectionVM.UncheckAllLinksCommand?.Execute(null);
                    }
                    else
                    {
                        vm.SettingsVM.UncheckAllLinksCommand?.Execute(null);
                    }
                    break;

                case Key.Tab:
                    if (vm.SettingsFieldVisibility == System.Windows.Visibility.Hidden)
                    {
                        vm.LinkCollectionVM.ChangeLinksMoveMenuVisibilityCommand?.Execute(null);
                    }
                    break;

                case Key.Delete:
                    if (vm.SettingsFieldVisibility == System.Windows.Visibility.Hidden)
                    {
                        vm.LinkCollectionVM.DeleteSelectedLinksCommand?.Execute(null);
                    }
                    break;
            }
        }
    }
}

