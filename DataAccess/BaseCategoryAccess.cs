using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LocalTypes;

namespace DataAccess
{
    public abstract class BaseCategoryAccess<T> where T : BaseCategory
    {
        #region Abstract Methods

        public virtual List<T> LoadAll()
        {
            return null;
        }

        internal virtual void UpdateDataBase(T categoryTranslating) { }

        public virtual int AddNewCategory(string categoryName)
        {
            return -1;
        }

        #endregion

        #region Implemented Methods

        public bool Save(T categoryToSave)
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

        internal int GetNextId()
        {
            return this.LoadAll().Max(ct => (int?)ct.Id).GetValueOrDefault(0) + 1;
        }

        #endregion
    }
}
