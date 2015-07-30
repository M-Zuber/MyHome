using System;

namespace MyHome.DataClasses
{
    public class Category
    {
        public Category()
        {
        }

        public Category(int id, string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("name");
            }

            Id = id;
            Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}
