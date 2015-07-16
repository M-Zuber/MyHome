using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using MyHome.DataClasses;
using MyHome.DataRepository;
using MyHome.Persistence;
using MyHome.Services;

namespace MyHome.Spec.Mocks
{
    public static class ServiceMocks
    {
        public static ExpenseCategoryService GetMockExpenseCategoryService()
        {
            var mockContext = new Mock<AccountingDataContext>();
            var mockSet = new Mock<DbSet<ExpenseCategory>>().SetupData();
            mockSet.Setup(c => c.AsNoTracking()).Returns(mockSet.Object);
            mockContext.Setup(c => c.ExpenseCategories).Returns(mockSet.Object);
            return new ExpenseCategoryService(new ExpenseCategoryRepository(mockContext.Object));
        }

        public static IncomeCategoryService GetMockIncomeCategoryService()
        {
            var mockContext = new Mock<AccountingDataContext>();
            var mockSet = new Mock<DbSet<IncomeCategory>>().SetupData();
            mockSet.Setup(c => c.AsNoTracking()).Returns(mockSet.Object);
            mockContext.Setup(c => c.IncomeCategories).Returns(mockSet.Object);
            return new IncomeCategoryService(new IncomeCategoryRepository(mockContext.Object));
        }

        public static PaymentMethodService GetMockPaymentMethodService()
        {
            var mockContext = new Mock<AccountingDataContext>();
            var mockSet = new Mock<DbSet<PaymentMethod>>().SetupData();
            mockSet.Setup(c => c.AsNoTracking()).Returns(mockSet.Object);
            mockContext.Setup(c => c.PaymentMethods).Returns(mockSet.Object);
            return new PaymentMethodService(new PaymentMethodRepository(mockContext.Object));
        }

        public static IncomeService GetMockIncomeService()
        {
            var mockContext = new Mock<AccountingDataContext>();
            var mockSet = new Mock<DbSet<Income>>();
            mockSet.Setup(c => c.AsNoTracking()).Returns(mockSet.Object);
            mockContext.Setup(c => c.Incomes).Returns(mockSet.Object);
            return new IncomeService(new IncomeRepository(mockContext.Object));
        }

        public static ITransactionService GetMockExpenseService()
        {
            var mockContext = new Mock<AccountingDataContext>();
            var mockSet = new Mock<DbSet<Expense>>();
            mockSet.Setup(c => c.AsNoTracking()).Returns(mockSet.Object);
            mockContext.Setup(c => c.Expenses).Returns(mockSet.Object);
            return new ExpenseService(new ExpenseRepository(mockContext.Object));
        }
    }
}
