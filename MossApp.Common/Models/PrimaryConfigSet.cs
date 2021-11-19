namespace MossApp.Common.Models
{
    public class PrimaryConfigSet
    {
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
        public int MaxMatches { get; set; }

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
        public bool IsBetaRequest { get; set; }

        /// <summary>
        /// Gets or sets the number of results to show.
        /// </summary>
        /// <value>
        /// The number of results to show.
        /// </value>
        /// <remarks>
        /// The -n option determines the number of matching files to show in the results. 
        /// The default is 250.
        /// </remarks>
        public int NumberOfResultsToShow { get; set; }

        public string Server { get; set; }

        public string Port { get; set; }

        public string SelectedLanguage { get; set; }

        public PrimaryConfigSet(int maxMatches, bool isBetaRequest, int numberOfResultsToShow, string server, string port, string selectedLanguage)
        {
            MaxMatches = maxMatches;
            IsBetaRequest = isBetaRequest;
            NumberOfResultsToShow = numberOfResultsToShow;
            Server = server;
            Port = port;
            SelectedLanguage = selectedLanguage;
        }
    }
}
