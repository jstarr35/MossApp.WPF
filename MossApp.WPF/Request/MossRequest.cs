

namespace MossApp.Request
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading.Tasks;
    using MossApp.WPF.Properties;
    using Prism.Mvvm;



    /// <summary>
    /// Models a Moss (for a Measure Of Software Similarity) Request. 
    /// </summary>
    /// <remarks>
    /// The comments regarding the options for the request are copied directly from the 
    /// MOSS documentation (http://moss.stanford.edu/general/scripts/mossnet) denoted by ++
    /// </remarks>
    public sealed class MossRequest : BindableBase, IMossRequest
    {
        //private string _status;

        //public string Status
        //{
        //    get => _status;
        //    set => AppendProperty(ref _status, value);
        //}



        /// <summary>
        /// The file upload format string. 
        /// </summary>
        private const string FileUploadFormat = "file {0} {1} {2} {3}\n";

        /// <summary>
        /// The default maximum matches
        /// </summary>
        private const int DefaultMaxMatches = 10;

        /// <summary>
        /// The default number of results to show
        /// </summary>
        private const int DefaultNumberOfResultsToShow = 250;

        /// <summary>
        /// The language for this request. 
        /// </summary>
        /// <remarks>
        /// ++ The -l option specifies the source language of the tested programs. 
        /// Moss supports many different languages
        /// See Properties.Settings.Default.Languages
        /// </remarks>
        private string language;

        /// <summary>
        /// The comments for this request.
        /// </summary>
        /// <remarks>
        /// ++ The -c option supplies a comment string that is attached to the generated 
        /// report.  This option facilitates matching queries submitted with replies 
        /// received, especially when several queries are submitted at once.
        /// </remarks>
        private string comments;

        /// <summary>
        /// Initializes a new instance of the <see cref="MossRequest"/> class.
        /// </summary>
        public MossRequest()
        {
            Files = new List<string>();
            BaseFile = new List<string>();
            UserId = 0;
            Server = Settings.Default.Server;
            Port = Settings.Default.Port;
            language = string.Empty;
            comments = string.Empty;
            MaxMatches = DefaultMaxMatches;
            NumberOfResultsToShow = DefaultNumberOfResultsToShow;
            IsDirectoryMode = false;
            IsBetaRequest = false;

        }

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
        /// Gets an object representing the collection of the Source File(s) contained in this Request.
        /// </summary>
        /// <value>
        /// The files.
        /// </value>
        /// <remarks>
        /// This property enables you to obtain a reference to the list of Source File(s) that are currently stored in the Request. 
        /// With this reference, you can add items, remove items, and obtain a count of the Files in the Request.
        /// </remarks>
        public List<string> Files { get; private set; } = new List<string>();

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
        public List<string> BaseFile { get; private set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        private long _userId;

        public long UserId
        {
            get => _userId;
            set => SetProperty(ref _userId, value);
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
        private bool _isDirectoryMode;
        public bool IsDirectoryMode
        {
            get => _isDirectoryMode;
            set => SetProperty(ref _isDirectoryMode, value);
        }

        private string _reponse;

        public string Response
        {
            get => _reponse;
            set => SetProperty(ref _reponse, value ?? string.Empty);
        }


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

        /// <summary>
        /// Gets or sets the language for this request
        /// </summary>
        /// <value>
        /// The language.
        /// </value>
        /// <remarks>
        /// ++ The -l option specifies the source language of the tested programs. 
        /// Moss supports many different languages
        /// See Properties.Settings.Default.Languages
        /// </remarks>
        public string Language
        {
            get => language;

            set => language = value ?? string.Empty;
        }

        /// <summary>
        /// Gets or sets the comments for the request.
        /// </summary>
        /// <value>
        /// The comments.
        /// </value>
        /// <remarks>
        /// ++ The -c option supplies a comment string that is attached to the generated 
        /// report.  This option facilitates matching queries submitted with replies 
        /// received, especially when several queries are submitted at once.
        /// </remarks>
        public string Comments
        {
            get => comments;

            set => comments = value ?? string.Empty;
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
        /// Gets or sets the moss socket.
        /// </summary>
        /// <value>
        /// The moss socket.
        /// </value>
        private Socket MossSocket { get; set; }

        /// <summary>
        /// Gets or sets the size of the response byte array.
        /// </summary>
        /// <value>
        /// The size of the response byte array.
        /// </value>
        private int ReplySize { get; set; } = 512;

        /// <summary>
        /// Sends the request.
        /// </summary>
        /// <param name="response">The response from the request.</param>
        /// <returns>
        /// <code>true</code> if the response was successful, otherwise <code>false</code>
        /// </returns>
        /// <remarks>
        /// If the request is successful, <code>true</code> is returned, then response is a valid <see cref="System.Uri"/>
        /// </remarks>
        public async Task<bool> SendRequestAsync()
        {
            try
            {
                var hostEntry = Dns.GetHostEntry(Server);

                var address = hostEntry.AddressList[0];
                var ipe = new IPEndPoint(address, Port);
                string result;
                using (var socket = new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp))
                {

                    await socket.ConnectAsync(ipe);
                    // Status.Status = $"Sending Moss option: {Settings.Default.MossOption} with UserID: {UserId}";
                    SendOption(
                        Settings.Default.MossOption,
                        UserId.ToString(CultureInfo.InvariantCulture),
                        socket);
                    SendOption(Settings.Default.DirectoryOption, IsDirectoryMode ? "1" : "0", socket);
                    SendOption(Settings.Default.ExperimentalOption, IsBetaRequest ? "1" : "0", socket);
                    SendOption(
                        Settings.Default.MaxMatchesOption,
                        MaxMatches.ToString(CultureInfo.InvariantCulture),
                        socket);
                    SendOption(
                        Settings.Default.ShowOption,
                        NumberOfResultsToShow.ToString(CultureInfo.InvariantCulture),
                        socket);

                    if (BaseFile.Count != 0)
                    {
                        int counter = 1;
                        foreach (var file in BaseFile)
                        {
                            //  Status.Status = $"Sending file {counter} of {BaseFile.Count}";
                            SendFile(file, socket, 0);
                            counter++;
                        }
                    } // else, no base files to send DoNothing();

                    if (Files.Count != 0)
                    {
                        int fileCount = 1;
                        foreach (var file in Files)
                        {
                            SendFile(file, socket, fileCount++);
                        }
                    } // else, no files to send DoNothing();

                    SendOption("query 0", Comments, socket);

                    var bytes = new byte[ReplySize];
                    socket.Receive(bytes);

                    result = Encoding.UTF8.GetString(bytes);
                    SendOption(Settings.Default.EndOption, string.Empty, socket);
                }

                if (Uri.TryCreate(result, UriKind.Absolute, out var url))
                {
                    Response = url?.ToString().IndexOf("\n", System.StringComparison.Ordinal) > 0
                                    ? url.ToString().Split('\n')[0]
                                    : url?.ToString();
                    return true;
                } // else, not a valid URL, DoNothing();

                Response = Resources.Moss_Request_URI_Error;
                return false;
            }
            catch (Exception ex)
            {
                // TODO Change from exception never catch exception...
                Response = ex.Message;
                return false;
            }
        }

        private bool SocketConnected(Socket s)
        {
            bool part1 = s.Poll(1000, SelectMode.SelectRead);
            bool part2 = (s.Available == 0);
            if (part1 && part2)
                return false;
            else
                return true;
        }

        /// <summary>
        /// Sends the argument using the given socket.
        /// </summary>
        /// <param name="option">The option.</param>
        /// <param name="value">The value of the argument.</param>
        /// <param name="socket">The OPEN socket.</param>
        /// <remarks>
        /// Assumes that the socket is open!
        /// </remarks>
        private void SendOption(string option, string value, Socket socket)
        {
            if (SocketConnected(socket))
                socket.Send(Encoding.UTF8.GetBytes($"{option} {value}\n"));
            else
                Console.WriteLine($"{socket.Poll(500, SelectMode.SelectError)}");
        }

        /// <summary>
        /// Sends the file using the given socket.
        /// </summary>
        /// <param name="file">The file to send.</param>
        /// <param name="socket">The OPEN socket.</param>
        /// <param name="number">A unique id number for the file.</param>
        /// <remarks>
        /// Assumes that the socket is open!
        /// </remarks>
        private void SendFile(string file, Socket socket, int number)
        {
            var fileInfo = new FileInfo(file);
            socket.Send(
                IsDirectoryMode
                    ? Encoding.UTF8.GetBytes(
                        string.Format(
                            FileUploadFormat,
                            number,
                            language,
                            fileInfo.Length,
                            fileInfo.FullName.Replace("\\", "/").Replace(" ", string.Empty)))
                    : Encoding.UTF8.GetBytes(
                        string.Format(
                            FileUploadFormat,
                            number,
                            language,
                            fileInfo.Length,
                            fileInfo.Name.Replace(" ", string.Empty))));
            Console.WriteLine(fileInfo.FullName.Replace("\\", "/").Replace(" ", string.Empty));
            socket.BeginSendFile(file, FileSendCallback, socket);
        }
        private static async Task<string> SendRequestAsync(string server, int port, string method, string data)
        {
            try
            {
                IPAddress ipAddress = null;
                IPHostEntry ipHostInfo = Dns.GetHostEntry(server);
                for (int i = 0; i < ipHostInfo.AddressList.Length; ++i)
                {
                    if (ipHostInfo.AddressList[i].AddressFamily ==
                      AddressFamily.InterNetwork)
                    {
                        ipAddress = ipHostInfo.AddressList[i];
                        break;
                    }
                }
                if (ipAddress == null)
                    throw new Exception("No IPv4 address for server");
                TcpClient client = new TcpClient();
                await client.ConnectAsync(ipAddress, port); // Connect
                NetworkStream networkStream = client.GetStream();
                StreamWriter writer = new StreamWriter(networkStream);
                StreamReader reader = new StreamReader(networkStream);
                writer.AutoFlush = true;
                string requestData = "method=" + method + "&" + "data=" +
                  data + "&eor"; // 'End-of-request'
                await writer.WriteLineAsync(requestData);
                string response = await reader.ReadLineAsync();
                client.Close();
                return response;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        private static void FileSendCallback(IAsyncResult result)
        {
            var client = result.AsyncState as Socket;
            client?.EndSendFile(result);
        }
    }
}