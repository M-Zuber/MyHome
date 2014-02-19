using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameWork;

namespace DataAccess
{
    public class ExpenseCategoryEntity
    {
        #region Properties

        /// <summary>
        /// The name (type) of the expense category
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Id number of the expense category in the table
        /// </summary>
        public int ID { get; internal set; }

        #endregion

        #region C'Tor

        public ExpenseCategoryEntity(int id, string name)
        {
            this.ID = id;
            this.Name = name;
        }

        #endregion

        #region CRUD Methods

        #region Read Methods

        public static ExpenseCategoryEntity LoadById(uint id)
        {
            StaticDataSet.t_expenses_categoryRow requestedRow = 
                Cache.SDB.t_expenses_category.FindByID((uint)id);
            return new ExpenseCategoryEntity((int)requestedRow.ID, requestedRow.NAME);
        }

        public static List<ExpenseCategoryEntity> LoadAll()
        {
            List<ExpenseCategoryEntity> allExpensesCategories = new List<ExpenseCategoryEntity>();

            foreach (StaticDataSet.t_expenses_categoryRow currExpenseCategory in Cache.SDB.t_expenses_category.Rows)
            {
                allExpensesCategories.Add(
                    new ExpenseCategoryEntity((int)currExpenseCategory.ID, currExpenseCategory.NAME));
            }

            return allExpensesCategories;
        }

        #endregion

        #endregion

        #region Other Methods

        #region Override Methods

        public override string ToString()
        {
            return this.Name;
        }

        #endregion

        #endregion
    }
}
