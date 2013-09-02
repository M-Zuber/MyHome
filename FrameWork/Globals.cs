using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrameWork
{
    public static class Globals
    {
        private const string LogPath = "../../../Log Files/";
        public static Dictionary<string, Log> LogFiles = new Dictionary<string, Log>()
            {
                {"ErrorLog", new Log(LogPath + "errors.mez")},
                {"ActivityLog", new Log(LogPath + "activity.mez")}
            };

        public enum ErrorCodes
        {
            SQL_ERROR = 0,
            BL_ERROR = 39,
            UI_ERROR = 12
        };
    }
}
