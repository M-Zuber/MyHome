using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace MyHome.UI
{
    internal class SortableBindingList<T> : BindingList<T>
    {
        readonly CustomComparer _comparer;

        protected override bool SupportsSortingCore => true;

        private bool _isSorted;
        protected override bool IsSortedCore => _isSorted;

        private PropertyDescriptor _propertyDescriptor;
        protected override PropertyDescriptor SortPropertyCore => _propertyDescriptor;

        private ListSortDirection _listSortDirection;
        protected override ListSortDirection SortDirectionCore => _listSortDirection;


        public SortableBindingList() : this(new(string, ListSortDirection)[0], new List<T>()) { }
        public SortableBindingList(IEnumerable<(string, ListSortDirection)> props) : this(props, new List<T>()) { }
        public SortableBindingList(IEnumerable<(string, ListSortDirection)> props, IList<T> list)
            : base(list)
        {
            // Build Comparer chain
            var t = TypeDescriptor.GetProperties(typeof(T));

            foreach (var prop in props)
            {
                var p = t.Find(prop.Item1, false);

                _isSorted = true;
                _propertyDescriptor = p ?? throw new ArgumentException($"The property \"{prop.Item1}\" was not found on {typeof(T).FullName}.", nameof(prop));
                _listSortDirection = prop.Item2;
                _comparer = new CustomComparer(_comparer, p, prop.Item2);
            }

            // Wrap in final Comparer for user sorting
            _comparer = new CustomComparer(_comparer);

            if (Items is List<T> items)
            {
                items.Sort(_comparer);
            }
        }

        public void Load(IEnumerable<T> collection)
        {
            if (Items is List<T> data)
            {
                data.Clear();
                data.AddRange(collection);
                data.Sort(_comparer);
            }
            OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
        {
            var items = Items as List<T>;

            _comparer.SetSort(prop, direction);
            items?.Sort(_comparer);

            _propertyDescriptor = prop;
            _listSortDirection = direction;
            _isSorted = true;
            OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        protected override int FindCore(PropertyDescriptor prop, object key)
        {
            for (int i = 0, count = Count; i < count; i++)
            {
                if (prop.GetValue(this[i])?.Equals(key) == true)
                {
                    return i;
                }
            }
            return -1;
        }



        private class CustomComparer : IComparer<T>
        {
            // ReSharper disable once StaticMemberInGenericType
            private static readonly Dictionary<PropertyDescriptor, IComparer> ComparerCache = new Dictionary<PropertyDescriptor, IComparer>();
            private readonly CustomComparer _baseComparer;
            private IComparer _comparer;
            private ListSortDirection Direction { get; set; }
            private PropertyDescriptor Property { get; set; }

            public CustomComparer(CustomComparer baseComparer, PropertyDescriptor prop = null, ListSortDirection direction = ListSortDirection.Ascending)
            {
                _baseComparer = baseComparer;
                Direction = direction;
                Property = prop;
                _comparer = GetComparer(prop);
            }

            public void SetSort(PropertyDescriptor prop, ListSortDirection direction)
            {
                Direction = direction;

                if (_baseComparer != null && _baseComparer.Property.Equals(prop))
                {
                    _baseComparer.Direction = direction;
                    prop = null;
                }

                Property = prop;
                _comparer = GetComparer(prop);
            }

            public int Compare(T x, T y)
            {
                if (_comparer != null && x != null && y != null)
                {
                    var result = _comparer.Compare(Property.GetValue(x), Property.GetValue(y));
                    if (Direction == ListSortDirection.Descending)
                        result = -result;

                    if (result != 0)
                        return result;
                }

                return _baseComparer?.Compare(x, y) ?? 0;
            }

            private static IComparer GetComparer(PropertyDescriptor prop)
            {
                if (prop == null) return null;
                if (ComparerCache.ContainsKey(prop)) return ComparerCache[prop];

                var propComparer = typeof(Comparer<>).MakeGenericType(prop.PropertyType);
                var result = propComparer.InvokeMember("Default", BindingFlags.Static | BindingFlags.GetProperty | BindingFlags.Public, null, null, null) as IComparer;
                ComparerCache.Add(prop, result);
                return result;
            }
        }
    }
}
