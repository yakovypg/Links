using System;
using System.Windows;

namespace Links.Infrastructure.Extensions.Windows
{
    internal static class FrameworkElementExtensions
    {
        public static object GetParent(this FrameworkElement sender, int depth = 1)
        {
            if (depth < 1)
                throw new ArgumentException("The depth must be greater than zero.");

            DependencyObject parent = sender.Parent;

            for (int i = 1; i < depth; ++i)
                parent = (parent as FrameworkElement).Parent;

            return parent;
        }
    }
}
