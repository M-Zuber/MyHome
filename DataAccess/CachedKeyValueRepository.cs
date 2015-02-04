using MyHome2013.Core.LocalTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess
{
    public class CachedKeyValueRepository<T> : IRepository<T>
            where T : BaseCategory
    {
        Dictionary<int, T> cache = new Dictionary<int, T>();
        IRepository<T> source;

        public CachedKeyValueRepository(IRepository<T> source)
        {
            this.source = source;
        }

        public T LoadById(int id)
        {
            if (!cache.ContainsKey(id))
                cache[id] = source.LoadById(id);

            return cache[id];
        }

        public List<T> LoadAll()
        {
            return source.LoadAll();
        }

        public T Save(T item)
        {
            var result = source.Save(item);

            // If the save fails and the item is not new, remove the item from the cache
            if (result == null && item.Id != default(int) && cache.ContainsKey(item.Id))
                cache.Remove(item.Id);

            // If the save succeeded, update the cache
            if (result != null)
                cache[result.Id] = result;

            return result;
        }
    }
}
