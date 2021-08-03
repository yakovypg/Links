using Links.Data.App;
using Links.ViewModels;
using Microsoft.Xaml.Behaviors;
using System.Windows.Input;

namespace Links.Infrastructure.Behaviors
{
    internal class WindowPreviewKeyDownBehavior : Behavior<MainWindow>
    {
        protected override void OnAttached()
        {
            AssociatedObject.PreviewKeyDown += WindowPreviewKeyDown;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.PreviewKeyDown -= WindowPreviewKeyDown;
        }

        private void WindowPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!(AssociatedObject.DataContext is MainWindowViewModel vm))
                return;

            switch (e.Key)
            {
                case Key.F1:
                    AppInfo.ShowInfo();
                    break;

                case Key.F6:
                    vm.ChangeGroupCreatorMenuVisibilityCommand?.Execute(AssociatedObject);
                    break;

                case Key.F7:
                    vm.ChangeLinkCreatorMenuVisibilityCommand?.Execute(AssociatedObject);
                    break;

                case Key.F10:
                    vm.ChangeSettingsFieldVisibilityCommand?.Execute(AssociatedObject);
                    break;

                case Key.F11:
                    vm.MaximizeWindowCommand?.Execute(AssociatedObject);
                    break;
            }
        }
    }
}
