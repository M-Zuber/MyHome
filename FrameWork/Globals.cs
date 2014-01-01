using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FrameWork
{
    public static class Globals
    {
        public static string DataBaseName = "";
        public static string UserId = "";
        public static string Password = "";
        
        private const string LogPath = "./Log Files/";
        public static Dictionary<string, Log> LogFiles = new Dictionary<string, Log>()
            {
                {"ErrorLog", new Log(LogPath + "errors.mez")},
                {"ProgramActivityLog", new Log(LogPath + "activity.mez")},
                {"DataBaseLog", new Log(LogPath + "dbActivity.mez")},
                {"BusinessLayerLog", new Log(LogPath + "blActivity.mez")}
            };

        private const string SettingsFilesPath = "./Setting Files/";
        public static Dictionary<string, SettingsManager> SettingFiles = new Dictionary<string, SettingsManager>()
            {
                {"DatabaseSettings", new SettingsManager(SettingsFilesPath + "database.set")}
            };

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
    }
}
