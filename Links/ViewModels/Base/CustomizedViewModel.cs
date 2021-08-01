using Links.Models.Localization;
using Links.Models.Themes;

namespace Links.ViewModels.Base
{
    internal class CustomizedViewModel : ViewModel
    {
        protected IWindowTheme _theme = WindowTheme.Dark;
        public IWindowTheme Theme => _theme;

        protected ILocale _locale = Locale.English;
        public ILocale CurrentLocale => _locale;

        protected int _linkPresenterGridWidth = 150;
        protected int _linkPresenterGridHeight = 230;

        protected int _maxLinkPresenterGridWidth = 270;
        protected int _maxLinkPresenterGridHeight = 414;
    }
}
