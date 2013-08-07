using MySql.Data.MySqlClient;

namespace DA
{
    /// <summary>
    /// Holds the connection needed to access the db
    /// </summary>
    public class ConnectionManager
    {
        #region Properties

        /// <summary>
        /// Allows access to the current connection, private set
        /// </summary>
        public MySqlConnection Connection
        {
            get; private set;
        }

        /// <summary>
        /// Private property for singelton implentation
        /// </summary>
        private static ConnectionManager ConnectionInstance
        {
            get; set;
        }

        /// <summary>
        /// Singelton access to the class
        /// </summary>
        public static ConnectionManager Instance
        {
            get
            {
                // Checking if the manager was already created
                // If not creates it
                if (ConnectionInstance == null)
                {
                    ConnectionInstance = new ConnectionManager();
                }

                // Returns the data member to allow access to the class
                return (ConnectionInstance);
            }
        }

        #endregion

        #region C'tor

        /// <summary>
        /// Private C'tor to ensure singleton implentation
        /// </summary>
        private ConnectionManager()
        {
            // Intializes the connection data member and sets it with the connection string
            this.Connection = new MySqlConnection();
            this.Connection.ConnectionString = "database=myhome2013; " +
                                                "Data Source=127.0.0.10; " +
                                                "User ID = root; " +
                                                "Password=7BAC61zuber";
        } 

        #endregion
    }
}
