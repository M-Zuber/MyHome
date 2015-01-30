using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LocalTypes
{
    public interface IQuery<T>
    {
        T LoadById(int id);
        List<T> LoadAll();
    }

    public interface IRepository<T> : IQuery<T>
    {
        T Save(T item);
    }

    public interface IRemovableRepository<T>
    {
        void Remove(T item);
        void Remove(int item);
    }

    public interface ITransactionRepository<T> : IRepository<T>, IRemovableRepository<T>
    {
        List<T> LoadMonth(DateTime month);
    }
}
