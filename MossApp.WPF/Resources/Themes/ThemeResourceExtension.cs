using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MossApp.WPF.Resources.Themes
{
    public sealed class ThemeResourceExtension : DynamicResourceExtension
    {
        public new ThemeResourceKey ResourceKey
        {
            get
            {
                Enum.TryParse(base.ResourceKey.ToString(), out ThemeResourceKey resourceKey);
                return resourceKey;
            }
            set => base.ResourceKey = value.ToString();
        }
    }
}
