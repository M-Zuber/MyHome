using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public static class GeneralCategoryHandler
    {
        public static List<string> GetAllCategoryNames()
        {
            List<string> allCategoryNames = new List<string>();

            allCategoryNames.Add("Total Expenses");
            allCategoryNames.AddRange((new ExpenseCategoryHandler()).LoadAll().Select(cat => cat.Name).ToList<string>());

            allCategoryNames.Add("Total Income");
            foreach (string incomeCategoryName in (new IncomeCategoryHandler()).LoadAll().Select(cat => cat.Name).ToList<string>())
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

        public static List<string> GetAllCategoryNames(string categoryType)
        {
            switch (categoryType.ToLower())
            {
                case ("expense"):
                {
                    return (new ExpenseCategoryHandler()).LoadAll().Select(exCat => exCat.Name).ToList<string>();
                }
                case ("income"):
                {
                    return (new IncomeCategoryHandler()).LoadAll().Select(inCat => inCat.Name).ToList<string>();
                }
                default:
                {
                    return null;
                }
            }
        }
    }
}
