using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Old_FrameWork;

namespace DataAccess
{
    public static class DataStatusAccess
    {
        public static bool DataHasChanges()
        {
            return Cache.SDB.HasChanges();
        }
    }
}
