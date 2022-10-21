using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyHome.DataClasses;
using MyHome.DataRepository;
using MyHome.Persistence;
using MyHome.Services;
using MyHome.Spec.Helpers;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace MyHome.Spec.TransactionManagement
{
    [Binding]
    [Scope(Feature = "AddingATransaction")]
    public class AddingATransactionSteps
    {
        const string ExceptionContextKey = "add_transaction_result";
        private readonly ScenarioContext _scenarioContext;
        private TransactionTypes _transactionType;
        private Transaction _transaction;
        private ITransactionService _transactionService;
        private PaymentMethodService _paymentMethodService;
        private ICategoryService<Category> _categoryService;
        private Category _category;
        private PaymentMethod _paymentMethod;
        private AccountingDataContext _context;

        public AddingATransactionSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public void Setup()
        {
            _context = new TestAccountingDataContext();
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
            _context.Database.Delete();
        }

        [Given(@"The transaction type is '(.*)'")]
        public void GivenTheTransactionTypeIs(TransactionTypes transactionType)
        {
            _transactionType = transactionType;
        }

        [Given(@"the following transaction data with a category '(.*)' and payment method '(.*)'")]
        public void GivenTheFollowingTransaction(string category, string paymentMethod, Table data)
        {
            _paymentMethodService = new PaymentMethodService(new PaymentMethodRepository(_context));

            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (_transactionType)
            {
                case TransactionTypes.Income:
                    _transaction = data.CreateInstance<Income>();
                    _transactionService = new IncomeService(new IncomeRepository(_context));
                    _categoryService = new IncomeCategoryService(new IncomeCategoryRepository(_context));
                    break;
                case TransactionTypes.Expense:
                    _transaction = data.CreateInstance<Expense>();
                    _transactionService = new ExpenseService(new ExpenseRepository(_context));
                    _categoryService = new ExpenseCategoryService(new ExpenseCategoryRepository(_context));
                    break;
            }

            if (!string.IsNullOrWhiteSpace(paymentMethod))
            {
                _paymentMethod = new PaymentMethod(0, paymentMethod);
                _transaction.Method = _paymentMethod;
            }

            if (!string.IsNullOrWhiteSpace(category))
            {
                _category = new Category(0, category);
                _transaction.Category = _category;
            }


            if (_transaction.Date.Equals(default))
            {
                _transaction.Date = DateTime.Today;
            }
        }

        [When(@"I press add")]
        public void WhenIPressAdd()
        {
            try
            {
                if (_category != null)
                {
                    _category = _categoryService.Create(_category?.Name ?? "not this one");
                    _transaction.CategoryId = _category.Id;
                }
                if (_paymentMethod != null)
                {
                    _paymentMethod = _paymentMethodService.Create(_paymentMethod?.Name ?? "not this one");
                    _transaction.PaymentMethodId = _paymentMethod.Id;
                }


                _transactionService.Create(_transaction);
            }
            catch (Exception e)
            {
                _scenarioContext.Add(ExceptionContextKey, e);
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
            var e = _scenarioContext.Get<Exception>(ExceptionContextKey);
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
