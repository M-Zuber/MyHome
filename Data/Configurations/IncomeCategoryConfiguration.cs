using System.Data.Entity.ModelConfiguration;
using LocalTypes;

namespace Data.Configurations
{
    class IncomeCategoryConfiguration : EntityTypeConfiguration<IncomeCategory>
    {
        public IncomeCategoryConfiguration()
        {
            HasKey(ec => ec.Id);

            Property(ec => ec.Id).HasColumnName("Id");
            Property(ec => ec.Name).HasColumnName("Name");

            ToTable("IncomeCategory", "Accounting");
        }
    }
}
