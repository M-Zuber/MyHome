using System;
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

namespace MyHome.Spec.Category_Management
{
    [Binding]
    [Scope(Feature = "UpdatingCategory")]
    public class UpdatingCategorySteps
    {
        const string AddCategoryResultKey = "add_category_result";

        private string _categoryName;
        private string _newName;
        private int _categoryId;

        Mock<AccountingDataContext> mockContext;
        ICategoryService _categoryService;

        [BeforeScenario]
        public void Setup()
        {
            mockContext = new Mock<AccountingDataContext>();
            _categoryName = "";
            _newName = "";
            _categoryId = -1;
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
        [Given(@"the current name is '(.*)'")]
        public void GivenTheCurrentNameIs(string currentName)
        {
            _categoryName = currentName;
        }

        [Given(@"there is another category with the same name")]
        public void GivenThereIsAnotherCategoryWithTheSameName()
        {
            _categoryService.Add(_categoryName);
        }

        [Given(@"there is no other category with that name")]
        public void GivenThereIsNoOtherCategoryWithThatName()
        {
            if (_categoryService.Exists(_categoryName))
            {
                _categoryService.Remove(_categoryName);
            }
        }

        [Given(@"I save the category")]
        public void SaveCategory()
        {
            _categoryService.Add(_categoryName);
            var category = _categoryService.GetAll().First(x => x.Name == _categoryName);
            _categoryId = category.Id;
        }

        [Given(@"the '(.*)' already exists")]
        public void GivenTheAlreadyExists(string newName)
        {
            _categoryService.Add(newName);
        }

        #endregion

        #region When

        [When(@"I have entered nothing for the name")]
        public void GivenIHaveEnteredNothingForTheName()
        {
            var category = _categoryService.GetAll().First(c => string.Equals(c.Name, _categoryName, StringComparison.CurrentCultureIgnoreCase));

            try
            {
                _categoryService.Update(category.Id, "");
            }
            catch (Exception e)
            {
                ScenarioContext.Current.Add(AddCategoryResultKey, e);
            }
        }

        [When(@"I change the name to '(.*)'")]
        public void WhenIChangeTheNameTo(string newName)
        {
            var category = _categoryService.GetAll().First(c => string.Equals(c.Name, _categoryName, StringComparison.CurrentCultureIgnoreCase));
            _newName = newName;

            try
            {
                _categoryService.Update(category.Id, _newName);
            }
            catch (Exception e)
            {
                ScenarioContext.Current.Add(AddCategoryResultKey, e);    
            }
        }

        #endregion

        #region Then

        [Then(@"the category is updated")]
        public void ThenTheCategoryIsUpdated()
        {
            var category = _categoryService.GetAll().FirstOrDefault(c => c.Id == _categoryId);
            Assert.IsNotNull(category);
            Assert.AreEqual(_newName, category.Name, true);
        }

        [Then(@"the handler returns an error indicator")]
        public void TheHandlerReturnsAnErrorIndicator()
        {
            var exception = ScenarioContext.Current.Get<Exception>(AddCategoryResultKey);
            Assert.IsNotNull(exception);
        }

        [Then(@"the category name remains '(.*)'")]
        public void ThenTheCategoryNameRemains(string oldName)
        {
            var category = _categoryService.GetAll().FirstOrDefault(c => c.Id == _categoryId);
            Assert.IsNotNull(category);
            Assert.AreEqual(oldName, category.Name, true);
        }

        #endregion
    }
}
