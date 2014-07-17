using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data;

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
