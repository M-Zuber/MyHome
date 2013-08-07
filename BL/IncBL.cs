using System;
using System.Collections.Generic;
using System.Data;
using DA;
using FrameWork;

namespace BL
{
    /// <summary>
    /// Entity representation of an income
    /// </summary>
    public class IncBL
    {
        #region Properties

        /// <summary>
        /// The amount of the income
        /// </summary>
        public string Amount { get; set; }

        /// <summary>
        /// The date of the income
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Category of the income
        /// </summary>
        public int Category { get; set; }

        /// <summary>
        /// How the income was payed
        /// </summary>
        public int Method { get; set; }

        /// <summary>
        /// Additional info about the income
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// ID number of the income in the data table
        /// </summary>
        public int ID { get; private set; }
        
        #endregion

        #region C'tor

        /// <summary>
        /// Private ctor for the class that initializes the ID property
        /// with the given value
        /// </summary>
        /// <param name="nId">The id of the income being created</param>
        private IncBL(int nId)
        {
            this.ID = nId;
        }
        
        #endregion

        #region Other Methods

        /// <summary>
        /// Saves the current income into the table in the cache
        /// -Deals with updating and creating new rows
        /// </summary>
        public void Save()
        {
            // Variable to contain the data of the income in the format
            // neccessary to add it to the table
            StaticDataSet.t_incomesRow drIncome = 
                (StaticDataSet.t_incomesRow)Cache.SDB.t_incomes.Rows.Find(this.ID);

            // Checks if the income already exists 
            // -If so performs an update
            if (drIncome != null)
            {
                // Updates each field with the data in the members of the class
                drIncome["AMOUNT"] = this.Amount;
                drIncome["INC_DATE"] = this.Date;
                drIncome["CATEGORY"] = this.Category;
                drIncome["METHOD"] = this.Method;
                drIncome["COMMENTS"] = this.Comment;
            }
            // If a new row is being added
            else
            {
                // Intializes the variable with an empty skeleton of the row format
                // and adds in the data based on the members of the class
                drIncome = Cache.SDB.t_incomes.Newt_incomesRow();
                drIncome["ID"] = this.ID;
                drIncome["AMOUNT"] = this.Amount;
                drIncome["INC_DATE"] = this.Date;
                drIncome["CATEGORY"] = this.Category;
                drIncome["METHOD"] = this.Method;
                drIncome["COMMENTS"] = this.Comment;

                // Adds the row to the table
                Cache.SDB.t_incomes.Addt_incomesRow(drIncome);
            }
        }

        /// <summary>
        /// Loads all the incomes from the cache and adds them
        /// to a dictionary in entity form
        /// </summary>
        /// <returns>A list of all the incomes in a sorted dictionary</returns>
        public static SortedDictionary<int, IncBL> GetAll()
        {
            // Intializes the return variable
            SortedDictionary<int, IncBL> srtAllIncomes =
                new SortedDictionary<int, IncBL>();

            // Goes over every row in the table in the cache
            foreach (StaticDataSet.t_incomesRow currRow in Cache.SDB.t_incomes)
            {
                // Adds the row to the dictionary, creating the entity as it gets added
                srtAllIncomes.Add(int.Parse(currRow["ID"].ToString()),
                                  Load(int.Parse(currRow["ID"].ToString())));
            }

            // Returns the list to the calling function
            return (srtAllIncomes);
        }

        /// <summary>
        /// Creates a new entity of an income -Using the sequence in the db
        /// for the income table
        /// </summary>
        /// <returns>A new income with only the ID property filled</returns>
        public static IncBL CreateIncome()
        {
            // Creates a return variable with a default value of null
            IncBL expNewIncome = null;

            // Tries intializing the id field with a value from the db
            try
            {
                // Reads the value from the db
                int nNewId = BaseDA.GetNextVal("t_incomes");

                // Intializes the return variable with the ID value pulled
                expNewIncome = new IncBL(nNewId);
            }
            // If there was any error, stops it at this level
            catch{}

            // Returns the intialized variable to the calling function
            return (expNewIncome);
        }

        /// <summary>
        /// Loads an income entity from the cache based on the given id
        /// </summary>
        /// <param name="nId">The id of the income to load</param>
        /// <returns>An entity represntation of the income asked for</returns>
        public static IncBL Load(int nId)
        {
            // Creates a return variable with a default value of null
            IncBL incLoadInc = null;

            // Before loading checks that the wanted id is in the table
            if (Cache.SDB.t_incomes.Rows.Contains(nId))
            {
                // Intializes the variable with the ctor, and sets the id property
                incLoadInc = new IncBL(nId);

                // Pulls the row of the wanted income from the cache
                DataRow drExpence = Cache.SDB.t_incomes.Rows.Find(nId);

                // Sets the properties based on the data in the row
                incLoadInc.Amount = drExpence["AMOUNT"].ToString();
                incLoadInc.Date = Convert.ToDateTime(drExpence["INC_DATE"].ToString());
                incLoadInc.Category = Convert.ToInt32(drExpence["CATEGORY"].ToString());
                incLoadInc.Method = Convert.ToInt32(drExpence["METHOD"].ToString());
                incLoadInc.Comment = drExpence["COMMENT"].ToString();
            }

            // Returns the variable to the calling function
            return (incLoadInc);
        } 

        #endregion
    }
}
