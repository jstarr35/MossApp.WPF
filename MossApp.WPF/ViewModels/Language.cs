using Prism.Mvvm;

namespace MossApp.WPF.ViewModels
{

    public class Language : BindableBase
    {
        private string _name;
        private string _displayName;
        private string _extensions;
        private string _icon;
        private string _mapping;
        private bool _isSelected;
        private bool _isMossIcon;
        private bool _isMaterialIcon;
        private bool _isTextIcon;
        private IconType _iconType;

        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }

        public bool IsMossIcon
        {
            get => _isMossIcon;
            set => SetProperty(ref _isMossIcon, value);
        }

        public bool IsMaterialIcon
        {
            get => _isMaterialIcon;
            set => SetProperty(ref _isMaterialIcon, value);
        }

        public bool IsTextIcon
        {
            get => _isTextIcon;
            set => SetProperty(ref _isTextIcon, value);
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

        public string Mapping
        {
            get => _mapping;
            set => SetProperty(ref _mapping, value);
        }

        public string DisplayName
        {
            get => _displayName;
            set => SetProperty(ref _displayName, value);
        }

        public IconType IconType
        {
            get => _iconType;
            set
            {
                _ = SetProperty(ref _iconType, value);
                if (value == IconType.Material)
                {
                    _ = SetProperty(ref _isTextIcon, false);
                    _ = SetProperty(ref _isMossIcon, false);
                    _ = SetProperty(ref _isMaterialIcon, true);
                }
                else if (value == IconType.Moss)
                {
                    _ = SetProperty(ref _isTextIcon, false);
                    _ = SetProperty(ref _isMossIcon, true);
                    _ = SetProperty(ref _isMaterialIcon, false);
                }
                else if (value == IconType.Text)
                {
                    _ = SetProperty(ref _isTextIcon, true);
                    _ = SetProperty(ref _isMossIcon, false);
                    _ = SetProperty(ref _isMaterialIcon, false);
                }
              

            }
               
        }
    }

    public enum IconType
    {
        Material,
        Moss,
        Text
    }
  
}
