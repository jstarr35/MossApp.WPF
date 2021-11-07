using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MossApp.Core;
using MossApp.Data;

namespace MossApp.Modules.SourceFileList.ViewModels
{
    public class SourceFileListViewModel : BindableBase
    {
        IEventAggregator _ea;

        private ObservableCollection<string> _files;
        public ObservableCollection<string> Files
        {
            get { return _files; }
            set { SetProperty(ref _files, value); }
        }

        private bool _isLoading;

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        public DelegateCommand SendRequestCommand;


        public SourceFileListViewModel(IEventAggregator ea)
        {
            _ea = ea;
            Files = new ObservableCollection<string>();

            _ea.GetEvent<FileSentEvent>().Subscribe(FileReceived, true);
        }

        private void FileReceived(string file)
        {
            Files.Add(file);
        }

        private async void SendRequest()
        {
            IsLoading = true;
            var request = new MossRequest
            {
                //UserId = Convert.ToInt32(this.UserIdTextBox.Text),
                //IsDirectoryMode = this.DirectoryModeCheckBox.Checked,
                //IsBetaRequest = this.ExperimentalServerCheckBox.Checked,
                //Comments = this.OptionCTextBox.Text,
                //Language = this.LanguagesComboBox.SelectedValue.ToString(),
                //NumberOfResultsToShow = Convert.ToInt32(this.OptionNTextBox.Text),
                //MaxMatches = Convert.ToInt32(this.OptionMTextBox.Text)
            };
            await Task.Run(async() => { });
        }
    }
}
