using System.Data.Entity;
using MyHome.DataClasses;
using MyHome.Persistence.Configurations;

namespace MyHome.Persistence
{
    public class AccountingDataContext : DbContext
    {
        // TODO : move to configuration file 
        public const string ConnectionString = @"Server=.;Database=MyHome2013;User Id=home_user;Password=homeuser;";

        public AccountingDataContext() : base(ConnectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new IncomeConfiguration());
            modelBuilder.Configurations.Add(new IncomeCategoryConfiguration());
            modelBuilder.Configurations.Add(new ExpenseConfiguration());
            modelBuilder.Configurations.Add(new ExpenseCategoryConfiguration());
            modelBuilder.Configurations.Add(new PaymentMethodConfiguration());
        }

        public IDbSet<Income> Incomes { get; set; }

        public IDbSet<Expense> Expenses { get; set; }

        public IDbSet<IncomeCategory> IncomeCategories { get; set; }

        public IDbSet<ExpenseCategory> ExpenseCategories { get; set; }

        public IDbSet<PaymentMethod> PaymentMethods { get; set; }
    }
}
