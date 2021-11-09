using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MossApp.Core;
using System.Collections.Generic;
using MossApp.Utilities.Extensions;
using System.Collections.Specialized;

namespace MossApp.Modules.Request.ViewModels
{
    public class SourceFileListViewModel : BindableBase
    {
        private IEventAggregator _ea;

        /// <summary>
        /// Gets or sets the languages.
        /// </summary>
        /// <value>
        /// The languages and their associated file extensions.
        /// </value>
        private ObservableCollection<Language> _languages = new ObservableCollection<Language>();

        public ObservableCollection<Language> Languages
        {
            get => _languages;
            set => SetProperty(ref _languages, value);

        }

        private Language _selectedLanguage = new Language();

        public Language SelectedLanguage
        {
            get => _selectedLanguage;
            set
            {
                SetProperty(ref _selectedLanguage, value);
                RestrictedFileTypesInput = value.Extensions.ToExtensionString();
            }
}
private string _restrictedFileTypesInput;

        public string RestrictedFileTypesInput
        {
            get => _restrictedFileTypesInput;
            set => SetProperty(ref _restrictedFileTypesInput, value);
        }


        private bool _restrictFileTypes;

        public bool RestrictFileTypes
        {
            get => _restrictFileTypes;
            set => SetProperty(ref _restrictFileTypes, value);
        }

        private ObservableCollection<string> _files;
        public ObservableCollection<string> Files
        {
            get => _files;
            set => SetProperty(ref _files, value);
        }
       
        private bool _isLoading;

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        public DelegateCommand SendRequestCommand { get; set; }

        public DelegateCommand RestrictFileTypesChangedCommand { get; set; }

        public SourceFileListViewModel(IEventAggregator ea)
        {
            _ea = ea;
            SendRequestCommand = new DelegateCommand(SendRequest);
            Files = new ObservableCollection<string>();
            
            _ea.GetEvent<FileSentEvent>().Subscribe(FileReceived, true);
            _ea.GetEvent<ConfigEvent>().Subscribe(GetLanguages, true);
            _ea.GetEvent<ToggleRestrictFileTypeEvent>().Publish(!RestrictFileTypes);
        }

        private void ToggleRestrictFileTypes()
        {
            _ea.GetEvent<ToggleRestrictFileTypeEvent>().Publish(!RestrictFileTypes);
        }

        private void FileReceived(string file)
        {
            if (!Files.Contains(file))
            {
                Files.Add(file);
            }
        }

        private async void SendRequest()
        {
            IsLoading = true;

            await Task.Delay(1500);
            await Task.Run(async () => { });
            IsLoading = false;
        }

        /// <summary>
        /// Parses the language settings into the Language Dictionary of
        /// Languages => extensions.
        /// </summary>
       private void GetLanguages(StringCollection stringCollection)
        {
            foreach (string? language in stringCollection)
            {
                Language temp = new Language();

                string[]? tokens = language?.Split(',');

                if (tokens?.Length > 0)
                {
                    temp.Name = tokens[0];
                }

                for (int index = 1; index < tokens?.Length; index++)
                {
                    temp.Extensions.Add(tokens[index]);
                }

                Languages.Add(temp);

                
                // because this setting is created with a specific format, this 
                // should never fail. If it does, the underlying data source needs to be 
                // fixed. 
            }

        }
    }

    public class Language
    {
        public string Name { get; set; }
        public List<string> Extensions { get; set; } = new List<string>();
    }
}
