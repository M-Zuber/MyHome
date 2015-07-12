﻿using System.Data.Entity.ModelConfiguration;
using MyHome.DataClasses;

namespace MyHome.Persistence.Configurations
{
    public class IncomeConfiguration : EntityTypeConfiguration<Income>
    {
        public IncomeConfiguration()
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
