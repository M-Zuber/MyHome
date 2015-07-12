using System.Collections.Generic;
using MyHome.DataClasses;

namespace MyHome.Services
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAll();
        bool Exists(string name);
        void Add(string name);
        void Remove(string name);
        void Update(int id, string name);
    }
}
