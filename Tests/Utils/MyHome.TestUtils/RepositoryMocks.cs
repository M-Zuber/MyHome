using Moq;
using MyHome.DataClasses;
using MyHome.DataRepository;
using MyHome.Persistence;
using System.Collections.Generic;
using System.Data.Entity;

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

        public static IncomeCategoryRepository GetMockIncomeCategoryRepository(List<IncomeCategory> data = null)
        {
            var mockContext = new Mock<AccountingDataContext>();
            var mockSet = new Mock<DbSet<IncomeCategory>>().SetupData(data ?? new List<IncomeCategory>());
            mockSet.Setup(c => c.AsNoTracking()).Returns(mockSet.Object);
            mockContext.Setup(c => c.IncomeCategories).Returns(mockSet.Object);
            return new IncomeCategoryRepository(mockContext.Object);
        }

        public static PaymentMethodRepository GetMockPaymentMethodRepository(List<PaymentMethod> data = null)
        {
            var mockContext = new Mock<AccountingDataContext>();
            var mockSet = new Mock<DbSet<PaymentMethod>>().SetupData(data ?? new List<PaymentMethod>());
            mockSet.Setup(c => c.AsNoTracking()).Returns(mockSet.Object);
            mockContext.Setup(c => c.PaymentMethods).Returns(mockSet.Object);
            return new PaymentMethodRepository(mockContext.Object);
        }

        public static IncomeRepository GetMockIncomeRepository(List<Income> data = null)
        {
            var mockContext = new Mock<AccountingDataContext>();
            var mockSet = new Mock<DbSet<Income>>().SetupData(data ?? new List<Income> { new Income { Category = new IncomeCategory(), Method = new PaymentMethod() } });
            mockSet.Setup(c => c.AsNoTracking()).Returns(mockSet.Object);
            mockContext.Setup(c => c.Incomes).Returns(mockSet.Object);
            return new IncomeRepository(mockContext.Object);
        }

        public static ExpenseRepository GetMockExpenseRepository(List<Expense> data = null)
        {
            var mockContext = new Mock<AccountingDataContext>();
            var mockSet = new Mock<DbSet<Expense>>().SetupData(data ?? new List<Expense> { new Expense { Category = new ExpenseCategory(), Method = new PaymentMethod() } });
            mockSet.Setup(c => c.AsNoTracking()).Returns(mockSet.Object);
            mockContext.Setup(c => c.Expenses).Returns(mockSet.Object);
            return new ExpenseRepository(mockContext.Object);
        }

        public static GeneralCategoryHandler GetMockGeneralCategoryHandler(List<ExpenseCategory> expenseCategories = null, List<IncomeCategory> incomeCategories = null)
        {
            var mockContext = new Mock<AccountingDataContext>();
            var mockIncomeCategorySet = new Mock<DbSet<IncomeCategory>>().SetupData(incomeCategories ?? new List<IncomeCategory>());
            var mockExpenseCategorySet = new Mock<DbSet<ExpenseCategory>>().SetupData(expenseCategories ?? new List<ExpenseCategory>());
            mockIncomeCategorySet.Setup(c => c.AsNoTracking()).Returns(mockIncomeCategorySet.Object);
            mockExpenseCategorySet.Setup(c => c.AsNoTracking()).Returns(mockExpenseCategorySet.Object);
            mockContext.Setup(c => c.IncomeCategories).Returns(mockIncomeCategorySet.Object);
            mockContext.Setup(c => c.ExpenseCategories).Returns(mockExpenseCategorySet.Object);
            return new GeneralCategoryHandler(mockContext.Object);
        }
    }
}
