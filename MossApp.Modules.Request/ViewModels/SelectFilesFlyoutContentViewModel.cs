using Microsoft.Extensions.Logging;
using MossApp.Core;
using MossApp.Utilities;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MossApp.Modules.Request.ViewModels
{
    public class SelectFilesFlyoutContentViewModel : FileSystemControlViewModel
    {
        private readonly IEventAggregator _ea;
        private readonly ILogger _logger;

        public SelectFilesFlyoutContentViewModel(IEventAggregator ea) : base()
        {
            _ea = ea;
          //  _logger = logger;
            SendFileCommand = new DelegateCommand(SendFile);
            PublishFileCommand = new RelayCommand(PublishFile);
        }
        public DelegateCommand SendFileCommand { get; private set; }
        public RelayCommand PublishFileCommand { get; private set; }
        private string _file = "FileName";
        public string File
        {
            get { return _file; }
            set { SetProperty(ref _file, value); }
        }

        private string _currentDirectory = "F:\\repo\\";
        public string CurrentDirectory
        {
            get { return _currentDirectory; }
            set { SetProperty(ref _currentDirectory, value); }
        }
        public override void AddSourceFile(string fileName)
        {

            base.AddSourceFile(fileName);
            //File = fileName;
            //SendFile(fileName);
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
        public void SendFile()
        {

            base.AddSourceFile(SelectedAction.ToString());
            //       _logger.LogInformation($"Publishings SelectedAction with a value of {SelectedAction}");

            _ea.GetEvent<FileSentEvent>().Publish(SelectedAction.ToString());
        }

        public void PublishFile(object param)
        {

            //   _logger.LogInformation($"Publish File Command with a param value of {param}");
            _ea.GetEvent<FileSentEvent>().Publish(param.ToString());
        }

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
