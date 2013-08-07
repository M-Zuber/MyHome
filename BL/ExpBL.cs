using System;
using System.Collections.Generic;
using System.Data;
using DA;
using FrameWork;

namespace BL
{
    /// <summary>
    /// Entity representation of an expense
    /// </summary>
    public class ExpBL
    {
        #region Properties

        /// <summary>
        /// The amount of the expense
        /// </summary>
        public string Amount { get; set; }

        /// <summary>
        /// The date of the expense
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Category of the expense
        /// </summary>
        public int Category { get; set; }

        /// <summary>
        /// How the expense was payed
        /// </summary>
        public int Method { get; set; }

        /// <summary>
        /// Additional info about the expense
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// ID number of the expense in the data table
        /// </summary>
        public int ID { get; private set; }
        
        #endregion

        #region C'tor

        /// <summary>
        /// Private ctor for the class that initializes the ID property
        /// with the given value
        /// </summary>
        /// <param name="nId">The id of the expense being created</param>
        private ExpBL(int nId)
        {
            this.ID = nId;
        }
        
        #endregion

        #region Other Methods

        /// <summary>
        /// Saves the current expense into the table in the cache
        /// -Deals with updating and creating new rows
        /// </summary>
        public void Save()
        {
            // Variable to contain the data of the expense in the format
            // neccessary to add it to the table
            StaticDataSet.t_expensesRow drExpence =
                    (StaticDataSet.t_expensesRow)Cache.SDB.t_expenses.Rows.Find(this.ID);
            
            // Checks if the expense already exists 
            // -If so performs an update
            if (drExpence != null)
            {
                // Updates each field with the data in the members of the class
                drExpence["AMOUNT"] = this.Amount;
                drExpence["exp_DATE"] = this.Date;
                drExpence["CATEGORY"] = this.Category;
                drExpence["METHOD"] = this.Method;
                drExpence["COMMENTS"] = this.Comment;
            }
            // If a new row is being added
            else
            {
                // Intializes the variable with an empty skeleton of the row format
                // and adds in the data based on the members of the class
                drExpence = Cache.SDB.t_expenses.Newt_expensesRow();
                drExpence["ID"] = this.ID;
                drExpence["AMOUNT"] = this.Amount;
                drExpence["exp_DATE"] = this.Date;
                drExpence["CATEGORY"] = this.Category;
                drExpence["METHOD"] = this.Method;
                drExpence["COMMENTS"] = this.Comment;

                // Adds the row to the table
                Cache.SDB.t_expenses.Addt_expensesRow(drExpence);
            }
        }

        /// <summary>
        /// Loads all the expenses from the cache and adds them
        /// to a dictionary in entity form
        /// </summary>
        /// <returns>A list of all the expenses in a sorted dictionary</returns>
        public static SortedDictionary<int, ExpBL> GetAll()
        {
            // Intializes the return variable
            SortedDictionary<int, ExpBL> srtAllExpences =
                new SortedDictionary<int, ExpBL>();

            // Goes over every row in the table in the cache
            foreach (StaticDataSet.t_expensesRow currRow in Cache.SDB.t_expenses)
            {
                // Adds the row to the dictionary, creating the entity as it gets added
                srtAllExpences.Add(int.Parse(currRow["ID"].ToString()),
                                  Load(int.Parse(currRow["ID"].ToString())));
            }

            // Returns the list to the calling function
            return (srtAllExpences);
        }

        /// <summary>
        /// Creates a new entity of an expense -Using the sequence in the db
        /// for the expenses table
        /// </summary>
        /// <returns>A new expense with only the ID property filled</returns>
        public static ExpBL CreateExpence()
        {
            // Creates a return variable with a default value of null
            ExpBL expNewExpence = null;

            // Tries intializing the id field with a value from the db
            try
            {
                // Reads the value from the db
                int nNewId = BaseDA.GetNextVal("t_expenses");
                
                // Intializes the return variable with the ID value pulled
                expNewExpence = new ExpBL(nNewId);
            }
            // If there was any error, stops it at this level
            catch{}

            // Returns the intialized variable to the calling function
            return (expNewExpence);
        }

        /// <summary>
        /// Loads an expense entity from the cache based on the given id
        /// </summary>
        /// <param name="nId">The id of the expense to load</param>
        /// <returns>An entity represntation of the expense asked for</returns>
        public static ExpBL Load(int nId)
        {
            // Creates a return variable with a default value of null
            ExpBL expLoadExpence = null;

            // Before loading checks that the wanted id is in the table
            if (Cache.SDB.t_incomes.Rows.Contains(nId))
            {
                // Intializes the variable with the ctor, and sets the id property
                expLoadExpence = new ExpBL(nId);

                // Pulls the row of the wanted expense from the cache
                DataRow drExpence = Cache.SDB.t_incomes.Rows.Find(nId);

                // Sets the properties based on the data in the row
                expLoadExpence.Amount = drExpence["AMOUNT"].ToString();
                expLoadExpence.Date = Convert.ToDateTime(drExpence["EXP_DATE"].ToString());
                expLoadExpence.Category = Convert.ToInt32(drExpence["CATEGORY"].ToString());
                expLoadExpence.Method = Convert.ToInt32(drExpence["METHOD"].ToString());
                expLoadExpence.Comment = drExpence["COMMENT"].ToString();
            }

            // Returns the variable to the calling function
            return (expLoadExpence);
        } 

        #endregion
    }
}
