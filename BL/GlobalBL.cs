using System.Collections.Generic;
using DA;

namespace BL
{
    /// <summary>
    /// Holds data members and methods that are used across the program
    /// </summary>
    public class GlobalBL
    {
        #region Data Members
        
        // Const members

        /// <summary>
        /// Dictionary with the name of the category tables -as set in the db
        /// </summary>
        public static Dictionary<int, string> CategoryTypeTableNames =
            new Dictionary<int, string>()
            {
                {1, "t_expenses_category"},
                {2, "t_incomes_category"},
                {3, "t_payment_methods"}
            };

        /// <summary>
        /// Dictionary with class instances of each category type
        /// -starts with null, appropiate values are added during runtime
        /// </summary>
        public static Dictionary<int, BaseBL> CategoryTypes =
            new Dictionary<int, BaseBL>()
            {
                {1, null},
                {2, null},
                {3, null}
            }; 

        #endregion

        #region Other Methods

        /// <summary>
        /// Intializes the cache and sets up local global variables for the program
        /// </summary>
        public static void IntializeData()
        {
            // Loads all the tables one at a time -but in the appropiate order
            GlobalBL.LoadToCache();

            // Intializes variables with values for new ids
            BaseDA.SetNewIdStart();
        }

        /// <summary>
        /// Loads all the tables into the cache
        /// </summary>
        public static void LoadToCache()
        {
            // Loads all the tbales one at a time -but in the appropiate order
            BaseDA.LoadToCache();
        }

        /// <summary>
        /// Updates the dictonary with the variables for new category items
        /// </summary>
        public static void UpdateCatDictionary()
        {
            GlobalBL.CategoryTypes[1] = ExpCatBL.CreateExpensesCat();
            GlobalBL.CategoryTypes[2] = IncCatBL.CreateIncomeCat();
            GlobalBL.CategoryTypes[3] = MethodBL.CreatePaymentMethodCat();
        }

        /// <summary>
        /// Loads the table with the given name to the cache
        /// </summary>
        /// <param name="strTableName">The name of the table to be loaded</param>
        public static void LoadToCache(string strTableName)
        {
            // Call the appropiate method from the DAL, sending the table name
            // given as a parameter
            BaseDA.LoadToCache(strTableName);
        }

        /// <summary>
        /// Saves the data from the cache into the db
        /// </summary>
        public static void SaveFromCache()
        {
            // Saves the tables in the cache to the db, one at a time, in the appropiate order
            BaseDA.SaveFromCache("t_expenses_category");
            BaseDA.SaveFromCache("t_incomes_category");
            BaseDA.SaveFromCache("t_payment_methods");
            BaseDA.SaveFromCache("t_expenses");
            BaseDA.SaveFromCache("t_incomes");
        }

        /// <summary>
        /// Creates an int indexed dictionary with lists of all the names per category type
        /// </summary>
        /// <returns>The dictionary filled in</returns>
        public static Dictionary<int, List<string>> GetAllCatNames()
        {
            // Creates a dictionary with a list of all the names per category type
            Dictionary<int, List<string>> CategoryNames =
                new Dictionary<int, List<string>>
                {
                    {1, ExpCatBL.GetAllNames()},
                    {2, IncCatBL.GetAllNames()},
                    {3, MethodBL.GetAllNames()}
                };

            // Returns the dictionary
            return CategoryNames;
        }

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

        #endregion
    }
}
