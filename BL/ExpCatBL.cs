﻿using System.Collections.Generic;
using System.Data;
using DA;
using FrameWork;

namespace BL
{
    /// <summary>
    /// Entity representation of an expense category
    /// </summary>
    public  class ExpCatBL : BaseBL
    {
        #region Properties

        /// <summary>
        /// The name (type) of the expense category
        /// </summary>
        public override string Name { get; set; }

        /// <summary>
        /// Id number of the expense category in the table
        /// </summary>
        public override int ID { get; internal set; }
        
        #endregion

        #region C'tor

        /// <summary>
        /// Private ctor for the class that initializes the ID property
        /// with the given value
        /// </summary>
        /// <param name="nID">The id of the expense category being created</param>
        private ExpCatBL(int nID)
        {
            this.ID = nID;
        }
        
        #endregion

        #region Other Methods

        /// <summary>
        /// Saves the current expense category into the table in the cache
        /// -Deals with updating and creating new rows
        /// </summary>
        public override void Save()
        {
            // Creates a variable to contain the data in the format neccessary to
            // add it to the table
            DataRow drExpenseCat =
                Cache.SDB.t_expenses_category.Rows.Find(this.ID);

            // Checks if the category is already in the table
            // If the category already exists
            if (drExpenseCat != null)
            {
                // Updates the name of the category
                drExpenseCat["NAME"] = this.Name;
            }
            // If the category does not exist yet
            else
            {
                // Adds a new row to the table in the cache with the wanted Id and name
                Cache.SDB.t_expenses_category.
                    Addt_expenses_categoryRow((uint)this.ID, this.Name);
            }
        }

        /// <summary>
        /// Loads all the expenses categories from the cache and adds them
        /// to a dictionary in entity form
        /// </summary>
        /// <returns>A list of all the expenses categories in a sorted dictionary</returns>
        public static SortedDictionary<int, ExpCatBL> GetAll()
        {
            // Intializes the return variable
            SortedDictionary<int, ExpCatBL> srtAllExpensesCat =
                new SortedDictionary<int, ExpCatBL>();

            // Goes over every row in the table in the cache
            foreach (StaticDataSet.t_expenses_categoryRow currRow in Cache.SDB.t_expenses_category)
            {
                // Adds the row to the dictionary, creating the entity as it gets added
                srtAllExpensesCat.Add(int.Parse(currRow["ID"].ToString()),
                                  Load(int.Parse(currRow["ID"].ToString())));
            }

            // Returns the list to the calling function
            return (srtAllExpensesCat);
        }
        
        /// <summary>
        /// Creates a new entity of an expense category 
        /// -Using the sequence in the db for the expense categories table
        /// </summary>
        /// <returns>A new expense category with only the ID property filled</returns>
        public static ExpCatBL CreateExpensesCat()
        {
            // Creates a return variable with a default value of null
            ExpCatBL expNewExpenseCat = null;

            // Tries intializing the id field with a value from the db
            try
            {
                // Reads the value from the db
                int nNewId = BaseDA.GetNextVal("t_expenses_category");

                // Intializes the return variable with the ID value pulled
                expNewExpenseCat = new ExpCatBL(nNewId);
            }
            // If there was any error, stops it at this level
            catch{}

            // Returns the intialized variable to the calling function
            return (expNewExpenseCat);
        }

        /// <summary>
        /// Loads an expense category entity from the cache based on the given id
        /// </summary>
        /// <param name="nId">The id of the expense category to load</param>
        /// <returns>An entity represntation of the expense category asked for</returns>
        public static ExpCatBL Load(int nId)
        {
            // Creates a return variable with a default value of null
            ExpCatBL expLoadExpenseCat = null;

            // Before loading checks that the wanted id is in the table
            if (Cache.SDB.t_expenses_category.Rows.Contains(nId))
            {
                // Intializes the variable with the ctor, and sets the id property
                expLoadExpenseCat = new ExpCatBL(nId);

                // Pulls the row of the wanted expense category from the cache
                DataRow drExpenseCat =
                    Cache.SDB.t_expenses_category.Rows.Find(nId);

                // Sets the name property based on the data in the row
                expLoadExpenseCat.Name = drExpenseCat["NAME"].ToString();
            }

            // Returns the variable to the calling function
            return (expLoadExpenseCat);
        }

        /// <summary>
        /// Gets a list of all the expense category names
        /// </summary>
        /// <returns>The list of expense category names</returns>
        public static List<string> GetAllNames()
        {
            // Creates  a return variable
            List<string> lsNames = new List<string>();

            // Goes over the dictionary of all the expense categories
            foreach (KeyValuePair<int, ExpCatBL> CurrExpCat in ExpCatBL.GetAll())
            {
                // Adds the name of the current categoy to the list
                lsNames.Add(CurrExpCat.Value.Name);
            }

            // Returns the list to the calling function
            return lsNames;
        }

        #endregion
    }
}
