using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrameWork
{
    public static class Globals
    {
        public static Dictionary<string, Log> LogFiles = new Dictionary<string, Log>()
            {
                {"ErrorLog", new Log("../../../Log Files/errors.mez")},
                {"ActivityLog", new Log("../../../Log Files/activity.mez")}
            };
    }
}
