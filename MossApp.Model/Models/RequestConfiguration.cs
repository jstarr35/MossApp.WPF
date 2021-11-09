using System.Collections.Generic;

namespace MossApp.Data
{
    public partial class MossRequest
    {
        /// <summary>
        /// The file upload format string. 
        /// </summary>
        private const string FileUploadFormat = "file {0} {1} {2} {3}\n";

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public long UserId { get; set; }

        /// <summary>
        /// The language for this request. 
        /// </summary>
        /// <remarks>
        /// ++ The -l option specifies the source language of the tested programs. 
        /// Moss supports many different languages
        /// See Properties.Settings.Default.Languages
        /// </remarks>
        public string Language { get; set; }

        /// <summary>
        /// The comments for this request.
        /// </summary>
        /// <remarks>
        /// ++ The -c option supplies a comment string that is attached to the generated 
        /// report.  This option facilitates matching queries submitted with replies 
        /// received, especially when several queries are submitted at once.
        /// </remarks>
        public string Comments { get; set; }


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
        public int MaxMatches { get; set; } = 10;

        /// <summary>
        /// Gets an object representing the collection of the Source File(s) contained in this Request.
        /// </summary>
        /// <value>
        /// The files.
        /// </value>
        /// <remarks>
        /// This property enables you to obtain a reference to the list of Source File(s) that are currently stored in the Request. 
        /// With this reference, you can add items, remove items, and obtain a count of the Files in the Request.
        /// </remarks>
        public List<string> SourceFiles { get; set; } = new List<string>();

        /// <summary>
        /// Gets an object representing the collection of the Base File(s) contained in this Request.
        /// </summary>
        /// <value>
        /// The base file.
        /// </value>
        /// <remarks>
        /// This property enables you to obtain a reference to the list of Base File(s) that are currently stored in the Request. 
        /// With this reference, you can add items, remove items, and obtain a count of the Files in the Request.
        /// ++ The -b option names a "base file".  Moss normally reports all code 
        /// that matches in pairs of files.  When a base file is supplied, 
        /// program code that also appears in the base file is not counted in matches. 
        /// A typical base file will include, for example, the instructor-supplied 
        /// code for an assignment.  Multiple -b options are allowed.  You should u
        /// se a base file if it is convenient; base files improve results, but 
        /// are not usually necessary for obtaining useful information. 
        /// IMPORTANT: Unlike previous versions of moss, the -b option *always* 
        /// takes a single filename,
        /// </remarks>
        public List<string> BaseFile { get; set; } = new List<string>();



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
        public bool IsBetaRequest { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is directory mode.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is directory mode; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>
        /// ++ The -d option specifies that submissions are by directory, not by file. 
        /// That is, files in a directory are taken to be part of the same program, 
        /// and reported matches are organized accordingly by directory.
        /// </remarks>
        public bool IsDirectoryMode { get; set; }

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
        public int NumberOfResultsToShow { get; set; } = 250;
    }
}
