using DA;

namespace BL
{
    /// <summary>
    /// Contains helper methods for the Business and UI layers that dont fit in GlobalBL
    /// </summary>
    public static class HelperMethods
    {
        /// <summary>
        /// Tests the Db connection with the current parameters in the settings
        /// </summary>
        /// <returns>True if the database can be connected to, otherwise false</returns>
        public static bool TestConnection()
        {
            return ConnectionManager.Instance.TestConnection();
        }
    }
}
