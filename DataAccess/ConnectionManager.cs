using MySql.Data.MySqlClient;
using Npgsql;
using System.Data.Common;
using System.Data.SQLite;

namespace DataAccess
{
    /// <summary>
    /// Holds the connection needed to access the db
    /// </summary>
    public static class ConnectionManager
    {
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

        public static string BuildConnectionString(DbProviderFactory factory, ConnectionOptions options)
        {
            var builder = factory.CreateConnectionStringBuilder();

            if (builder is MySqlConnectionStringBuilder)
            {
                var b = builder as MySqlConnectionStringBuilder;
                b.Server = options.Server;
                b.Database = options.Database;
                b.UserID = options.Username;
                b.Password = options.Password;
                b.IntegratedSecurity = false;
                return b.ToString();
            }
            else if (builder is SQLiteConnectionStringBuilder)
            {
                var b = builder as SQLiteConnectionStringBuilder;
                b.Version = 3;
                b.DataSource = options.Database;
                return b.ToString();
            }
            else if (builder is NpgsqlConnectionStringBuilder)
            {
                var b = builder as NpgsqlConnectionStringBuilder;
                b.Host = options.Server;
                b.Database = options.Database;
                b.UserName = options.Username;
                b.Password = options.Password;
                b.IntegratedSecurity = false;
                return b.ToString();
            }

            return null;
        }

        public static DbProviderFactory GetDbProvider(string providerName, string connectionString)
        {
            return new DbProviderFactoryProxy(providerName, connectionString);
        }

        public static DbProviderFactory GetDbProvider(string providerName, ConnectionOptions connectionOptions)
        {
            var provider = DbProviderFactoryProxy.GetFactory(providerName);
            string connectionString = BuildConnectionString(provider, connectionOptions);
            return new DbProviderFactoryProxy(provider, connectionString);
        }
    }

    public struct ConnectionOptions
    {
        public string Server;
        public string Database;
        public string Username;
        public string Password;
    }

}
