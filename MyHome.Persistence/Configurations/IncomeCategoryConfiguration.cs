using System.Data.Entity.ModelConfiguration;
using MyHome.DataClasses;

namespace MyHome.Persistence.Configurations
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
