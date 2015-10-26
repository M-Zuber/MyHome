using Moq;
using MyHome.DataClasses;
using MyHome.DataRepository;
using MyHome.Persistence;
using MyHome.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHome.TestUtils
{
    public static class RepositoryMocks
    {
        public static ExpenseCategoryRepository GetMockExpenseCategoryRepository(List<ExpenseCategory> data = null)
        {
            var mockContext = new Mock<AccountingDataContext>();
            var mockSet = new Mock<DbSet<ExpenseCategory>>().SetupData(data ?? new List<ExpenseCategory>());
            mockSet.Setup(c => c.AsNoTracking()).Returns(mockSet.Object);
            mockContext.Setup(c => c.ExpenseCategories).Returns(mockSet.Object);
            return new ExpenseCategoryRepository(mockContext.Object);
        }

        public static IncomeCategoryRepository GetMockIncomeCategoryRepository()
        {
            var mockContext = new Mock<AccountingDataContext>();
            var mockSet = new Mock<DbSet<IncomeCategory>>().SetupData();
            mockSet.Setup(c => c.AsNoTracking()).Returns(mockSet.Object);
            mockContext.Setup(c => c.IncomeCategories).Returns(mockSet.Object);
            return new IncomeCategoryRepository(mockContext.Object);
        }

        public static PaymentMethodRepository GetMockPaymentMethodRepository()
        {
            var mockContext = new Mock<AccountingDataContext>();
            var mockSet = new Mock<DbSet<PaymentMethod>>().SetupData();
            mockSet.Setup(c => c.AsNoTracking()).Returns(mockSet.Object);
            mockContext.Setup(c => c.PaymentMethods).Returns(mockSet.Object);
            return new PaymentMethodRepository(mockContext.Object);
        }

        public static IncomeRepository GetMockIncomeRepository()
        {
            var mockContext = new Mock<AccountingDataContext>();
            var mockSet = new Mock<DbSet<Income>>().SetupData(new List<Income> { new Income { Category = new IncomeCategory(), Method = new PaymentMethod() } });
            mockSet.Setup(c => c.AsNoTracking()).Returns(mockSet.Object);
            mockContext.Setup(c => c.Incomes).Returns(mockSet.Object);
            return new IncomeRepository(mockContext.Object);
        }

        public static ExpenseRepository GetMockExpenseRepository()
        {
            var mockContext = new Mock<AccountingDataContext>();
            var mockSet = new Mock<DbSet<Expense>>().SetupData(new List<Expense> { new Expense { Category = new ExpenseCategory(), Method = new PaymentMethod() } });
            mockSet.Setup(c => c.AsNoTracking()).Returns(mockSet.Object);
            mockContext.Setup(c => c.Expenses).Returns(mockSet.Object);
            return new ExpenseRepository(mockContext.Object);
        }
    }
}
