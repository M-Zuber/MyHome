using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DA;

namespace BL
{
    public static class HelperMethods
    {
        public static bool TestConnection()
        {
            return ConnectionManager.Instance.TestConnection();
        }
    }
}
