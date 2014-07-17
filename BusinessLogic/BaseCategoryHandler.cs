using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LocalTypes;

namespace BusinessLogic
{
    public abstract class BaseCategoryHandler
    {
        #region Abstract Method

        public abstract List<BaseCategory> LoadAll();

        public abstract bool Save(BaseCategory categoryToSave);

        public abstract int AddNewCategory(string categoryName);

        #endregion

        #region Implemented Methods

        public bool DoesNameExist(string categoryName)
        {
            return this.LoadAll().Exists(cT => cT.Name.ToLower() == categoryName.ToLower());
        }

        #endregion
    }
}
