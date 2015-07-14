using System;
using System.Configuration;
using System.Data.Common;

namespace MyHome.Spec.Helpers
{
    public class ConnectionHelper
    {
        public static DbConnection CreateConnection()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["Database"];
            var providerFactory = DbProviderFactories.GetFactory(connectionString.ProviderName);
            var connection = providerFactory.CreateConnection();

            if (connection == null)
            {
                throw new Exception("Cannot create a connection with the connection string 'Database'");
            }

            connection.ConnectionString = connectionString.ConnectionString;
            return connection;
        }
    }
}
