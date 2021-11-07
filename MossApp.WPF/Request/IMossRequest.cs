using System.Collections.Generic;

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

        bool SendRequest();
    }
}