



using MossApp.Common;
using MossApp.Utilities;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System.Collections.Generic;

namespace MossApp.WPF.ViewModels
{
    public class Language : BindableBase
    {
        private string _name;
        private string _extensions;
        public string _icon;
        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                SetProperty(ref _isSelected, value);

            }
        }
        public string Extensions
        {
            get => _extensions;
            set => SetProperty(ref _extensions, value);
        }

        public string Icon
        {
            get => _icon;
            set => SetProperty(ref _icon, value);
        }


        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
    }
  
}
