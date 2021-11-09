using System.Collections.Generic;
using System.Threading.Tasks;

namespace MossApp.Request
{
    public interface IMossRequest
    {
        List<string> BaseFile { get; }
        string Comments { get; set; }
        List<string> Files { get; }
        bool IsBetaRequest { get; set; }
        bool IsDirectoryMode { get; set; }
        string Language { get; set; }
        int MaxMatches { get; set; }
        int NumberOfResultsToShow { get; set; }
        string Response { get; set; }
        long UserId { get; set; }
        int Port { get; set; }
        string Server { get; set; }

        Task<bool> SendRequestAsync();
    }
}