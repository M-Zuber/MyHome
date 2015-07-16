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
    public class AddingATransactionSteps
    {
        private TransactionTypes _transactionType;
        private Transaction _transaction;
        private ITransactionService _transactionService;
        private PaymentMethodService _paymentMethodService = ServiceMocks.GetMockPaymentMethodService();
        private ICategoryService _categoryService;

        [BeforeScenario]
        public void Setup()
        {
            _transaction = null;
            _transactionService = null;
            _categoryService = null;
        }

        [Given(@"The transaction type is '(.*)'")]
        public void GivenTheTransactionTypeIs(TransactionTypes transactionType)
        {
            _transactionType = transactionType;
        }
        
        [Given(@"the following transaction data")]
        public void GivenTheFollowingTransaction(Table data)
        {
            //TODO CreateInstance does not create the category/payment method within transaction
            // add them to mock service from here - maybe change the step to have three paramaters?
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
        }
        
        [When(@"I press add")]
        public void WhenIPressAdd()
        {
            _categoryService.Add(_transaction.Category.Name);
            _paymentMethodService.Add(_transaction.Method.Name);
            _transactionService.Create(_transaction);
        }
        
        [Then(@"the transaction should be added to the list")]
        public void ThenTheTransactionShouldBeAddedToTheList()
        {
            //var actual = _transactionService.GetAll().FirstOrDefault(t => t.Amount == _transaction.Amount && t.Date == _transaction.Date);

            //Assert.IsNotNull(actual);
        }
    }
}
