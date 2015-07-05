using System.Data.Entity.ModelConfiguration;
using LocalTypes;

namespace Data.Configurations
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
