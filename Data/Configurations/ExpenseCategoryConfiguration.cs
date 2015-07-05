using System.Data.Entity.ModelConfiguration;
using LocalTypes;

namespace Data.Configurations
{
    public class ExpenseCategoryConfiguration : EntityTypeConfiguration<ExpenseCategory>
    {
        public ExpenseCategoryConfiguration()
        {
            HasKey(ec => ec.Id);

            Property(ec => ec.Id).HasColumnName("Id");
            Property(ec => ec.Name).HasColumnName("Name");

            ToTable("ExpenseCategory", "Accounting");
        }
    }
}
