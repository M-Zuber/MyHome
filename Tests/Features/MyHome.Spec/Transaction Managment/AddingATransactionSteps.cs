using System;
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
    [Binding]
    [Scope(Feature = "AddingATransaction")]
    public class AddingATransactionSteps
    {
        const string EXCEPTION_CONTEXT_KEY = "add_transaction_result";
        private TransactionTypes _transactionType;
        private Transaction _transaction;
        private ITransactionService _transactionService;
        private PaymentMethodService _paymentMethodService;
        private ICategoryService _categoryService;
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


            if (_transaction.Date.Equals(default(DateTime)))
            {
                _transaction.Date = DateTime.Today;
            }
        }

        [When(@"I press add")]
        public void WhenIPressAdd()
        {
            try
            {
                _categoryService.Create(_transaction.Category?.Name ?? "not this one");
                _paymentMethodService.Create(_transaction.Method?.Name ?? "not this one");
                _transactionService.Create(_transaction);
            }
            catch (Exception e)
            {
                ScenarioContext.Current.Add(EXCEPTION_CONTEXT_KEY, e);
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
            //TODO check this once issue with Contract.Require is sorted out
            //Assert.AreEqual(errorMessage, e.Message, ignoreCase: true); 
        }

        [Then(@"the date is the current date")]
        public void ThenTheDateIsTheCurrentDate()
        {
            Assert.AreEqual(DateTime.Today, _transaction.Date);
        }


    }
}
