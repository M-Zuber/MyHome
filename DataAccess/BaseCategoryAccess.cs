using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LocalTypes;

namespace DataAccess
{
    public abstract class BaseCategoryAccess
    {
        public abstract List<BaseCategory> LoadAll();

        internal abstract void UpdateDataBase(BaseCategory categoryTranslating);

        public bool Save(BaseCategory categoryToSave)
        {
            try
            {
                UpdateDataBase(categoryToSave);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
