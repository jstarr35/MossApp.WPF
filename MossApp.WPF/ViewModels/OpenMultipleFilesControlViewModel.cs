using System.Collections.Generic;

namespace MossApp.WPF.ViewModels
{
    public class OpenMultipleFilesControlViewModel : FileSystemControlViewModel, IOpenMultipleFilesControlViewModel
    {

        public OpenMultipleFilesControlViewModel() : base()
        {

        }

        public override void AddSourceFile(string fileName)
        {
            base.AddSourceFile(fileName);
            //SendTo<RequestConfigViewModel>(fileName);
        }

        //public override void Send(string message)
        //{
        //    base.Send(message);
        //}

        //public override void SendTo<T>(string message)
        //{
        //    Send(message);
        //    base.SendTo<T>(message);
        //}

        public override void OnSourceFilesChanged(string file)
        {
            base.OnSourceFilesChanged(file);
            //  SendTo<RequestConfigViewModel>(file);
        }

        public override List<string> GetSourceFiles()
        {
            return base.GetSourceFiles();
        }

        //public override void HandleNotification(string message)
        //{
        //    throw new NotImplementedException();
        //}



    }
}
