using MossApp.Core;
using MossApp.Utilities;
using Prism.Commands;
using Prism.Events;
using System.Collections;
using System.Collections.Generic;


namespace MossApp.Modules.Request.ViewModels
{
    public class SelectFilesFlyoutContentViewModel : FileSystemControlViewModel
    {
        private readonly IEventAggregator _ea;

        public SelectFilesFlyoutContentViewModel(IEventAggregator ea) : base()
        {
            _ea = ea;
            //  _logger = logger;
            //SendFileCommand = new DelegateCommand(SendFile);
            PublishFileCommand = new RelayCommand(PublishFile);
        }
        public DelegateCommand SendFileCommand { get; private set; }
        public RelayCommand PublishFileCommand { get; private set; }
        private string _file = "FileName";
        public string File
        {
            get => _file;
            set => SetProperty(ref _file, value);
        }

        private string _currentDirectory = "F:\\repos\\";
        public string CurrentDirectory
        {
            get => _currentDirectory;
            set => SetProperty(ref _currentDirectory, value);
        }
        public override void AddSourceFile(string fileName)
        {

            base.AddSourceFile(fileName);

        }

        //public void SendFile()
        //{

        //    base.AddSourceFile(SelectedAction.ToString());


        //    _ea.GetEvent<FileSentEvent>().Publish(SelectedAction.ToString());
        //}

        public void PublishFile(object param)
        {

            var list = (IList)param;
            foreach(var i in list)
            {
                _ea.GetEvent<FileSentEvent>().Publish(i.ToString());
            }
            
        }

        public override void OnSourceFilesChanged(string file)
        {
            base.OnSourceFilesChanged(file);
        }

        public override List<string> GetSourceFiles()
        {
            return base.GetSourceFiles();
        }




    }
}
