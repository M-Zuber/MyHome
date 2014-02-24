using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LocalTypes;
using DataAccess;

namespace BusinessLogic
{
    public class ExpenseHandler
    {
        #region Properties

        //TODO Maybe this should be private?
        public List<Expense> AllExpenses { get; set; }

        #endregion

        #region C'Tor

        public ExpenseHandler()
        {
            this.AllExpenses = LoadAll();
        }

        #endregion

        #region CRUD Methods

        #region Read Methods

        public static Expense LoadById(int id)
        {
            return ExpenseAccess.LoadById(id);
        }

        public static List<Expense> LoadAll()
        {
            return ExpenseAccess.LoadAll();
        }

        public List<Expense> LoadOfMonth(DateTime monthWanted)
        {
            List<Expense> allExpensesCategories = new List<Expense>();

            foreach (Expense currExpense in this.AllExpenses)
            {
                if ((currExpense.Date.Month == monthWanted.Month) &&
                    (currExpense.Date.Year == monthWanted.Year))
                {
                    allExpensesCategories.Add(currExpense);
                }
            }

            return allExpensesCategories;
        }

        #endregion

        #region Update Methods
        
        public static void Save(Expense expenseToSave)
        {
            ExpenseAccess.Save(expenseToSave);
        }

        #endregion

        #endregion
    }
}
