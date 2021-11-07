using MaterialDesignThemes.Wpf;
#nullable enable

using System.Windows;
using System.Windows.Media;


namespace MossApp.WPF.Resources.Themes
{
    public class ThemeResourceDictionary : ResourceDictionary, IMaterialDesignThemeDictionary
    {
        public ThemeResourceDictionary()
        {
            MergedDictionaries.Add(Theme.ResourceDictionary);
        }

        public BaseTheme? BaseTheme { get; set; }
        public Color? PrimaryColor { get; set; }
        public Color? SecondaryColor { get; set; }
        public ColorAdjustment? ColorAdjustment { get; set; }

        protected virtual void ApplyTheme(ITheme theme) { }
    }
}
