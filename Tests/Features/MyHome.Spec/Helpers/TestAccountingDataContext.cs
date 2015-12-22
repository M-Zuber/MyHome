using MyHome.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHome.Spec.Helpers
{
    public class TestAccountingDataContext: AccountingDataContext
    {
        public TestAccountingDataContext():base("name=Database")
        {

        }
    }
}
