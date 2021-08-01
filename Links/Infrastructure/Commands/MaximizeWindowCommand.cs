using Links.Infrastructure.Commands.Base;
using System.Windows;

namespace Links.Infrastructure.Commands
{
    internal class MaximizeWindowCommand : Command
    {
        public override bool CanExecute(object parameter)
        {
            return parameter is Window;
        }

        public override void Execute(object parameter)
        {
            if (!CanExecute(parameter))
                return;

            var window = parameter as Window;

            if (window.WindowState == WindowState.Maximized)
                SystemCommands.RestoreWindow(window);
            else
                SystemCommands.MaximizeWindow(window);
        }
    }
}
