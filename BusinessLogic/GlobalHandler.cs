using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LocalTypes;

namespace BusinessLogic
{
    public static class GlobalHandler
    {
        #region Data Members

        public static Dictionary<int, BaseCategoryHandler> CategoryHandlers =
            new Dictionary<int, BaseCategoryHandler>()
            {
                
            };

        public static Dictionary<int, string> CategoryTypeNames =
            new Dictionary<int, string>()
            {
                
            };
        
        #endregion
    }
}
