using Microsoft.JSInterop;
using PlannerModel.Enums;

namespace BudgetPlanner.Services
{
    public class ThemeService(IJSRuntime js)
    {
        private readonly IJSRuntime _js = js;

        public ThemeModeEnum CurrentTheme { get; private set; }
        public event Action? OnThemeChanged;

        private bool _userHasChosenTheme;

        public async Task InitializeAsync()
        {
            var savedTheme = await _js.InvokeAsync<string>("localStorage.getItem", "theme");

            if (!string.IsNullOrEmpty(savedTheme) && 
                Enum.TryParse<ThemeModeEnum>(savedTheme, out var stored))
            {
                SetTheme(stored);
                return;
            }

            var isDark = await _js.InvokeAsync<bool>("themeHelper.isDarkMode");
            SetTheme(isDark ? ThemeModeEnum.Dark : ThemeModeEnum.Light, false);
        }

        public async Task LoadAsync()
        {
            var theme = await _js.InvokeAsync<string>("localStorage.getItem", "theme");
            if (Enum.TryParse<ThemeModeEnum>(theme, out var result))
                SetTheme(result);
        }

        public async Task SaveAsync() => await _js.InvokeVoidAsync("localStorage.setItem", "theme", CurrentTheme.ToString());

        public void SetTheme(ThemeModeEnum theme, bool save = true)
        {
            if (CurrentTheme == theme)
                return;

            CurrentTheme = theme;

            if (save)
            {
                _userHasChosenTheme = true;
                _ = SaveAsync();
            }

            OnThemeChanged?.Invoke();
        }

        [JSInvokable]
        public void OnSystemThemeChanged(bool isDark)
        {
            if (_userHasChosenTheme)
                return;

            SetTheme(isDark ? ThemeModeEnum.Dark : ThemeModeEnum.Light, false);
        }
    }
}
