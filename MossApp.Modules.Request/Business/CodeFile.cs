using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace MossApp.Modules.Request.Business
{
    public class CodeFile : INotifyPropertyChanged
    {
        #region Properties

        private string _fileName;
        public string FileName
        {
            get { return _fileName; }
            set
            {
                _fileName = value;
                OnPropertyChanged();
            }
        }

        private string _language;
        public string Language
        {
            get { return _language; }
            set
            {
                _language = value;
                OnPropertyChanged();
            }
        }

        private string _extension;
        public string Extension
        {
            get { return _extension; }
            set
            {
                _extension = value;
                OnPropertyChanged();
            }
        }

        private int _age;
        public int Age
        {
            get { return _age; }
            set
            {
                _age = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _lastUpdated;
        public DateTime? LastUpdated
        {
            get { return _lastUpdated; }
            set
            {
                _lastUpdated = value;
                OnPropertyChanged();
            }
        }

        #endregion //Properties

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

        #endregion //INotifyPropertyChanged

        public override string ToString()
        {
            return String.Format("{0}, {1}", Language, FileName);
        }
    }
}
