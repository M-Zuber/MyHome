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
        public static Dictionary<int, BaseCategoryAccess> CategoryTypes =
            new Dictionary<int, BaseCategoryAccess>()
            {
                {1, new ExpenseCategoryAccess()},
                {2, new IncomeCategoryAccess()},
                {3, new PaymentMethodAccess()}
            };
    }
}
