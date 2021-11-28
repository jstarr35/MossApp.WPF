using Prism.Mvvm;
using System;
using System.Collections.Generic;

namespace MossApp.WPF.ViewModels
{

    public class Language : BindableBase
    {
        private string _name;
        private string _displayName;
        private string _extensionString;
        public List<string> Extensions { get; set; } = new List<string>();
        private string _icon;
        private string _mapping;



        public string Mapping
        {
            get => _mapping;
            set => SetProperty(ref _mapping, value);
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



        public string DisplayName
        {
            get => _displayName;
            set => SetProperty(ref _displayName, value);
        }

        public Language(string rawValues)
        {
            string[]? tokens = rawValues?.Split(',');

            if (tokens?.Length > 0)
            {
                DisplayName = tokens[0];
            }
            if (tokens?.Length > 1)
            {
                Mapping = tokens[1];
            }
            if (tokens?.Length > 2)
            {
                Name = tokens[2];
            }
            if (tokens?.Length > 3)
            {
               
                for (int index = 3; index < tokens?.Length; index++)
                {
                    Extensions.Add(tokens[index]);
                }
                ExtensionString = string.Join(", ", Extensions);
            }
        }


    }



}
