﻿using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyHome.DataClasses;
using MyHome.Services;
using MyHome.Spec.Helpers;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using MyHome.Persistence;
using MyHome.DataRepository;

namespace MyHome.Spec.Transaction_Managment
{
    public enum Properties
    {
        Date,
        Amount,
        Comment,
        Category,
        Method
    }

    [Binding]
    [Scope(Feature = "EditingATransaction")]
    public class EditingATransactionSteps
    {
        const string EXCEPTION_CONTEXT_KEY = "edit_transaction_result";
        private TransactionTypes _transactionType;
        private Transaction _transaction;
        private ITransactionService _transactionService;
        private PaymentMethodService _paymentMethodService;
        private ICategoryService<Category> _categoryService;
        private Category _category;
        private PaymentMethod _paymentMethod;
        private AccountingDataContext context;

        [BeforeScenario]
        public void Setup()
        {
            context = new TestAccountingDataContext();
            _transaction = null;
            _transactionService = null;
            _categoryService = null;
            _paymentMethodService = null;
            _category = null;
            _paymentMethod = null;
        }

        [AfterScenario]
        public void TearDown()
        {
            context.Database.Delete();
        }

        [Given(@"The transaction type is '(.*)'")]
        public void GivenTheTransactionTypeIs(TransactionTypes transactionType)
        {
            _transactionType = transactionType;
        }

        [Given(@"the following transaction data with a category '(.*)' and payment method '(.*)'")]
        public void GivenTheFollowingTransaction(string category, string paymentMethod, Table data)
        {
            _paymentMethodService = new PaymentMethodService(new PaymentMethodRepository(context));
            switch (_transactionType)
            {
                case TransactionTypes.Income:
                    _transaction = data.CreateInstance<Income>();
                    _transactionService = new IncomeService(new IncomeRepository(context));
                    _categoryService = new IncomeCategoryService(new IncomeCategoryRepository(context));
                    break;
                case TransactionTypes.Expense:
                    _transaction = data.CreateInstance<Expense>();
                    _transactionService = new ExpenseService(new ExpenseRepository(context));
                    _categoryService = new ExpenseCategoryService(new ExpenseCategoryRepository(context));
                    break;
            }

            _paymentMethod = _paymentMethodService.Create(paymentMethod);
            _transaction.Method = _paymentMethod;
            _transaction.PaymentMethodId = _paymentMethod.Id;

            _category = _categoryService.Create(category);
            _transaction.Category = _category;
            _transaction.CategoryId = _category.Id;
            _transaction.Date = DateTime.Today;

            _transaction.Id = 1;
            _transactionService.Create(_transaction);
        }

        [When(@"I change the '(.*)' to '(.*)'")]
        public void WhenIChangeTheTo(Properties propChanging, string value)
        {
            switch (propChanging)
            {
                case Properties.Date:
                    _transaction.Date = DateTime.Parse(value != "" ? value : DateTime.Now.ToString());
                    break;
                case Properties.Amount:
                    _transaction.Amount = decimal.Parse(value);
                    break;
                case Properties.Comment:
                    _transaction.Comments = value;
                    break;
                case Properties.Category:
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        _transaction.Category = _categoryService.Create(value);
                        _transaction.CategoryId = _transaction.Category.Id;
                    }
                    else
                    {
                        _transaction.CategoryId = 0;
                    }
                    break;
                case Properties.Method:
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        _transaction.Method = _paymentMethodService.Create(value);
                        _transaction.PaymentMethodId = _transaction.Method.Id;
                    }
                    else
                    {
                        _transaction.PaymentMethodId = 0;
                    }
                    break;
            }
            try
            {
                _transactionService.Save(_transaction);
            }
            catch (Exception e)
            {
                ScenarioContext.Current.Add(EXCEPTION_CONTEXT_KEY, e);
            }
        }

        [Then(@"the new '(.*)' equals '(.*)'")]
        public void ThenTheNewEquals(Properties propChanged, string value)
        {
            var transactionFromDB = _transactionService.GetAll().First(t => t.Id == 1);
            switch (propChanged)
            {
                case Properties.Date:
                    Assert.AreEqual(transactionFromDB.Date.ToString("yyyy-MM-dd"), value != "" ? value : DateTime.Now.ToString("yyyy-MM-dd"));
                    break;
                case Properties.Amount:
                    Assert.AreEqual(transactionFromDB.Amount.ToString(), value);
                    break;
                case Properties.Comment:
                    Assert.AreEqual(transactionFromDB.Comments, value);
                    break;
                case Properties.Category:
                    Assert.AreEqual(transactionFromDB.Category.Name, value);
                    break;
                case Properties.Method:
                    Assert.AreEqual(transactionFromDB.Method.Name, value);
                    break;
            }
        }

        [Then(@"the transaction should be added to the list")]
        public void ThenTheTransactionShouldBeAddedToTheList()
        {
            var actual = _transactionService.GetAll().FirstOrDefault(t => t.Amount == _transaction.Amount && t.Date == _transaction.Date);

            Assert.IsNotNull(actual);
        }

        [Then(@"the handler returns an error indicator - '(.*)'")]
        public void ThenTheHandlerReturnsAnErrorIndicator(string errorMessage)
        {
            var e = ScenarioContext.Current.Get<Exception>(EXCEPTION_CONTEXT_KEY);
            Assert.IsNotNull(e);
            Assert.IsInstanceOfType(e, typeof(ArgumentException));
            Assert.AreEqual(errorMessage, e.Message, ignoreCase: true); 
        }

        [Then(@"the date is the current date")]
        public void ThenTheDateIsTheCurrentDate()
        {
            Assert.AreEqual(DateTime.Today, _transaction.Date);
        }


    }
}
