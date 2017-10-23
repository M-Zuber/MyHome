using System.Collections.Generic;

namespace MyHome.Infrastructure
{
    public static class Globals
    {
        public enum ErrorCodes
        {
            SqlError = 0,
            BlError = 39,
            UiError = 12
        };

        public enum DbActivity
        {
            Connection,
            Disconnect,
            Write,
            Read
        };

        public static string DataBaseName = "";
        public static string UserId = "";
        public static string Password = "";

        // Log files directory
        private const string LogPath = "./Log Files/";

        // Dictionary of all log files
        public static Dictionary<string, Log> LogFiles = new Dictionary<string, Log>
        {
                {"ErrorLog", new Log(LogPath + "errors.mez")},
                {"ProgramActivityLog", new Log(LogPath + "activity.mez")},
                {"DataBaseLog", new Log(LogPath + "dbActivity.mez")},
                {"BusinessLayerLog", new Log(LogPath + "blActivity.mez")}
            };

        // Settings files directory
        private const string SettingsFilesPath = "./Setting Files/";

        // Dictionary of all setting files
        public static Dictionary<string, SettingsManager> SettingFiles = new Dictionary<string, SettingsManager>
        {
                {"DatabaseSettings", new SettingsManager(SettingsFilesPath + "database.set")}
            };
    }
}
