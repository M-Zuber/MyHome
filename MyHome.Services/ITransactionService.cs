﻿using System;
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
            if (!(t is Expense e))
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
            e.Category = t.Category != null ? new ExpenseCategory(t.Category.Id, t.Category.Name) : null;
            return e;
        }

        public static Income TryParseToIncome(Transaction t)
        {
            if (!(t is Income i))
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
            i.Category = t.Category != null ? new IncomeCategory(t.Category.Id, t.Category.Name) : null;
            return i;
        }
    }
}
