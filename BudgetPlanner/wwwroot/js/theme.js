window.themeHelper = {
    isDarkMode: function () {
        return window.matchMedia('(prefers-color-schema: dark)').matches;
    }
};

window.themeHelper.watch = function (dotnetRef) {
    window.matchMedia('(prefers-color-schema: dark)')
        .addEventListener('change', e => {
            dotnetRef.invokeMethodAsync('OnSystemThemeChanged', e.matches);
        });
};