using System.Data.Common;

namespace DataAccess
{
    public class DbProviderFactoryProxy : DbProviderFactory
    {
        readonly DbProviderFactory factory;
        readonly string connectionString;

        public DbProviderFactoryProxy(string name, string connectionString)
            : this(GetFactory(name), connectionString)
        {
        }

        public DbProviderFactoryProxy(DbProviderFactory factory, string connectionString)
        {
            this.connectionString = connectionString;
            this.factory = factory;
        }

        public override bool CanCreateDataSourceEnumerator
        {
            get { return factory.CanCreateDataSourceEnumerator; }
        }

        public override DbCommand CreateCommand()
        {
            return factory.CreateCommand();
        }

        public override DbCommandBuilder CreateCommandBuilder()
        {
            return factory.CreateCommandBuilder();
        }

        public override DbConnection CreateConnection()
        {
            var conn = factory.CreateConnection();
            conn.ConnectionString = this.connectionString;
            return conn;
        }

        public override DbConnectionStringBuilder CreateConnectionStringBuilder()
        {
            return factory.CreateConnectionStringBuilder();
        }

        public override DbDataAdapter CreateDataAdapter()
        {
            return factory.CreateDataAdapter();
        }

        public override DbDataSourceEnumerator CreateDataSourceEnumerator()
        {
            return factory.CreateDataSourceEnumerator();
        }

        public override DbParameter CreateParameter()
        {
            return factory.CreateParameter();
        }

        public override System.Security.CodeAccessPermission CreatePermission(System.Security.Permissions.PermissionState state)
        {
            return factory.CreatePermission(state);
        }

        public override bool Equals(object obj)
        {
            return factory.Equals(obj);
        }

        public override int GetHashCode()
        {
            return factory.GetHashCode();
        }

        public override string ToString()
        {
            return factory.ToString();
        }

        public static DbProviderFactory GetFactory(string providerName)
        {
            return DbProviderFactories.GetFactory(providerName);
        }
    }
}
