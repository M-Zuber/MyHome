using FrameWork;
using MySql.Data.MySqlClient;
using Old_FrameWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess
{
    public static class GlobalDataAccess
    {
        #region Data Members

        private static List<string> tableNames = new List<string>()
        {
            "t_expenses_category",
            "t_incomes_category",
            "t_payment_methods",
            "t_expenses",
            "t_incomes"
        }; 
        
        #endregion

        #region Setup Methods

        private static MySqlDataAdapter GetAdapter(string strTableName)
        {
            // Create select command
            MySqlCommand cmdSelectCommand = new MySqlCommand("SELECT * FROM " + strTableName,
                                                             ConnectionManager.Instance.Connection);

            // Create an adapter to fill the table, and returns it to the calling function
            return (new MySqlDataAdapter(cmdSelectCommand));
        }

        #endregion

        #region Load Methods

        public static void LoadAllToCache()
        {
            // Goes over every table name in the array and loads it
            foreach (string CurrTableName in tableNames)
            {
                LoadToCache(CurrTableName);
            }
        }

        internal static void LoadToCache(string strTableName)
        {
            try
            {

                // Loads the table using the appropiate data adapter
                int nRowsFilled = GetAdapter(strTableName).Fill(Cache.SDB.Tables[strTableName]);

                Globals.LogFiles["DataBaseLog"].AddMessages(Globals.DbActivity.READ.ToString() +
                                                            " at " + DateTime.Now,
                                                            "Command: Adapter.Fill(" + strTableName + ")",
                                                            "Result: " + nRowsFilled.ToString());
            }
            // In the event of a databse exception
            catch (MySqlException e)
            {
                Globals.LogFiles["ErrorLog"].AddError(e.ErrorCode, e.Message, DateTime.Now);
                Globals.LogFiles["ErrorLog"].AddMessage(e.StackTrace);
            }
            // If any other exception occurs
            catch (Exception e)
            {
                Globals.LogFiles["ErrorLog"].AddError(Globals.ErrorCodes.SQL_ERROR, e.Message, DateTime.Now);
                Globals.LogFiles["ErrorLog"].AddMessages(e.StackTrace, e.InnerException.Message);
            }
        }

        #endregion
    }
}
