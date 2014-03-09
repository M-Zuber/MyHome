using System;
using System.Collections.Generic;
using System.Data;
using DA;
using Old_FrameWork;

namespace BL
{
    /// <summary>
    /// Entity representation of a payment method
    /// </summary>
    public class MethodBL : BaseBL
    {
        #region Properties

        /// <summary>
        /// The name (type) of the payment method category
        /// </summary>
        public override string Name { get; set; }

        /// <summary>
        /// Id number of the payment method category in the table
        /// </summary>
        public override int ID { get; internal set; }

        #endregion

        #region C'tor

        /// <summary>
        /// Private ctor for the class that initializes the ID property
        /// with the given value
        /// </summary>
        /// <param name="nID">The id of the payment method being created</param>
        private MethodBL(int nID)
        {
            this.ID = nID;
        }
        
        #endregion

        #region Other Methods

        /// <summary>
        /// Saves the current payment method into the table in the cache
        /// -Deals with updating and creating new rows
        /// </summary>
        public override void Save()
        {
            // Creates a variable to contain the data in the format neccessary to
            // add it to the table
            DataRow drMethods =
                Cache.SDB.t_payment_methods.Rows.Find(this.ID);

            // Checks if the category is already in the table
            // If the category already exists
            if (drMethods != null)
            {
                Globals.LogFiles["BusinessLayerLog"].AddMessages(
                                           "Updating existing payment method with id of: " + this.ID,
                                           DateTime.Today.ToString());

                // Updates the name of the category
                drMethods["NAME"] = this.Name;
            }
            // If the category does not exist yet
            else
            {
                Globals.LogFiles["BusinessLayerLog"].AddMessages(
                                        "Creating new payment method with id of: " + this.ID,
                                        DateTime.Today.ToString());

                // Adds a new row to the table in the cache with the wanted Id and name
                Cache.SDB.t_payment_methods.
                    Addt_payment_methodsRow(this.ID,this.Name);
            }
        }

        /// <summary>
        /// Loads all the payment methods from the cache and adds them
        /// to a dictionary in entity form
        /// </summary>
        /// <returns>A list of all the payment methods in a sorted dictionary</returns>
        public static SortedDictionary<int, MethodBL> GetAll()
        {
            // Intializes the return variable
            SortedDictionary<int, MethodBL> srtAllMethods =
                new SortedDictionary<int, MethodBL>();

            // Saves the number of rows actually in the database
            int rowsInCache = Cache.SDB.t_payment_methods.Rows.Count;

            // Counter for the number of rows actually read from the databse
            int rowsPulled = 0;

            // Goes over every row in the table in the cache
            foreach (StaticDataSet.t_payment_methodsRow currRow in Cache.SDB.t_payment_methods)
            {
                // Adds the row to the dictionary, creating the entity as it gets added
                srtAllMethods.Add(int.Parse(currRow["ID"].ToString()),
                                  Load(int.Parse(currRow["ID"].ToString())));
                rowsPulled++;
            }

            // If there is a difference between the number of rows read and the number
            // actually in the databse, logs an error
            if (rowsInCache != rowsPulled)
            {
                Globals.LogFiles["ErrorLog"].AddError(Globals.ErrorCodes.BL_ERROR,
                    "The amount in the cache is:" + rowsInCache +
                                        " but only " + rowsPulled + " payment methods where pulled",
                    DateTime.Today);
            }

            // Returns the list to the calling function
            return (srtAllMethods);
        }

        /// <summary>
        /// Creates a new entity of a payment method
        /// -Using the sequence in the db for the payment methods table
        /// </summary>
        /// <returns>A new payment method with only the ID property filled</returns>
        public static  MethodBL CreatePaymentMethodCat()
        {
            // Creates a return variable with a default value of null
            MethodBL mthNewMethod = null;

            // Tries intializing the id field with a value from the db
            try
            {
                // Reads the value from the db
                int nNewId = BaseDA.GetNextVal("t_payment_methods");

                // Intializes the return variable with the ID value pulled
                mthNewMethod = new MethodBL(nNewId);
            }
            // If there was any error, stops it at this level
            catch (Exception e)
            {
                Globals.LogFiles["ErrorLog"].AddError(Globals.ErrorCodes.SQL_ERROR,
                                                            e.Message, DateTime.Now);
                Globals.LogFiles["ErrorLog"].AddMessage(e.StackTrace);
            }

            // Returns the intialized variable to the calling function
            return (mthNewMethod);
        }

        /// <summary>
        /// Loads a payment method entity from the cache based on the given id
        /// </summary>
        /// <param name="nId">The id of the payment method to load</param>
        /// <returns>An entity represntation of the payment method asked for</returns>
        public static MethodBL Load(int nId)
        {
            // Creates a return variable with a default value of null
            MethodBL mthLoadMethod = null;

            // Before loading checks that the wanted id is in the table
            if (Cache.SDB.t_payment_methods.Rows.Contains(nId))
            {
                // Intializes the variable with the ctor, and sets the id property
                mthLoadMethod = new MethodBL(nId);

                // Pulls the row of the wanted payment method category from the cache
                DataRow drMethod =
                    Cache.SDB.t_payment_methods.Rows.Find(nId);

                // Sets the name property based on the data in the row
                mthLoadMethod.Name = drMethod["NAME"].ToString();
            }
            else
            {
                Globals.LogFiles["ErrorLog"].AddError(Globals.ErrorCodes.BL_ERROR,
                   "Attempt to pull non exsistent expense with an id of:" + nId,
                   DateTime.Today);
            }

            // Returns the variable to the calling function
            return (mthLoadMethod);
        }

        /// <summary>
        /// Gets a list of all the payment method category names
        /// </summary>
        /// <returns>The list of payment method category names</returns>
        public static List<string> GetAllNames()
        {
            // Creates  a return variable
            List<string> lsNames = new List<string>();

            // Goes over the dictionary of all the payment method categories
            foreach (KeyValuePair<int, MethodBL> CurrMethodCat in MethodBL.GetAll())
            {
                // Adds the name of the current categoy to the list
                lsNames.Add(CurrMethodCat.Value.Name);
            }

            // Returns the list to the calling function
            return lsNames;
        }

        #endregion
    }
}
