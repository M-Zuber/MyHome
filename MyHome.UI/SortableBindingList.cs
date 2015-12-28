using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace MyHome.UI
{
    class SortableBindingList<T> : BindingList<T>
    {
        CustomComparer comparer;

        protected override bool SupportsSortingCore
        {
            get { return true; }
        }

        bool isSorted = false;
        protected override bool IsSortedCore
        {
            get { return isSorted; }
        }

        PropertyDescriptor propertyDescriptor;
        protected override PropertyDescriptor SortPropertyCore
        {
            get { return propertyDescriptor; }
        }

        private ListSortDirection listSortDirection;
        protected override ListSortDirection SortDirectionCore
        {
            get { return listSortDirection; }
        }



        public SortableBindingList() : this(new Tuple<string, ListSortDirection>[0], new List<T>()) { }
        public SortableBindingList(IEnumerable<Tuple<string, ListSortDirection>> props) : this(props, new List<T>()) { }
        public SortableBindingList(IEnumerable<Tuple<string, ListSortDirection>> props, List<T> list)
            : base(list)
        {
            // Build Comparer chain
            var t = TypeDescriptor.GetProperties(typeof(T));

            foreach (var prop in props)
            {
                var p = t.Find(prop.Item1, false);
                if (p == null)
                    throw new ArgumentException("The property \"" + prop.Item1 + "\" was not found on " + typeof(T).FullName + ".", "prop");

                isSorted = true;
                propertyDescriptor = p;
                listSortDirection = prop.Item2;
                comparer = new CustomComparer(comparer, p, prop.Item2);
            }

            // Wrap in final Comparer for user sorting
            comparer = new CustomComparer(comparer);

            var items = Items as List<T>;
            items.Sort(comparer);
        }

        public void Load(IEnumerable<T> collection)
        {
            var data = Items as List<T>;
            data.Clear();
            data.AddRange(collection);
            data.Sort(comparer);
            OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
        {
            var items = Items as List<T>;

            comparer.SetSort(prop, direction);
            items.Sort(comparer);

            propertyDescriptor = prop;
            listSortDirection = direction;
            isSorted = true;
            OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        protected override int FindCore(PropertyDescriptor prop, object key)
        {
            for (int i = 0, count = Count; i < count; i++)
            {
                if (prop.GetValue(this[i]).Equals(key))
                {
                    return i;
                }
            }
            return -1;
        }



        private class CustomComparer : IComparer<T>
        {
            static readonly Dictionary<PropertyDescriptor, IComparer> comparerCache = new Dictionary<PropertyDescriptor, IComparer>();

            readonly CustomComparer baseComparer;
            IComparer comparer;
            public ListSortDirection Direction { get; set; }
            public PropertyDescriptor Property { get; private set; }

            public CustomComparer(PropertyDescriptor prop, ListSortDirection direction) : this(null, prop, direction) { }
            public CustomComparer(CustomComparer baseComparer) : this(baseComparer, null, ListSortDirection.Ascending) { }
            public CustomComparer(CustomComparer baseComparer, PropertyDescriptor prop, ListSortDirection direction)
            {
                this.baseComparer = baseComparer;
                Direction = direction;
                Property = prop;
                comparer = GetComparer(prop);
            }

            public void SetSort(PropertyDescriptor prop, ListSortDirection direction)
            {
                Direction = direction;

                if (baseComparer != null && prop == baseComparer.Property)
                {
                    baseComparer.Direction = direction;
                    prop = null;
                }

                Property = prop;
                comparer = GetComparer(prop);
            }

            public int Compare(T x, T y)
            {
                if (comparer != null)
                {
                    var result = comparer.Compare(Property.GetValue(x), Property.GetValue(y));
                    if (Direction == ListSortDirection.Descending)
                        result = -result;

                    if (result != 0)
                        return result;
                }

                if (baseComparer != null)
                    return baseComparer.Compare(x, y);

                return 0;
            }

            IComparer GetComparer(PropertyDescriptor prop)
            {
                if (prop == null) return null;
                if (comparerCache.ContainsKey(prop)) return comparerCache[prop];

                var propComparer = typeof(Comparer<>).MakeGenericType(prop.PropertyType);
                var result = propComparer.InvokeMember("Default", BindingFlags.Static | BindingFlags.GetProperty | BindingFlags.Public, null, null, null) as IComparer;
                comparerCache.Add(prop, result);
                return result;
            }
        }
    }
}
