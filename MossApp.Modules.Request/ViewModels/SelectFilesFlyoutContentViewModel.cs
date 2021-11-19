
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
           // _ea.GetEvent<FilterSetEvent>().Subscribe(SetFilter, true);

            //PublishFileCommand = new RelayCommand(PublishFile);
            ControlLoadedCommand = new DelegateCommand(ControlLoaded);

        }
        public DelegateCommand SendFileCommand { get; private set; }

        public RelayCommand PublishFileCommand { get; private set; }
        public DelegateCommand ControlLoadedCommand { get; set; }

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

        private string _filters;

        public string Filters
        {
            get => _filters;
            set => SetProperty(ref _filters, value);
        }

        public void ControlLoaded()
        {
            //_ea.GetEvent<ControlLoadedEvent>().Publish();
        }

        private void SetFilter(string filters)
        {
            Filters = filters;
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
        private bool _isBaseSelection;
        public bool IsBaseSelection
        {
            get => _isBaseSelection;
            set => SetProperty(ref _isBaseSelection, value);
        }

        //public void PublishFile(object param)
        //{
        //    List<string> list = (List<string>)param;
        //    if (_isBaseSelection)
        //        list.ForEach(i => BaseFiles.Add(i));
        //    else
        //        list.ForEach(i => Files.Add(i));


        //}

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
