using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LocalTypes;
using DataAccess;

namespace BusinessLogic
{
    public class GlobalHandler
    {
        public static Dictionary<int, BaseCategoryHandler> CategoryTypes =
            new Dictionary<int, BaseCategoryHandler>()
            {
                {1, new ExpenseCategoryHandler()},
                {2, new IncomeCategoryHandler()},
                {3, new PaymentMethodHandler()}
            };
    }
}
