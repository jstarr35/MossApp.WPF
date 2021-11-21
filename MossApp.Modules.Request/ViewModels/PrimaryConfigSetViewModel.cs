using MossApp.Utilities.Extensions;
using MossApp.Utilities.Wrapper;
using Prism.Events;
using System.Collections.ObjectModel;

namespace MossApp.Modules.Request.ViewModels
{
    public class PrimaryConfigSetViewModel : BindableBase
    {
        IEventAggregator _ea;

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

    }
}
