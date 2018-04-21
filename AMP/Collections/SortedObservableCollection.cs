using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AMP.Collections
{
    public enum SortDirection
    {
        Ascending,
        Descending
    }

    public class SortableObservableCollection<T> : ObservableCollection<T>
    {

        Func<T, string> _funcString;
        Func<T, int> _funcInt;

        private SortDirection _sortDirection;
        private Func<T, DateTimeOffset> _funcDate;
        private Func<T, TimeSpan> _funcTime;
        private Func<T, bool> _funcBool;

        public void Sort(Func<T, string> keySelector, SortDirection direction = SortDirection.Ascending)
        {

            _funcString = keySelector;
            _sortDirection = direction;
            switch (direction)
            {
                case SortDirection.Ascending:
                    {
                        ApplySort(Items.OrderBy(keySelector));
                        break;
                    }
                case SortDirection.Descending:
                    {
                        ApplySort(Items.OrderByDescending(keySelector));
                        break;
                    }
            }
        }

        public void Sort(Func<T, int> keySelector, SortDirection direction = SortDirection.Ascending)
        {

            _funcInt = keySelector;
            _sortDirection = direction;
            switch (direction)
            {
                case SortDirection.Ascending:
                    {
                        ApplySort(Items.OrderBy(keySelector));
                        break;
                    }
                case SortDirection.Descending:
                    {
                        ApplySort(Items.OrderByDescending(keySelector));
                        break;
                    }
            }
        }

        public void Sort(Func<T, DateTimeOffset> keySelector, SortDirection direction = SortDirection.Ascending)
        {

            _funcDate = keySelector;
            _sortDirection = direction;
            switch (direction)
            {
                case SortDirection.Ascending:
                    {
                        ApplySort(Items.OrderBy(keySelector));
                        break;
                    }
                case SortDirection.Descending:
                    {
                        ApplySort(Items.OrderByDescending(keySelector));
                        break;
                    }
            }
        }

        public void Sort(Func<T, TimeSpan> keySelector, SortDirection direction = SortDirection.Ascending)
        {

            _funcTime = keySelector;
            _sortDirection = direction;
            switch (direction)
            {
                case SortDirection.Ascending:
                    {
                        ApplySort(Items.OrderBy(keySelector));
                        break;
                    }
                case SortDirection.Descending:
                    {
                        ApplySort(Items.OrderByDescending(keySelector));
                        break;
                    }
            }
        }

        public void Sort(Func<T, bool> keySelector, SortDirection direction = SortDirection.Ascending)
        {

            _funcBool = keySelector;
            _sortDirection = direction;
            switch (direction)
            {
                case SortDirection.Ascending:
                    {
                        ApplySort(Items.OrderBy(keySelector));
                        break;
                    }
                case SortDirection.Descending:
                    {
                        ApplySort(Items.OrderByDescending(keySelector));
                        break;
                    }
            }
        }



        private void ApplySort(IEnumerable<T> sortedItems)
        {
            var sortedItemsList = sortedItems.ToList();

            foreach (var item in sortedItemsList)
            {
                Move(IndexOf(item), sortedItemsList.IndexOf(item));
            }
        }

        protected override void InsertItem(int index, T item)
        {
            base.InsertItem(index, item);
            if (_funcInt != null)
            {
                Sort(_funcInt, _sortDirection);
                return;
            }
            if (_funcString != null)
            {
                Sort(_funcString, _sortDirection);
                return;
            }
            if (_funcDate != null)
            {
                Sort(_funcDate, _sortDirection);
                return;
            }
            if (_funcBool != null)
            {
                Sort(_funcBool, _sortDirection);
                return;
            }
            if (_funcTime != null)
            {
                Sort(_funcTime, _sortDirection);
                return;
            }
            
        }

        protected override void SetItem(int index, T item)
        {
            this.InsertItem(index, item);
        }
    }
}
