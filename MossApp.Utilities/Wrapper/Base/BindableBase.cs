using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace MossApp.Utilities.Wrapper
{
    public class BindableBase : INotifyPropertyChanged
    {
        protected virtual void SetProperty<T>(ref T member, T val,
            [CallerMemberName] string propertyName = null)
        {
            if (Equals(member, val)) return;
            member = val;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected readonly Dictionary<string, List<string>> Errors;

        protected BindableBase()
        {
            Errors = new Dictionary<string, List<string>>();
        }

        public bool HasErrors => Errors.Any();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            return propertyName != null && Errors.ContainsKey(propertyName)
              ? Errors[propertyName]
              : Enumerable.Empty<string>();
        }

        protected virtual void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        protected void ClearErrors()
        {
            foreach (string propertyName in Errors.Keys.ToList())
            {
                _ = Errors.Remove(propertyName);
                OnErrorsChanged(propertyName);
            }
        }
    }
}
