using MossApp.Utilities.Wrapper;

using System;
using System.Collections.Generic;

namespace MossApp.Modules.Request.ViewModels
{
    public abstract class FileSystemControlViewModel : BindableBase
    {
        private string m_selectedAction;
        private bool m_showHiddenFilesAndDirectories;
        private bool m_showSystemFilesAndDirectories;
        private bool m_createNewDirectoryEnabled;
        private bool m_switchPathPartsAsButtonsEnabled;


        public bool CreateNewDirectoryEnabled
        {
            get => m_createNewDirectoryEnabled;

            set
            {
                m_createNewDirectoryEnabled = value;

                OnPropertyChanged(nameof(CreateNewDirectoryEnabled));
            }
        }

        public string SelectedAction
        {
            get => m_selectedAction;

            set
            {
                m_selectedAction = value;

                OnPropertyChanged(nameof(SelectedAction));
            }
        }

        public bool ShowHiddenFilesAndDirectories
        {
            get => m_showHiddenFilesAndDirectories;

            set
            {
                m_showHiddenFilesAndDirectories = value;

                OnPropertyChanged(nameof(ShowHiddenFilesAndDirectories));
            }
        }

        public bool ShowSystemFilesAndDirectories
        {
            get => m_showSystemFilesAndDirectories;

            set
            {
                m_showSystemFilesAndDirectories = value;

                OnPropertyChanged(nameof(ShowSystemFilesAndDirectories));
            }
        }

        public bool SwitchPathPartsAsButtonsEnabled
        {
            get => m_switchPathPartsAsButtonsEnabled;

            set
            {
                m_switchPathPartsAsButtonsEnabled = value;

                OnPropertyChanged(nameof(SwitchPathPartsAsButtonsEnabled));
            }
        }

        private List<string> _sourceFileList;
        public List<string> SourceFileList
        {
            get => _sourceFileList;
            set => SetProperty(ref _sourceFileList, value);
        }

        public virtual void AddSourceFile(string fileName)
        {
            _sourceFileList.Add(fileName);
        }

        public virtual List<string> GetSourceFiles()
        {
            return SourceFileList;
        }

        public event EventHandler<FileListChangedEventArgs> SourceFilesChanged;

        public virtual void OnSourceFilesChanged(string file)
        {
            SourceFilesChanged.Invoke(this, new FileListChangedEventArgs(file));
        }

        public void ClearSourceFiles()
        {
            SourceFileList.Clear();
            OnSourceFilesChanged("CLEAR");
        }

        public FileSystemControlViewModel()

        {
            m_selectedAction = null;
            m_showHiddenFilesAndDirectories = false;
            m_showSystemFilesAndDirectories = false;
            m_createNewDirectoryEnabled = false;
            m_switchPathPartsAsButtonsEnabled = false;
            _sourceFileList = new List<string>();
        }



    }

    public class FileListChangedEventArgs : EventArgs
    {
        public FileListChangedEventArgs(string? fileName)
        {

        }

        public virtual string? FileName { get; }
        public virtual List<string> FileList { get; }
    }
}
