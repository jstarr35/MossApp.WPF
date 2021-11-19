


using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System.Collections.Generic;

namespace MossApp.Core.Models
{
    public class Language : BindableBase
    {
        private string? _name;
        private string _extensionString;
        private string _icon;
        private bool _isSelected;

        private string? _description;
        private char _code;
        private double _numeric;


        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }

        public char Code
        {
            get => _code;
            set => SetProperty(ref _code, value);
        }

        public string? Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public double Numeric
        {
            get => _numeric;
            set => SetProperty(ref _numeric, value);
        }
        public string ExtensionString
        {
            get => _extensionString;
            set => SetProperty(ref _extensionString, value);
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
        public List<string> Extensions { get; set; } = new List<string>();

        public RelayCommand LanguageSelectedCommand;
        private IEventAggregator _ea;
        public Language(IEventAggregator ea)
        {
            _ea = ea;
            LanguageSelectedCommand = new RelayCommand(SetLanguage);
        }

        private void SetLanguage(object language)
        {
            _ea.GetEvent<LanguageSetEvent>().Publish(language as Language);
        }
    }
}
