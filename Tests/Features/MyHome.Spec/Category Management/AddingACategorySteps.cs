using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyHome.DataRepository;
using MyHome.Persistence;
using MyHome.Services;
using TechTalk.SpecFlow;
using MyHome.Spec.Helpers;

namespace MyHome.Spec
{
    [Binding]
    [Scope(Feature = "AddingACategory")]
    public class AddingACategorySteps
    {
        AccountingDataContext context;
        ICategoryService<DataClasses.Category> _categoryService;
        string _categoryName;

        [BeforeScenario]
        public void Setup()
        {
            context = new TestAccountingDataContext();
            _categoryService = null;
            _categoryName = "";
        }

        [AfterScenario]
        public void TearDown()
        {
            context.Database.Delete();
        }

        #region Given

        [Given(@"The category type is '(.*)'")]
        public void GivenTheCategoryTypeIs(string categoryType)
        {
            switch (categoryType)
            {
                case "expense":
                    _categoryService = new ExpenseCategoryService(new ExpenseCategoryRepository(context));
                    break;
                case "income":
                    _categoryService = new IncomeCategoryService(new IncomeCategoryRepository(context));
                    break;
                case "paymentmethod":
                    _categoryService = new PaymentMethodService(new PaymentMethodRepository(context));
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
                _categoryService.Delete(_categoryName);
            }
        }

        [Given(@"there is another category with the same name")]
        public void GivenThereIsAnotherCategoryWithTheSameName()
        {
            try
            {
                _categoryService.Create(_categoryName);
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
                _categoryService.Create(_categoryName);
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
                _categoryService.Create(_categoryName);
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
