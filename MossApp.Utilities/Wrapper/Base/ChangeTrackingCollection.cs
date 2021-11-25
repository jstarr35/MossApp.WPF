﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace MossApp.Utilities.Wrapper
{
    public class ChangeTrackingCollection<T> : ObservableCollection<T>, IValidatableTrackingObject
   where T : class, IValidatableTrackingObject
    {
        private IList<T> _originalCollection;

        private ObservableCollection<T> _addedItems;
        private ObservableCollection<T> _removedItems;
        private ObservableCollection<T> _modifiedItems;

        public ChangeTrackingCollection(IEnumerable<T> items)
            : base(items)
        {
            _originalCollection = this.ToList();

            AttachItemPropertyChangedHandler(_originalCollection);

            _addedItems = new ObservableCollection<T>();
            _removedItems = new ObservableCollection<T>();
            _modifiedItems = new ObservableCollection<T>();

            AddedItems = new ReadOnlyObservableCollection<T>(_addedItems);
            RemovedItems = new ReadOnlyObservableCollection<T>(_removedItems);
            ModifiedItems = new ReadOnlyObservableCollection<T>(_modifiedItems);
        }

        public ReadOnlyObservableCollection<T> AddedItems { get; private set; }
        public ReadOnlyObservableCollection<T> RemovedItems { get; private set; }
        public ReadOnlyObservableCollection<T> ModifiedItems { get; private set; }

        public bool IsChanged => AddedItems.Count > 0 || RemovedItems.Count > 0 || ModifiedItems.Count > 0;

        public bool IsValid => this.All(t => t.IsValid);

        public void AcceptChanges()
        {
            _addedItems.Clear();
            _modifiedItems.Clear();
            _removedItems.Clear();
            foreach (T item in this)
            {
                item.AcceptChanges();
            }

            _originalCollection = this.ToList();
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsChanged)));
        }

        public void RejectChanges()
        {
            foreach (T addedItem in _addedItems.ToList())
            {
                _ = Remove(addedItem);
            }
            foreach (T removedItem in _removedItems.ToList())
            {
                removedItem.RejectChanges(); // in case it has been first modified and then removed
                Add(removedItem);
            }
            foreach (T modifiedItem in _modifiedItems.ToList())
            {
                modifiedItem.RejectChanges();
            }
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsChanged)));
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            IEnumerable<T> added = this.Where(current => _originalCollection.All(orig => orig != current));
            IEnumerable<T> removed = _originalCollection.Where(orig => this.All(current => current != orig));

            List<T> modified = this.Except(added).Except(removed).Where(item => item.IsChanged).ToList();

            AttachItemPropertyChangedHandler(added);
            DetachItemPropertyChangedHandler(removed);

            UpdateObservableCollection(_addedItems, added);
            UpdateObservableCollection(_removedItems, removed);
            UpdateObservableCollection(_modifiedItems, modified);

            base.OnCollectionChanged(e);
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsChanged)));
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsValid)));
        }

        private void ItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IsValid))
            {
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsValid)));
            }
            else
            {
                T item = (T)sender;
                if (_addedItems.Contains(item))
                {
                    return;
                }

                if (item.IsChanged)
                {
                    if (!_modifiedItems.Contains(item))
                    {
                        _modifiedItems.Add(item);
                    }
                }
                else
                {
                    if (_modifiedItems.Contains(item))
                    {
                        _ = _modifiedItems.Remove(item);
                    }
                }
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsChanged)));
            }
        }

        private void AttachItemPropertyChangedHandler(IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                item.PropertyChanged += ItemPropertyChanged;
            }
        }

        private void DetachItemPropertyChangedHandler(IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                item.PropertyChanged -= ItemPropertyChanged;
            }
        }

        private void UpdateObservableCollection(ObservableCollection<T> collection, IEnumerable<T> items)
        {
            collection.Clear();
            foreach (T item in items)
            {
                collection.Add(item);
            }
        }
    }
}
