using System.Data.Common;

namespace DataAccess
{
    /// <summary>
    /// Holds the connection needed to access the db
    /// </summary>
    public static class ConnectionManager
    {
        public static DbProviderFactory ProviderFactory { get; set; }

        /// <summary>
        /// Tries the connection based on the current values for User-Id and password
        /// </summary>
        /// <returns>If the connection succeeded</returns>
        public static bool TestConnection(DbProviderFactory factory)
        {
            
            using (var conn = factory.CreateConnection())
            {
                try
                {
                    // Opens and immedatly closes the connection
                    // to make sure the connection parameters are correct
                    conn.Open();

                    // The parameters are correct and the conenction succeeded
                    return true;
                }
                catch
                {
                    // There is an issue with one or more parameters, and the connection
                    // did not succeeded
                    return false;
                }
            }
        }
    }
}
