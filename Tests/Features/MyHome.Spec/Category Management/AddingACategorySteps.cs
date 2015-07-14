using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyHome.DataClasses;
using MyHome.DataRepository;
using MyHome.Persistence;
using MyHome.Services;
using TechTalk.SpecFlow;

namespace MyHome.Spec
{
    [Binding]
    [Scope(Feature = "AddingACategory")]
    public class AddingACategorySteps
    {
        ICategoryService _categoryService;
        string _categoryName;
        Mock<AccountingDataContext> mockContext;

        [BeforeScenario]
        public void Setup()
        {
            mockContext = new Mock<AccountingDataContext>();
            
            _categoryService = null;
            _categoryName = "";
        }

        [AfterScenario]
        public void TearDown()
        {
            mockContext = null;
        }

        #region Given

        [Given(@"The category type is '(.*)'")]
        public void GivenTheCategoryTypeIs(string categoryType)
        {
            switch (categoryType)
            {
                case "expense":
                    var mockExpenseCategorySet = new Mock<DbSet<ExpenseCategory>>().SetupData();
                    mockExpenseCategorySet.Setup(c => c.AsNoTracking()).Returns(mockExpenseCategorySet.Object);
                    mockContext.Setup(c => c.ExpenseCategories).Returns(mockExpenseCategorySet.Object);
                    _categoryService = new ExpenseCategoryService(new ExpenseCategoryRepository(mockContext.Object));
                    break;
                case "income":
                    var mockIncomeCategorySet = new Mock<DbSet<IncomeCategory>>().SetupData();
                    mockIncomeCategorySet.Setup(c => c.AsNoTracking()).Returns(mockIncomeCategorySet.Object);
                    mockContext.Setup(c => c.IncomeCategories).Returns(mockIncomeCategorySet.Object);
                    _categoryService = new IncomeCategoryService(new IncomeCategoryRepository(mockContext.Object));
                    break;
                case "paymentmethod":
                    var mockPaymentMethodSet = new Mock<DbSet<PaymentMethod>>().SetupData();
                    mockPaymentMethodSet.Setup(c => c.AsNoTracking()).Returns(mockPaymentMethodSet.Object);
                    mockContext.Setup(c => c.PaymentMethods).Returns(mockPaymentMethodSet.Object);
                    _categoryService = new PaymentMethodService(new PaymentMethodRepository(mockContext.Object));
                    break;
            }
        }

        [Given(@"I have entered '(.*)' as the name")]
        public void GivenIHaveEnteredAsTheName(string name)
        {
            _categoryName = name;
        }

        [Given(@"there is no other category with that name")]
        public void GivenThereIsNoOtherCategoryWithThatName()
        {
            if (_categoryService.Exists(_categoryName))
            {
                _categoryService.Remove(_categoryName);
            }
        }

        [Given(@"there is another category with the same name")]
        public void GivenThereIsAnotherCategoryWithTheSameName()
        {
            try
            {
                _categoryService.Add(_categoryName);
            }
            catch (Exception e)
            {
                ScenarioContext.Current.Add("add_category_result", e);
            }
        }

        [Given(@"I have entered nothing for the name")]
        public void GivenIHaveEnteredNothingForTheName()
        {
            try
            {
                _categoryService.Add(_categoryName);
            }
            catch (ArgumentException e)
            {
                ScenarioContext.Current.Add("add_category_result", e);
            }
        }

        #endregion

        #region When

        [When(@"I press add")]
        public void WhenIPressAdd()
        {
            try
            {
                _categoryService.Add(_categoryName);
            }
            catch (Exception e)
            {
                ScenarioContext.Current.Add("add_category_result", e);
            }
        }

        #endregion

        #region Then

        [Then(@"the handler returns an error indicator")]
        public void TheHandlerReturnsAnErrorIndicator()
        {
            var exception = ScenarioContext.Current.Get<Exception>("add_category_result");
            Assert.IsNotNull(exception);
        }

        [Then(@"the category should be added to the list")]
        public void ThenTheCategoryShouldBeAddedToTheList()
        {
            var result = _categoryService.GetAll().FirstOrDefault(c => string.Equals(c.Name, _categoryName));
            Assert.IsNotNull(result);
        }

        #endregion
    }
}
