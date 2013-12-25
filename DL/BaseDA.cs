using System;
using System.Collections;
using System.Data;
using FrameWork;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace DA
{
    /// <summary>
    /// Provides the basic operations to access and save data that is in the db
    /// </summary>
    public static class BaseDA
    {
        #region Data Members

        // Data members
        private static ArrayList arrTableNames = new ArrayList()
        {
            "t_expenses_category",
            "t_incomes_category",
            "t_payment_methods",
            "t_expenses",
            "t_incomes"
        };

        public static Dictionary<string, int> dcNewIdValues = new Dictionary<string, int>()
        {
            {"t_expenses_category",0},
            {"t_incomes_category",0},
            {"t_payment_methods",0},
            {"t_expenses",0},
            {"t_incomes",0}
        };

        #endregion

        #region Other Methods

        /// <summary>
        /// Creates a data adapter for the table with the name passed in
        /// </summary>
        /// <param name="strTableName">The name of the table to create the adapter for</param>
        /// <returns>The adapter for the table</returns>
        private static MySqlDataAdapter GetAdapter(string strTableName)
        {
            // Create select command
            MySqlCommand cmdSelectCommand = new MySqlCommand("SELECT * FROM " + strTableName,
                                                             ConnectionManager.Instance.Connection);

            // Create an adapter to fill the table, and returns it to the calling function
            return (new MySqlDataAdapter(cmdSelectCommand));
        }
        
        /// <summary>
        /// Intializes the local data member with the values for the new ids of the tables
        /// </summary>
        public static void SetNewIdStart()
        {
            foreach (string CurrIdFunc in BaseDA.arrTableNames)
            {
                BaseDA.dcNewIdValues[CurrIdFunc] = BaseDA.GetInitSeqVal(CurrIdFunc);
            }
        }

        /// <summary>
        /// Gets the next value of the sequence -and upps the value for the next call
        /// to the sequence
        /// </summary>
        /// <param name="strSequenceName">The name of the sequence being called for</param>
        /// <returns>The value from the sequence</returns>
        public static int GetNextVal(string strSequenceName)
        {
            return (BaseDA.dcNewIdValues[strSequenceName]++);
        }

        /// <summary>
        /// Gets the initial sequence value from the sequence with the name passed in
        /// </summary>
        /// <param name="strSequenceName">The name of the sequence to read from</param>
        /// <returns>The value pulled from the sequnce, in case of any issue 
        /// an error is thrown</returns>
        private static int GetInitSeqVal(string strSequenceName)
        {
            // Creates a return variable
            int nReturnValue = 0;

            // Creates the select command to pull the next value from the sequence
            MySqlCommand cmdGetValCommand =
                new MySqlCommand("SELECT max(id) from " + strSequenceName,
                                            ConnectionManager.Instance.Connection);

            // Attempts to get the next value of the sequence
            try
            {
                // Opens the connection and sets the return variable with 
                // the next value of the sequence
                ConnectionManager.Instance.Connection.Open();
                Globals.LogFiles["DataBaseLog"].AddMessage(Globals.DbActivity.CONNECTION.ToString() +
                                                            " at " + DateTime.Now);
                nReturnValue = Convert.ToInt32(cmdGetValCommand.ExecuteScalar());
                Globals.LogFiles["DataBaseLog"].AddMessages(Globals.DbActivity.READ.ToString() +
                                                            " at " + DateTime.Now,
                                                            "Command: " + cmdGetValCommand.CommandText,
                                                            "Result: " + nReturnValue.ToString());
            }
            catch (MySqlException e)
            {
                Globals.LogFiles["ErrorLog"].AddError(e.ErrorCode, e.Message, DateTime.Now);
                Globals.LogFiles["ErrorLog"].AddMessage(e.StackTrace);
            }
            catch (Exception e)
            {
                Globals.LogFiles["ErrorLog"].AddError(Globals.ErrorCodes.SQL_ERROR, e.Message, DateTime.Now);
                Globals.LogFiles["ErrorLog"].AddMessages(e.StackTrace, e.InnerException.Message);
            }
            // Close the connection in any case
            finally
            {
                // Checks what value returned from the db and adjusts accordingly
                if (nReturnValue == 0)
                {
                    nReturnValue = 1;
                }
                else
                {
                    nReturnValue += 1;
                }

                ConnectionManager.Instance.Connection.Close();
                Globals.LogFiles["DataBaseLog"].AddMessage(Globals.DbActivity.DISCONNECT.ToString() +
                                                            " at " + DateTime.Now);
            }

            // Returns the value to the calling function
            return (nReturnValue);
        }

        /// <summary>
        /// Loads the table with the name given from the db into the cache
        /// </summary>
        /// <param name="strTableName">The name of the tbale to load</param>
        public static void LoadToCache(string strTableName)
        {
            try
            {

                // Loads the table using the appropiate data adapter
                int nRowsFilled = GetAdapter(strTableName).Fill(FrameWork.Cache.SDB.Tables[strTableName]);

                Globals.LogFiles["DataBaseLog"].AddMessages(Globals.DbActivity.READ.ToString() +
                                                            " at " + DateTime.Now,
                                                            "Command: Adapter.Fill(" + strTableName + ")",
                                                            "Result: " + nRowsFilled.ToString());
            }
            catch (MySqlException e)
            {
                Globals.LogFiles["ErrorLog"].AddError(e.ErrorCode, e.Message, DateTime.Now);
                Globals.LogFiles["ErrorLog"].AddMessage(e.StackTrace);
            }
            catch (Exception e)
            {
                Globals.LogFiles["ErrorLog"].AddError(Globals.ErrorCodes.SQL_ERROR, e.Message, DateTime.Now);
                Globals.LogFiles["ErrorLog"].AddMessages(e.StackTrace, e.InnerException.Message);
            }
        }

        /// <summary>
        /// Loads all the tables into the cache
        /// </summary>
        public static void LoadToCache()
        {
            // Goes over every table name in the array and loads it
            foreach (string CurrTableName in BaseDA.arrTableNames)
            {
                BaseDA.LoadToCache(CurrTableName);
            }
        }
        /// <summary>
        /// Saves the table with the name given from the cache into the db
        /// </summary>
        /// <param name="strTableName"></param>
        public static void SaveFromCache(string strTableName)
        {
            try
            {

                // Creates the data adapter and sets it with the update commands
                MySqlDataAdapter daAdapter = GetAdapter(strTableName);
                MySqlCommandBuilder cbBuilder = new MySqlCommandBuilder(daAdapter);

                // Updating table as is
                int nRowsUpdated = daAdapter.Update(Cache.SDB.Tables[strTableName]);

                Globals.LogFiles["DataBaseLog"].AddMessages(Globals.DbActivity.WRITE.ToString() +
                                                            " at " + DateTime.Now,
                                                            "Command: Adapter.Update(" + strTableName + ")",
                                                            "Result: " + nRowsUpdated.ToString());
            }
            catch (MySqlException e)
            {
                Globals.LogFiles["ErrorLog"].AddError(e.ErrorCode, e.Message, DateTime.Now);
                Globals.LogFiles["ErrorLog"].AddMessage(e.StackTrace);
            }
            catch (Exception e)
            {
                Globals.LogFiles["ErrorLog"].AddError(Globals.ErrorCodes.SQL_ERROR, e.Message, DateTime.Now);
                Globals.LogFiles["ErrorLog"].AddMessages(e.StackTrace, e.InnerException.Message);
            }
        }

        /// <summary>
        /// Updates the tables in the db as to which rows have been deleted from the cache
        /// </summary>
        /// <param name="strTableName">The name of the table to be updated</param>
        public static void SaveDeletedFromCache(string strTableName)
        {
            try
            {

                // Creates the adapter and sets it with the update command
                MySqlDataAdapter daAdapter = GetAdapter(strTableName);
                MySqlCommandBuilder cbBuilder = new MySqlCommandBuilder(daAdapter);

                // Getting all the deleted rows and updating them
                DataRow[] drarrDeletedRows =
                    Cache.SDB.Tables[strTableName].Select(string.Empty, string.Empty,
                                                            DataViewRowState.Deleted);
                daAdapter.Update(drarrDeletedRows);
            }
            catch (MySqlException e)
            {
                Globals.LogFiles["ErrorLog"].AddError(e.ErrorCode, e.Message, DateTime.Now);
                Globals.LogFiles["ErrorLog"].AddMessage(e.StackTrace);
            }
            catch (Exception e)
            {
                Globals.LogFiles["ErrorLog"].AddError(Globals.ErrorCodes.SQL_ERROR, e.Message, DateTime.Now);
                Globals.LogFiles["ErrorLog"].AddMessages(e.StackTrace, e.InnerException.Message);
            }
        } 

        #endregion
    }
}
