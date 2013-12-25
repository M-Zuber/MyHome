using System;
using System.Collections.Generic;
using System.Data;
using DA;
using FrameWork;

namespace BL
{
    /// <summary>
    /// Entity representation of an income category
    /// </summary>
    public class IncCatBL : BaseBL
    {
        #region Properties

        /// <summary>
        /// The name (type) of the income category
        /// </summary>
        public override string Name { get; set; }

        /// <summary>
        /// Id number of the income category in the table
        /// </summary>
        public override int ID { get; internal set; }

        #endregion

        #region C'tor

        /// <summary>
        /// Private ctor for the class that initializes the ID property
        /// with the given value
        /// </summary>
        /// <param name="nID">The id of the income category being created</param>
        private IncCatBL(int nID)
        {
            this.ID = nID;
        }
        
        #endregion

        #region Other Methods

        /// <summary>
        /// Saves the current income category into the table in the cache
        /// -Deals with updating and creating new rows
        /// </summary>
        public override void Save()
        {
            // Creates a variable to contain the data in the format neccessary to
            // add it to the table            
            DataRow drIncomeCat =
                Cache.SDB.t_incomes_category.Rows.Find(this.ID);

            // Checks if the category is already in the table
            // If the category already exists
            if (drIncomeCat != null)
            {
                Globals.LogFiles["BusinessLayerLog"].AddMessages(
                                    "Updating existing income category with id of: " + this.ID,
                                    DateTime.Today.ToString());

                // Updates the name of the category
                drIncomeCat["NAME"] = this.Name;
            }
            // If the category does not exist yet
            else
            {
                Globals.LogFiles["BusinessLayerLog"].AddMessages(
                                        "Creating new income category with id of: " + this.ID,
                                        DateTime.Today.ToString());

                // Adds a new row to the table in the cache with the wanted Id and name
                Cache.SDB.t_incomes_category.
                    Addt_incomes_categoryRow(this.ID, this.Name);
            }
        }

        /// <summary>
        /// Loads all the income categories from the cache and adds them
        /// to a dictionary in entity form
        /// </summary>
        /// <returns>A list of all the income categories in a sorted dictionary</returns>
        public static SortedDictionary<int, IncCatBL> GetAll()
        {
            // Intializes the return variable
            SortedDictionary<int, IncCatBL> srtAllIncomesCat =
                new SortedDictionary<int, IncCatBL>();

            int rowsInCache = Cache.SDB.t_incomes_category.Rows.Count;
            int rowsPulled = 0;

            // Goes over every row in the table in the cache
            foreach (StaticDataSet.t_incomes_categoryRow currRow in Cache.SDB.t_incomes_category)
            {
                // Adds the row to the dictionary, creating the entity as it gets added
                srtAllIncomesCat.Add(int.Parse(currRow["ID"].ToString()),
                                  Load(int.Parse(currRow["ID"].ToString())));
            }

            if (rowsInCache != rowsPulled)
            {
                Globals.LogFiles["ErrorLog"].AddError(Globals.ErrorCodes.BL_ERROR,
                    "The amount in the cache is:" + rowsInCache +
                                        " but only " + rowsPulled + " expenses where pulled",
                    DateTime.Today);
            }

            // Returns the list to the calling function
            return (srtAllIncomesCat);
        }

        /// <summary>
        /// Creates a new entity of an income category 
        /// -Using the sequence in the db for the income categories table
        /// </summary>
        /// <returns>A new income category with only the ID property filled</returns>
        public static  IncCatBL CreateIncomeCat()
        {
            // Creates a return variable with a default value of null
            IncCatBL intNewIncCat = null;

            // Tries intializing the id field with a value from the db
            try
            {
                // Reads the value from the db
                int nNewId = BaseDA.GetNextVal("t_incomes_category");

                // Intializes the return variable with the ID value pulled
                intNewIncCat = new IncCatBL(nNewId);
            }
            // If there was any error, stops it at this level
            catch (Exception e)
            {
                Globals.LogFiles["ErrorLog"].AddError(Globals.ErrorCodes.SQL_ERROR,
                                                            e.Message, DateTime.Now);
                Globals.LogFiles["ErrorLog"].AddMessage(e.StackTrace);
            }

            // Returns the intialized variable to the calling function
            return (intNewIncCat);
        }

        /// <summary>
        /// Loads an income category entity from the cache based on the given id
        /// </summary>
        /// <param name="nId">The id of the income category to load</param>
        /// <returns>An entity represntation of the income category asked for</returns>
        public static IncCatBL Load(int nId)
        {
            // Creates a return variable with a default value of null
            IncCatBL incLoadIncCat = null;

            // Before loading checks that the wanted id is in the table
            if (Cache.SDB.t_incomes_category.Rows.Contains(nId))
            {
                // Intializes the variable with the ctor, and sets the id property
                incLoadIncCat = new IncCatBL(nId);

                // Pulls the row of the wanted income category from the cache
                DataRow drIncomeCat =
                    Cache.SDB.t_incomes_category.Rows.Find(nId);

                // Sets the name property based on the data in the row
                incLoadIncCat.Name = drIncomeCat["NAME"].ToString();
            }
            else
            {
                Globals.LogFiles["ErrorLog"].AddError(Globals.ErrorCodes.BL_ERROR,
                   "Attempt to pull non exsistent expense with an id of:" + nId,
                   DateTime.Today);
            }

            // Returns the variable to the calling function
            return (incLoadIncCat);
        }

        /// <summary>
        /// Gets a list of all the income category names
        /// </summary>
        /// <returns>The list of income category names</returns>
        public static List<string> GetAllNames()
        {
            // Creates  a return variable
            List<string> lsNames = new List<string>();

            // Goes over the dictionary of all the income categories
            foreach (KeyValuePair<int, IncCatBL> CurrIncCat in IncCatBL.GetAll())
            {
                // Adds the name of the current categoy to the list
                lsNames.Add(CurrIncCat.Value.Name);
            }

            // Returns the list to the calling function
            return lsNames;
        }

        #endregion
    }
}
