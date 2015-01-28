using Data;
using FrameWork;
using System;
using System.Collections.Generic;
using System.Data.Common;

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
                var factory = ConnectionManager.ProviderFactory;

                using (var conn = factory.CreateConnection())
                using (var command = conn.CreateCommand())
                using (var adapter = factory.CreateDataAdapter())
                {
                    command.CommandText = "SELECT * FROM " + strTableName;
                    adapter.SelectCommand = command;

                    // Loads the table using the appropiate data adapter
                    int nRowsFilled = adapter.Fill(Cache.SDB.Tables[strTableName]);

                    Globals.LogFiles["DataBaseLog"].AddMessages(Globals.DbActivity.READ.ToString() +
                                                                " at " + DateTime.Now,
                                                                "Command: Adapter.Fill(" + strTableName + ")",
                                                                "Result: " + nRowsFilled.ToString());
                }
            }
            // In the event of a databse exception
            catch (DbException e)
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

        #region Save Methods

        public static void SaveAllFromCache()
        {
            foreach (string CurrTableName in tableNames)
            {
                SaveFromCache(CurrTableName);
            }
        }

        internal static void SaveFromCache(string strTableName)
        {
            try
            {
                var factory = ConnectionManager.ProviderFactory;

                using (var conn = factory.CreateConnection())
                using (var command = conn.CreateCommand())
                using (var adapter = factory.CreateDataAdapter())
                using (var cbBuilder = factory.CreateCommandBuilder())
                {
                    command.CommandText = "SELECT * FROM " + strTableName;
                    adapter.SelectCommand = command;
                    cbBuilder.DataAdapter = adapter;

                    // Updating table as is
                    int nRowsUpdated = adapter.Update(Cache.SDB.Tables[strTableName]);

                    Globals.LogFiles["DataBaseLog"].AddMessages(Globals.DbActivity.WRITE.ToString() +
                                                                " at " + DateTime.Now,
                                                                "Command: Adapter.Update(" + strTableName + ")",
                                                                "Result: " + nRowsUpdated.ToString());
                }
            }
            // In the event of a databse exception
            catch (DbException e)
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
