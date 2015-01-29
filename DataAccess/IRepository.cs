using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess
{
    public interface IRepository<T, TKey>
    {
        T LoadById(TKey id);
        List<T> LoadAll();
        T Save(T item);
    }

    public interface IRemovableRepository<T, TKey>
    {
        void Remove(T item);
        void Remove(TKey item);
    }

    public interface ITransactionRepository<T, TKey> : IRepository<T, TKey>, IRemovableRepository<T, TKey>
    {
        List<T> LoadMonth(DateTime month);
    }
}
