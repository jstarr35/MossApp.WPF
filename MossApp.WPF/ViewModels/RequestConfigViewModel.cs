using MossApp.Utilities;
using MossApp.Utilities.Wrapper;
using System;
using System.Windows;
using MahApps.Metro.Controls;
using MossApp.WPF.Views.Windows;

using System.Collections.Generic;
using MossApp.Request;
using System.ComponentModel.DataAnnotations;
using MossApp.WPF.Views.Dialogs;
using MaterialDesignThemes.Wpf;
using System.Diagnostics;
using System.Collections.ObjectModel;
using MossApp.Utilities.Extensions;
using Prism.Events;
using MossApp.Core;

namespace MossApp.WPF.ViewModels
{
    using static MossApp.WPF.Properties.Settings;

    public class RequestConfigViewModel : BindableBase
    {

        public RelayCommand ShowOptionsFlyoutCommand { get; }
        public RelayCommand ShowSourceFilesFlyoutCommand { get; }
        public RelayCommand ShowBaseFileFlyoutCommand { get; }
        public RelayCommand OpenUserIdDialogCommand { get; set; }
        private ResourceDictionary DialogDictionary = new ResourceDictionary() { Source = new Uri("pack://application:,,,/MaterialDesignThemes.MahApps;component/Themes/MaterialDesignTheme.MahApps.Dialogs.xaml") };
        private int _sourceButtonZIndex = 1;
        public int SourceButtonZIndex
        {
            get => _sourceButtonZIndex;
            set => SetProperty(ref _sourceButtonZIndex, value);
        }

        private int _baseButtonZIndex;
        public int BaseButtonZIndex
        {
            get => _baseButtonZIndex;
            set => SetProperty(ref _baseButtonZIndex, value);
        }

        private List<string> SourceFileList { get; set; } = new List<string>();




        // private readonly IOpenMultipleFilesControlViewModel _filesControlViewModel;
        private IEventAggregator _ea;
        private IServiceProvider _provider;
        private IMossRequest _mossRequest;
        public RequestConfigViewModel(IMossRequest mossRequest, IEventAggregator ea)
        {
            _mossRequest = mossRequest;
            _ea = ea;
            _ea.GetEvent<FileSentEvent>().Subscribe(FileReceived, true);
            _ea.GetEvent<ToggleRestrictFileTypeEvent>().Subscribe(DependencyBuilt, true);
            ShowOptionsFlyoutCommand = new RelayCommand(_ => ShowOptionsFlyout());
            ShowSourceFilesFlyoutCommand = new RelayCommand(_ => ShowSourceFilesFlyout());
            ShowBaseFileFlyoutCommand = new RelayCommand(_ => ShowBaseFileFlyout());
            OpenUserIdDialogCommand = new RelayCommand(_ => OpenUserIdDialog());

            ParseLanguageSettings();
            InitializeRequest();
            //_filesControlViewModel = openMultipleFilesControlViewModel;
        }

        private void InitializeRequest()
        {
            // Read default values from setting file. 
            long setting = Default.UserId;
            UserId = setting == 0 ? 0 : setting;

            MaxMatches = Default.OptionM;
            NumberOfResultsToShow = Default.OptionN;

            Comments = Default.OptionC;
        }

        private void FileReceived(string file)
        {
            SourceFileList.Add(file);
        }

       

        public Flyout? OptionsFlyout { get; set; }
        public Flyout? SourceFilesFlyout { get; set; }

        private void ShowOptionsFlyout()
        {

            ((RequestConfigWindow)Application.Current.MainWindow).OptionsFlyout.IsOpen = !((RequestConfigWindow)Application.Current.MainWindow).OptionsFlyout.IsOpen;
        }

        private void ShowSourceFilesFlyout()
        {
            while (BaseButtonZIndex >= SourceButtonZIndex)
            {
                SourceButtonZIndex++;
                BaseButtonZIndex--;

            }
            ((RequestConfigWindow)Application.Current.MainWindow).SourceFilesFlyout.IsOpen = !((RequestConfigWindow)Application.Current.MainWindow).OptionsFlyout.IsOpen;
        }

        public void ShowBaseFileFlyout()
        {
            while (SourceButtonZIndex >= BaseButtonZIndex)
            {
                BaseButtonZIndex++;
                SourceButtonZIndex--;

            }
        }

        private async void OpenUserIdDialog()
        {
            //let's set up a little MVVM, cos that's what the cool kids are doing:
            var view = new SingleFieldDialog
            {

            };

            //show the dialog
            var result = await DialogHost.Show(view, "RootDialog", ClosingEventHandler);

            //check the result...
            Debug.WriteLine("Dialog was closed, the CommandParameter used to close it was: " + (result ?? "NULL"));
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
            => Debug.WriteLine("You can intercept the closing event, and cancel here.");


        /// <summary>
        /// All files command
        /// </summary>
        private const string AllFiles = "*.*";
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

        private List<string> _sourceFiles = new();
        public List<string> SourceFiles
        {
            get => _sourceFiles;
            set => SetProperty(ref _sourceFiles, value);
        }

        private List<string> _baseFiles = new();

        public List<string> BaseFiles
        {
            get => _baseFiles;
            set => SetProperty(ref _baseFiles, value);
        }

        private long _userId;
        [Required]
        public long UserId
        {
            get => _userId;
            set
            {
                SetProperty(ref _userId, value);
                _mossRequest.UserId = _userId;
            }
        }

        private int _port;
        public int Port
        {
            get => _port;
            set
            {
                SetProperty(ref _port, value);
                _mossRequest.Port = _port;
            }
        }

        private string _server;
        public string Server
        {
            get => _server;
            set
            {
                SetProperty(ref _server, value ?? string.Empty);
                _mossRequest.Server = _server;
            }
        }


        private int _maxMatches;

        public int MaxMatches
        {
            get => _maxMatches;
            set
            {
                SetProperty(ref _maxMatches, value);
                _mossRequest.MaxMatches = _maxMatches;
            }
        }

        private int _numberOfResultsToShow;
        public int NumberOfResultsToShow
        {
            get => _numberOfResultsToShow;
            set
            {
                SetProperty(ref _numberOfResultsToShow, value);
                _mossRequest.NumberOfResultsToShow = _numberOfResultsToShow;
            }
        }

        private string _comments;
        public string Comments
        {
            get => _comments;
            set
            {
                SetProperty(ref _comments, value ?? string.Empty);
                _mossRequest.Comments = _comments;
            }
        }

        private bool _isBetaRequest;
        public bool IsBetaRequest
        {
            get => _isBetaRequest;
            set
            {
                SetProperty(ref _isBetaRequest, value);
                _mossRequest.IsBetaRequest = _isBetaRequest;
            }
        }

        private void DependencyBuilt(bool isReady)
        {
            System.Collections.Specialized.StringCollection? languageList = Default.Languages;
            _ea.GetEvent<ConfigEvent>().Publish(languageList);
        }
        private void ParseLanguageSettings()
        {

           
           
        }
        //public override void Receive(string from, string message)
        //{
        //    if (from.Equals("OpenMultipleFilesControlViewModel"))
        //    {
        //        SourceFileList.Add(message);
        //    }
        //}
        



    }
    public class Language
    {
        public string Name { get; set; }
        public List<string> Extensions { get; set; } = new List<string>();
    }
}
