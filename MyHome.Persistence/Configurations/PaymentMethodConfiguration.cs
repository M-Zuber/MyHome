using System.Data.Entity.ModelConfiguration;
using MyHome.DataClasses;

namespace MyHome.Persistence.Configurations
{
    public class PaymentMethodConfiguration : EntityTypeConfiguration<PaymentMethod>
    {
        public PaymentMethodConfiguration()
        {
            HasKey(pm => pm.Id);

            Property(pm => pm.Id).HasColumnName("Id");
            Property(pm => pm.Name).HasColumnName("Name");

            ToTable("PaymentMethod", "Accounting");
        }
    }
}
