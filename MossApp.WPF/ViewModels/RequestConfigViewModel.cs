using HtmlAgilityPack;
using MahApps.Metro.Controls;
using MaterialDesignThemes.Wpf;
using MossApp.Request;
using MossApp.Utilities;
using MossApp.Utilities.Extensions;
using MossApp.Utilities.Wrapper;
using MossApp.WPF.Views.Dialogs;
using MossApp.WPF.Views.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Media;

namespace MossApp.WPF.ViewModels
{
    using static Properties.Settings;

    public class RequestConfigViewModel : BindableBase
    {

        #region FileList
        private ObservableCollection<string> _baseFiles;
        public ObservableCollection<string> BaseFiles
        {
            get => _baseFiles;
            set => SetProperty(ref _baseFiles, value);
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

        public RelayCommand SendRequestCommand { get; set; }

        #endregion FileList

        #region PrimaryConfigSet

        /// <summary>
        /// Gets or sets the maximum matches.
        /// </summary>
        /// <value>
        /// The maximum matches.
        /// </value>
        /// <remarks>
        /// ++ The -m option sets the maximum number of times a given passage may appear 
        /// before it is ignored.  A passage of code that appears in many programs 
        /// is probably legitimate sharing and not the result of plagiarism.  With -m N, 
        /// any passage appearing in more than N programs is treated as if it appeared in 
        /// a base file (i.e., it is never reported).  Option -m can be used to control 
        /// moss' sensitivity.  With -m 2, moss reports only passages that appear 
        /// in exactly two programs.  If one expects many very similar solutions 
        /// (e.g., the short first assignments typical of introductory programming courses) 
        /// then using -m 3 or -m 4 is a good way to eliminate all but 
        /// truly unusual matches between programs while still being able to detect 
        /// 3-way or 4-way plagiarism.  With -m 1000000 (or any very large number), 
        /// moss reports all matches, no matter how often they appear.  
        /// The -m setting is most useful for large assignments where one also a base file 
        /// expected to hold all legitimately shared code.  
        /// The default for -m is 3.
        /// </remarks>
        private int _maxMatches;

        public int MaxMatches
        {
            get => _maxMatches;
            set => SetProperty(ref _maxMatches, value);
        }

        private int _numberOfResultsToShow;
        public int NumberOfResultsToShow
        {
            get => _numberOfResultsToShow;
            set
            {
                SetProperty(ref _numberOfResultsToShow, value);
                //_mossRequest.NumberOfResultsToShow = _numberOfResultsToShow;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is beta request.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is beta request; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>
        /// ++ Represents the -x option sends queries to the current experimental version of the server. 
        /// The experimental server has the most recent Moss features and is also usually 
        /// less stable (read: may have more bugs).
        /// </remarks>
        /// 
        private bool _isBetaRequest;
        public bool IsBetaRequest
        {
            get => _isBetaRequest;
            set => SetProperty(ref _isBetaRequest, value);
        }


        private bool _isDirectoryMode;

        public bool IsDirectoryMode
        {
            get => _isDirectoryMode;
            set => SetProperty(ref _isDirectoryMode, value);
        }


        /// <summary>
        /// Gets or sets the server.
        /// </summary>
        /// <value>
        /// The server.
        /// </value>
        private string _server;
        public string Server
        {
            get => _server;
            set => SetProperty(ref _server, value ?? string.Empty);
        }

        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        /// <value>
        /// The port.
        /// </value>
        private int _port;
        public int Port
        {
            get => _port;
            set => SetProperty(ref _port, value);
        }

        /// <summary>
        /// Gets or sets the languages.
        /// </summary>
        /// <value>
        /// The languages and their associated file extensions.
        /// </value>
        private ObservableCollection<Language> _languages = new();

        public ObservableCollection<Language> Languages
        {
            get => _languages;
            set => SetProperty(ref _languages, value);

        }

        private string m_selectedAction;
        public string SelectedAction
        {
            get => m_selectedAction;

            set
            {
                m_selectedAction = value;

                OnPropertyChanged(nameof(SelectedAction));
            }
        }


      
        private Language _selectedLanguage;

        public Language SelectedLanguage
        {
            get => _selectedLanguage;
            set
            {
                SetProperty(ref _selectedLanguage, value);
                if (value?.Extensions != null)
                    RestrictedFileTypesInput = value.Extensions.Split(",").ToList().ToExtensionString();

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
            set
            {

                SetProperty(ref _restrictFileTypes, value);
                if (value)
                {
                    //SetFilters(true);
                }
                else
                {
                    // SetFilters(false);
                }

            }
        }
        #endregion PrimaryConfigSet

        #region SelectFiles
        private string _currentDirectory = "F:\\repos\\";
        public string CurrentDirectory
        {
            get => _currentDirectory;
            set => SetProperty(ref _currentDirectory, value);
        }

        private string _filters;

        public string Filters
        {
            get => _filters;
            set => SetProperty(ref _filters, value);
        }

        private bool _isBaseSelection;
        public bool IsBaseSelection
        {
            get => _isBaseSelection;
            set
            {
                SetProperty(ref _isBaseSelection, value);
                if (value)
                {
                    SourceFileLabelColor = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF505050");
                    BaseFileLabelColor = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFAFAFA");
                }
                else
                {
                    BaseFileLabelColor = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF505050");
                    SourceFileLabelColor = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFAFAFA");
                }
            }
        }

        private SolidColorBrush _sourceFileLabelColor = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFAFAFA");
        public SolidColorBrush SourceFileLabelColor
        {
            get => _sourceFileLabelColor;
            set => SetProperty(ref _sourceFileLabelColor, value);
        }

        private SolidColorBrush _baseFileLabelColor;
        public SolidColorBrush BaseFileLabelColor
        {
            get => _baseFileLabelColor;
            set => SetProperty(ref _baseFileLabelColor, value);
        }


        public void AddFile(object param)
        {

            List<FileInfo> infoList = (List<FileInfo>)param;
            List<string> list = infoList.Select(x => x.Name).ToList();
            if (_isBaseSelection)
            {
                list.ForEach(i => BaseFiles.Add(i));
            }
            else
            {
                list.ForEach(i => Files.Add(i));
            }
        }

        public RelayCommand AddFileCommand { get; private set; }
        #endregion SelectFiles

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
        private CancellationTokenSource cts;
      
        private IMossRequest _mossRequest;
        public RequestConfigViewModel()
        {
           // _mossRequest = mossRequest;
         
            AddFileCommand = new RelayCommand(AddFile);
            ShowOptionsFlyoutCommand = new RelayCommand(_ => ShowOptionsFlyout());
            ShowSourceFilesFlyoutCommand = new RelayCommand(_ => ShowSourceFilesFlyout());
            ShowBaseFileFlyoutCommand = new RelayCommand(_ => ShowBaseFileFlyout());
            OpenUserIdDialogCommand = new RelayCommand(_ => OpenUserIdDialog());
            Files = new ObservableCollection<string>()
            {
                "F:\\repos\\demos\\c89\\C89Demo\\C89Demo\\C89Demo.cpp",
                "F:\\repos\\demos\\c89\\C89_Copy\\C89_Copy\\C89_Copy.cpp"
            };
            BaseFiles = new ObservableCollection<string>();
            SendRequestCommand = new RelayCommand(SendRequestAsync);
            // _ea.GetEvent<LanguageSetEvent>().Subscribe(ReceiveLanguageSet);
            ParseLanguageSettings();
            InitializeRequest();
            cts = new CancellationTokenSource();
          
            //_filesControlViewModel = openMultipleFilesControlViewModel;
        }

        private void InitializeRequest()
        {
            // Read default values from setting file. 
            long setting = Default.UserId;
            UserId = setting == 0 ? 0 : setting;

            MaxMatches = Default.OptionM;
            NumberOfResultsToShow = Default.OptionN;
            Server = Default.Server;
            Port = Default.Port;

            Comments = Default.OptionC;
        }

        


        public Flyout? OptionsFlyout { get; set; }
        public Flyout? SourceFilesFlyout { get; set; }

        private void ToggleFlyout(object param)
        {
            var side = param as string;
            switch (side)
            {
                case "Bottom":
                    ((RequestConfigWindow)Application.Current.MainWindow).OptionsFlyout.IsOpen = !((RequestConfigWindow)Application.Current.MainWindow).OptionsFlyout.IsOpen;
                    break;

            }
        }

        private void ShowOptionsFlyout()
        {

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



        private long _userId;
        [Required]
        public long UserId
        {
            get => _userId;
            set => SetProperty(ref _userId, value);
        }





        private string _comments;
        public string Comments
        {
            get => _comments;
            set => SetProperty(ref _comments, value ?? string.Empty);
        }

        private string _response;

        public string Response
        {
            get => _response;
            set => SetProperty(ref _response, value);
        }

        private bool SetResponse(string response)
        {
            Response = response;
            return true;
        }

  

        private bool _isWaiting;

        public bool IsWaiting
        {
            get => _isWaiting;
            set => SetProperty(ref _isWaiting, value);
        }


        private async void SendRequestAsync(object param)
        {
            var token = cts.Token;
            Func<string,bool> responseSetter = (string x) => { return SetResponse(x); };
            //TODO: Check User Id, selected language, etc before sending request

            //this.ErrorLabel.Text = string.Empty;
            Default.UserId = Convert.ToInt32(UserId) == 0 ? 337859480 : Convert.ToInt32(UserId);
            Default.OptionM = Convert.ToInt32(MaxMatches);
            Default.OptionN = Convert.ToInt32(NumberOfResultsToShow);
            Default.OptionC = Comments;
            Default.Save();

            var request = new MossRequest
            {
                UserId = Convert.ToInt32(UserId),
                //IsDirectoryMode = IsDirectoryMode,
                IsBetaRequest = IsBetaRequest,
                Comments = Comments,
                Language = SelectedLanguage.Name,
                NumberOfResultsToShow = Convert.ToInt32(NumberOfResultsToShow),
                MaxMatches = Convert.ToInt32(MaxMatches)
            };
            
            request.BaseFile.AddRange(BaseFiles);
            request.Files.AddRange(Files);
            //Mediator.GetInstance().OnRequestSent(this, request.SendRequest(out var response));
            //   RequestProgressBar.Visible = true;
            IsWaiting = true;
            string response = "";
            bool success = false;
            var requestTask = await request.SendRequestAsync(token, responseSetter);
            IsWaiting = false;
            //await Task.Run(() =>
            //{
            //    success = request.SendRequest(out response);

            //});
           // RequestProgressBar.Visible = false;
            if (requestTask)
            {
                
                HtmlWeb web = new HtmlWeb();
                List<string> links = new List<string>();
                var htmlDoc = web.Load(Response);

                var node = htmlDoc.DocumentNode.SelectSingleNode("//body");

                foreach (var nNode in node.Descendants("a"))
                {
                    if (nNode.NodeType == HtmlNodeType.Element)
                    {

                        Console.WriteLine("Node Name: " + nNode.Name + "\n" + nNode.OuterHtml);
                    }
                }

              //  this.WebBrowser.Navigate(new Uri(response));
            }
            else
            {
                //MessageBox.Show(
                //    response,
                //    Resources.Request_Error_Caption,
                //    MessageBoxButtons.OK,
                //    MessageBoxIcon.Error);
            }
            //MossRequest request = new MossRequest();
            //request.BaseFile.AddRange(BaseFiles);
            //request.Files.AddRange(Files);
            //request.Language = SelectedLanguage.Name;
            //request.IsBetaRequest = IsBetaRequest;
            ////request.IsDirectoryMode = IsDirectoryMode;
            //request.MaxMatches = MaxMatches;
            //request.NumberOfResultsToShow = NumberOfResultsToShow;
            //request.Port = Port;
            //request.Server = Server;
            //request.Comments = Comments;

        }

        /// <summary>
        /// Gets the restricted file types as a list.
        /// </summary>
        /// <returns>
        /// A list of files types to accept.
        /// </returns>
        private List<string> GetRestrictedFileTypes()
        {

            return !string.IsNullOrWhiteSpace(_restrictedFileTypesInput) ? _restrictedFileTypesInput.Split(',').ToList() : new List<string>();
        }

        /// <summary>
        /// Parses the language settings into the Language Dictionary of
        /// Languages => extensions.
        /// </summary>
        private void ParseLanguageSettings()
        {

            System.Collections.Specialized.StringCollection? languageList = Default.Languages;
            foreach (string? language in languageList)
            {
                Language temp = new Language();

                string[]? tokens = language?.Split(',');

                if (tokens?.Length > 0)
                {
                    temp.DisplayName = tokens[0];
                }
                if (tokens?.Length > 1)
                {
                    temp.Mapping = tokens[1];
                }
                if (tokens?.Length > 2)
                {
                    temp.Name = tokens[2];
                }
                if (tokens?.Length > 3)
                {
                    var iconStatus = tokens[3];
                    if (iconStatus == "Text")
                        temp.IconType = IconType.Text;
                    else if (iconStatus == "Moss")
                        temp.IconType = IconType.Moss;
                    else if (iconStatus == "Material")
                        temp.IconType = IconType.Material;
                }

                if (tokens?.Length > 4)
                {
                    temp.Icon = tokens[4];
                }

                
                for (int index = 4; index < tokens?.Length; index++)
                {
                    temp.Extensions = temp.Extensions + ", " + tokens?[index];
                }

                Languages.Add(temp);
                // because this setting is created with a specific format, this 
                // should never fail. If it does, the underlying data source needs to be 
                // fixed. 
            }
        }

        private void OpenLink(string url)
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            };

            Process.Start(psi);
        }


    }
  
}
