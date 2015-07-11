using System.Collections;
using System.Collections.Generic;
using MyHome.DataClasses;

namespace MyHome.Services
{
    public interface ICategoryService
    {
        bool Exists(string name);
        void Add(string name);
        IEnumerable<Category> LoadAll();
    }
}
