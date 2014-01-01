using System.Collections.Generic;

namespace FrameWork
{
    public static class Globals
    {
        #region Enums

        public enum ErrorCodes
        {
            SQL_ERROR = 0,
            BL_ERROR = 39,
            UI_ERROR = 12
        };

        public enum DbActivity
        {
            CONNECTION,
            DISCONNECT,
            WRITE,
            READ
        };

        #endregion

        #region Settings Variables

        #region Database Connection

        public static string DataBaseName = "";
        public static string UserId = "";
        public static string Password = "";
        
        #endregion

        #endregion

        #region Log/Settings File Access

        #region Log Files

        // Log files directory
        private const string LogPath = "./Log Files/";
        
        // Dictionary of all log files
        public static Dictionary<string, Log> LogFiles = new Dictionary<string, Log>()
            {
                {"ErrorLog", new Log(LogPath + "errors.mez")},
                {"ProgramActivityLog", new Log(LogPath + "activity.mez")},
                {"DataBaseLog", new Log(LogPath + "dbActivity.mez")},
                {"BusinessLayerLog", new Log(LogPath + "blActivity.mez")}
            };

        #endregion

        #region Settings Files

        // Settings files directory
        private const string SettingsFilesPath = "./Setting Files/";

        // Dictionary of all setting files
        public static Dictionary<string, SettingsManager> SettingFiles = new Dictionary<string, SettingsManager>()
            {
                {"DatabaseSettings", new SettingsManager(SettingsFilesPath + "database.set")}
            };
        
        #endregion

        #endregion
    }
}
