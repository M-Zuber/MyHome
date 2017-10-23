using System.Collections.Generic;
using MyHome.DataClasses;

namespace MyHome.Services
{
    public interface ICategoryService<out TCategoryType>
    {
        IEnumerable<Category> GetAll();
        bool Exists(string name);
        TCategoryType Create(string name, int id = 0);
        void Delete(string name);
        void Save(int id, string name);
    }
}
