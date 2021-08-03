using Links.ViewModels;
using Microsoft.Xaml.Behaviors;
using System.Windows.Controls;
using System.Windows.Input;

namespace Links.Infrastructure.Behaviors
{
    internal class LinkFilterTextBoxPreviewKeyDownBehavior : Behavior<TextBox>
    {
        protected override void OnAttached()
        {
            AssociatedObject.PreviewKeyDown += LinkFilterTextBoxPreviewKeyDown;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.PreviewKeyDown -= LinkFilterTextBoxPreviewKeyDown;
        }

        private void LinkFilterTextBoxPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (MainWindowViewModel.StringSaver.IsEmpty)
                return;

            switch (e.Key)
            {
                case Key.Up:
                    if (MainWindowViewModel.StringSaver.Current == AssociatedObject.Text)
                        MainWindowViewModel.StringSaver.DecreaseIndex();

                    AssociatedObject.Text = MainWindowViewModel.StringSaver.Current;
                    AssociatedObject.SelectionStart = AssociatedObject.Text.Length;
                    MainWindowViewModel.StringSaver.DecreaseIndex();
                    break;

                case Key.Down:
                    AssociatedObject.Text = MainWindowViewModel.StringSaver.Next();
                    AssociatedObject.SelectionStart = AssociatedObject.Text.Length;
                    break;
            }
        }
    }
}
