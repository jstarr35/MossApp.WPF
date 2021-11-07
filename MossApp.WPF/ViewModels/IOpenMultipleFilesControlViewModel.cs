using System.Collections.Generic;

namespace MossApp.WPF.ViewModels
{
    public interface IOpenMultipleFilesControlViewModel
    {
        void AddSourceFile(string fileName);
        List<string> GetSourceFiles();
        void OnSourceFilesChanged(string file);
    }
}