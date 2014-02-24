using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LocalTypes;
using DataAccess;

namespace BusinessLogic
{
    public class ExpenseCategoryHandler
    {
        #region CRUD Methods

        #region Read Methods

        public static ExpenseCategory LoadById(uint id)
        {
            return ExpenseCategoryAccess.LoadById(id);
        }

        public static List<ExpenseCategory> LoadAll()
        {
            return ExpenseCategoryAccess.LoadAll();
        }

        #endregion

        #endregion
    }
}
