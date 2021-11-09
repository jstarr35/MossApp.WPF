using System;
using System.Windows;
using System.Windows.Media;


namespace MossApp.WPF.Resources.Themes
{
    public sealed class Theme
    {
        [ThreadStatic]
        private static ResourceDictionary resourceDictionary;

        internal static ResourceDictionary ResourceDictionary
        {
            get
            {
                if (resourceDictionary != null)
                {
                    return resourceDictionary;
                }

                resourceDictionary = new ResourceDictionary();
                LoadThemeType(ThemeType.Dark);
                return resourceDictionary;
            }
        }
        public static ThemeType ThemeType { get; set; } = ThemeType.Dark;

        public static void LoadThemeType(ThemeType type)
        {
            ThemeType = type;

            SetResource(ThemeResourceKey.PrimaryColor.ToString(), new SolidColorBrush(ColorFromHex("#FF8C1515")));
            SetResource(ThemeResourceKey.SecondaryColor.ToString(), new SolidColorBrush(ColorFromHex("#175e54")));

        }

        public static object GetResource(ThemeResourceKey resourceKey)
        {
            return ResourceDictionary.Contains(resourceKey.ToString()) ? ResourceDictionary[resourceKey.ToString()] : null;
        }

        internal static void SetResource(object key, object resource)
        {
            ResourceDictionary[key] = resource;
        }

        internal static Color ColorFromHex(string colorHex)
        {
            return (Color?)ColorConverter.ConvertFromString(colorHex) ?? Colors.Transparent;
        }
    }
}
