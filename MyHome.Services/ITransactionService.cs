using System;
using System.Collections.Generic;
using MyHome.DataClasses;

namespace MyHome.Services
{
    public interface ITransactionService
    {
        void Create(Transaction transaction);
        void Save(Transaction transaction);
        IEnumerable<Transaction> GetAll();
    }

    public static class TransactionExtensions
    {
        public static Expense TryParseToExpense(Transaction t)
        {
            var e = t as Expense;
            if (e == null)
            {
                throw new ArgumentException("The transaction is the wrong type");
            }
            e.Id = t.Id;
            e.Amount = t.Amount;
            e.Date = t.Date;
            e.Comments = t.Comments;
            e.Method = t.Method;
            e.PaymentMethodId = t.PaymentMethodId;
            e.CategoryId = t.CategoryId;
            if (t.Category != null)
            {
                e.Category = new ExpenseCategory(t.Category.Id, t.Category.Name);
            }
            else
            {
                e.Category = null;
            }
            return e;
        }

        public static Income TryParseToIncome(Transaction t)
        {
            var i = t as Income;
            if (i == null)
            {
                throw new ArgumentException("The transaction is the wrong type");
            }
            i.Id = t.Id;
            i.Amount = t.Amount;
            i.Date = t.Date;
            i.Comments = t.Comments;
            i.Method = t.Method;
            i.PaymentMethodId = t.PaymentMethodId;
            i.CategoryId = t.CategoryId;
            if (t.Category != null)
            {
                i.Category = new IncomeCategory(t.Category.Id, t.Category.Name);
            }
            else
            {
                i.Category = null;
            }
            return i;
        }
    }
}
