using System.Net.Sockets;

namespace MossApp.Data
{
    public partial class MossRequest
    {
        /// <summary>
        /// Gets or sets the server.
        /// </summary>
        /// <value>
        /// The server.
        /// </value>
        public string Server { get; set; }

        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        /// <value>
        /// The port.
        /// </value>
        public int Port { get; set; }

        /// <summary>
        /// Gets or sets the moss socket.
        /// </summary>
        /// <value>
        /// The moss socket.
        /// </value>
        public Socket MossSocket { get; set; }

        /// <summary>
        /// Gets or sets the size of the response byte array.
        /// </summary>
        /// <value>
        /// The size of the response byte array.
        /// </value>
        public int ReplySize { get; set; } = 512;
    }
}
