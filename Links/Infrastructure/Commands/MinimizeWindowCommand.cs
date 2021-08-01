using Links.Infrastructure.Commands.Base;
using System.Windows;

namespace Links.Infrastructure.Commands
{
    internal class MinimizeWindowCommand : Command
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
            SystemCommands.MinimizeWindow(window);
        }
    }
}
