using MyHome.Persistence;

namespace MyHome.Spec.Helpers
{
    public class TestAccountingDataContext: AccountingDataContext
    {
        public TestAccountingDataContext():base("name=Database")
        {

        }
    }
}
