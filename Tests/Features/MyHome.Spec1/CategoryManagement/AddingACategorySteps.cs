using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyHome.DataRepository;
using MyHome.Persistence;
using MyHome.Services;
using MyHome.Spec.Helpers;
using TechTalk.SpecFlow;

namespace MyHome.Spec.CategoryManagement
{
    [Binding]
    [Scope(Feature = "AddingACategory")]
    public class AddingACategorySteps
    {
        private AccountingDataContext _context;
        private ICategoryService<DataClasses.Category> _categoryService;
        private string _categoryName;
        private readonly ScenarioContext _scenarioContext;

        public AddingACategorySteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public void Setup()
        {
            _context = new TestAccountingDataContext();
            _categoryService = null;
            _categoryName = "";
        }

        [AfterScenario]
        public void TearDown()
        {
            _context.Database.Delete();
        }

        [Given(@"The category type is '(.*)'")]
        public void GivenTheCategoryTypeIs(string categoryType)
        {
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (categoryType)
            {
                case "expense":
                    _categoryService = new ExpenseCategoryService(new ExpenseCategoryRepository(_context));
                    break;
                case "income":
                    _categoryService = new IncomeCategoryService(new IncomeCategoryRepository(_context));
                    break;
                case "paymentmethod":
                    _categoryService = new PaymentMethodService(new PaymentMethodRepository(_context));
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
                _scenarioContext.Add("add_category_result", e);
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
                _scenarioContext.Add("add_category_result", e);
            }
        }

        [When(@"I press add")]
        public void WhenIPressAdd()
        {
            try
            {
                _categoryService.Create(_categoryName);
            }
            catch (Exception e)
            {
                _scenarioContext.Add("add_category_result", e);
            }
        }

        [Then(@"the handler returns an error indicator")]
        public void TheHandlerReturnsAnErrorIndicator()
        {
            var exception = _scenarioContext.Get<Exception>("add_category_result");
            Assert.IsNotNull(exception);
        }

        [Then(@"the category should be added to the list")]
        public void ThenTheCategoryShouldBeAddedToTheList()
        {
            var result = _categoryService.GetAll().FirstOrDefault(c => string.Equals(c.Name, _categoryName));
            Assert.IsNotNull(result);
        }
    }
}
