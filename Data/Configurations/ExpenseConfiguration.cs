using System.Data.Entity.ModelConfiguration;
using LocalTypes;

namespace Data.Configurations
{
    public class ExpenseConfiguration : EntityTypeConfiguration<Expense>
    {
        public ExpenseConfiguration()
        {
            HasKey(e => e.Id);

            Property(e => e.Id).HasColumnName("Id");
            Property(e => e.Amount).HasColumnName("Amount");
            Property(e => e.Date).HasColumnName("Date").HasColumnType("Date");
            Property(e => e.CategoryId).HasColumnName("CategoryId");
            Property(e => e.PaymentMethodId).HasColumnName("PaymentMethodId");

            HasRequired(e => e.Method).WithMany().HasForeignKey(e => e.PaymentMethodId);
            HasRequired(e => e.Category).WithMany().HasForeignKey(e => e.CategoryId);

            ToTable("Expense", "Accounting");
        }
    }
}
