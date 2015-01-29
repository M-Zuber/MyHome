using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess;
using BusinessLogic;

namespace MyHome2013
{
    public class HelperMethods
    {
        //TODO should this be in the bl or the ui??
        /// <summary>
        /// Checks if the given string is a floating point number
        /// </summary>
        /// <param name="strText">The string to be checked</param>
        /// <returns>The result of the check</returns>
        public static bool IsNumeric(string strText)
        {
            double dbToParse;
            return double.TryParse(strText, out dbToParse);
        }

        public static List<string> GetAllCategoryNames()
        {
            var ech = Program.Container.GetInstance<ExpenseCategoryHandler>();
            var ich = Program.Container.GetInstance<IncomeCategoryHandler>();

            var allCategoryNames = new List<string>();

            allCategoryNames.Add("Total Expenses");
            allCategoryNames.AddRange(ech.LoadAll().Select(cat => cat.Name).ToList<string>());

            allCategoryNames.Add("Total Income");
            foreach (string incomeCategoryName in ich.LoadAll().Select(cat => cat.Name).ToList<string>())
            {
                if (allCategoryNames.Contains(incomeCategoryName))
                {
                    allCategoryNames[allCategoryNames.IndexOf(incomeCategoryName)] = string.Format("{0} - {1}", incomeCategoryName, "Expense");
                    allCategoryNames.Add(string.Format("{0} - {1}", incomeCategoryName, "Income"));
                }
                else
                {
                    allCategoryNames.Add(incomeCategoryName);
                }
            }

            return allCategoryNames;
        }
    }
}
