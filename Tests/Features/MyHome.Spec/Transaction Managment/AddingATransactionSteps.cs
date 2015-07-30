using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyHome.DataClasses;
using MyHome.Services;
using MyHome.Spec.Helpers;
using MyHome.Spec.Mocks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

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
        private PaymentMethodService _paymentMethodService = ServiceMocks.GetMockPaymentMethodService();
        private ICategoryService _categoryService;
        private Category _category;
        private PaymentMethod _paymentMethod;

        [BeforeScenario]
        public void Setup()
        {
            _transaction = null;
            _transactionService = null;
            _categoryService = null;
            _category = null;
            _paymentMethod = null;
        }

        [Given(@"The transaction type is '(.*)'")]
        public void GivenTheTransactionTypeIs(TransactionTypes transactionType)
        {
            _transactionType = transactionType;
        }

        [Given(@"the following transaction data with a category '(.*)' and payment method '(.*)'")]
        public void GivenTheFollowingTransaction(string category, string paymentMethod, Table data)
        {
            switch (_transactionType)
            {
                case TransactionTypes.Income:
                    _transaction = data.CreateInstance<Income>();
                    _transactionService = ServiceMocks.GetMockIncomeService();
                    _categoryService = ServiceMocks.GetMockIncomeCategoryService();
                    break;
                case TransactionTypes.Expense:
                    _transaction = data.CreateInstance<Expense>();
                    _transactionService = ServiceMocks.GetMockExpenseService();
                    _categoryService = ServiceMocks.GetMockExpenseCategoryService();
                    break;
                default:
                    break;
            }

            _paymentMethod = new PaymentMethod(0, paymentMethod);
            _category = new Category(0, category);
            _transaction.Category = _category;
            _transaction.Method = _paymentMethod;
        }

        [When(@"I press add")]
        public void WhenIPressAdd()
        {
            try
            {
                _categoryService.Add(_transaction.Category.Name);
                _paymentMethodService.Add(_transaction.Method.Name);
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

        [Then(@"the handler returns an error indicator")]
        public void ThenTheHandlerReturnsAnErrorIndicator()
        {
            var e = ScenarioContext.Current.Get<Exception>(EXCEPTION_CONTEXT_KEY);
            Assert.IsNotNull(e);
            Assert.IsInstanceOfType(e, typeof(ArgumentException));
            Assert.AreEqual("The amount can not be empty", e.Message);
        }

    }
}
