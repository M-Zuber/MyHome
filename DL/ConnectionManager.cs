using FrameWork;
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
            this.Connection.ConnectionString = "database=" + Globals.DataBaseName +"; " +
                                                "Data Source=127.0.0.10; " +
                                                "User ID = " + Globals.UserId +"; " +
                                                "Password=" + Globals.Password;
        } 

        #endregion

        #region Other Methods

        /// <summary>
        /// Tries the connection based on the current values for User-Id and password
        /// </summary>
        /// <returns>If the connection succeeded</returns>
        public bool TestConnection()
        {
            try
            {
                // Opens and immedatly closes the connection
                // to make sure the connection parameters are correct
                this.Connection.Open();
                this.Connection.Close();
                
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

        #endregion
    }
}
