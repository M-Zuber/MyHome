using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyHome.DataRepository;
using MyHome.Persistence;
using MyHome.Services;
using TechTalk.SpecFlow;
using MyHome.Spec.Helpers;

namespace MyHome.Spec.CategoryManagement
{
    [Binding]
    [Scope(Feature = "UpdatingCategory")]
    public class UpdatingCategorySteps
    {
        private const string AddCategoryResultKey = "add_category_result";
        private readonly ScenarioContext _scenarioContext;
        private AccountingDataContext _context;
        private string _categoryName;
        private string _newName;
        // ReSharper disable once NotAccessedField.Local
        private int _categoryId;

        ICategoryService<DataClasses.Category> _categoryService;

        public UpdatingCategorySteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }
        [BeforeScenario]
        public void Setup()
        {
            _context = new TestAccountingDataContext();
            _categoryName = "";
            _newName = "";
            _categoryId = -1;
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
        [Given(@"the current name is '(.*)'")]
        public void GivenTheCurrentNameIs(string currentName)
        {
            _categoryName = currentName;
        }
        [Given(@"a category with the name '(.*)'")]
        public void GivenACategoryWithTheName(string categoryName)
        {
            _categoryName = categoryName;
            if (_categoryService.Exists(categoryName))
            {
                _categoryService.Delete(categoryName);
            }
            _categoryService.Create(categoryName, 1);
        }

        [Given(@"there is another category with the same name")]
        public void GivenThereIsAnotherCategoryWithTheSameName()
        {
            _categoryService.Create(_categoryName);
        }

        [Given(@"there is no other category with that name")]
        public void GivenThereIsNoOtherCategoryWithThatName()
        {
            if (_categoryService.Exists(_categoryName))
            {
                _categoryService.Delete(_categoryName);
            }
        }

        [Given(@"I save the category")]
        public void SaveCategory()
        {
            _categoryService.Create(_categoryName);
            var category = _categoryService.GetAll().First(x => x.Name == _categoryName);
            _categoryId = category.Id;
        }

        [Given(@"the '(.*)' already exists")]
        public void GivenTheAlreadyExists(string newName)
        {
            _categoryService.Create(newName);
        }

        [When(@"I have entered nothing for the name")]
        public void GivenIHaveEnteredNothingForTheName()
        {
            var category = _categoryService.GetAll().First(c => string.Equals(c.Name, _categoryName, StringComparison.CurrentCultureIgnoreCase));

            try
            {
                _categoryService.Save(category.Id, "");
            }
            catch (Exception e)
            {
                _scenarioContext.Add(AddCategoryResultKey, e);
            }
        }

        [When(@"I change the name to '(.*)'")]
        public void WhenIChangeTheNameTo(string newName)
        {
            var category = _categoryService.GetAll().First(c => string.Equals(c.Name, _categoryName, StringComparison.CurrentCultureIgnoreCase));
            _newName = newName;

            try
            {
                _categoryService.Save(category.Id, _newName);
            }
            catch (Exception e)
            {
                _scenarioContext.Add(AddCategoryResultKey, e);
            }
        }

        [Then(@"the category is updated")]
        public void ThenTheCategoryIsUpdated()
        {
            var category = _categoryService.GetAll().FirstOrDefault(c => c.Id == 1);
            Assert.IsNotNull(category);
            Assert.AreEqual(_newName, category.Name, true);
        }

        [Then(@"the handler returns an error indicator")]
        public void TheHandlerReturnsAnErrorIndicator()
        {
            var exception = _scenarioContext.Get<Exception>(AddCategoryResultKey);
            Assert.IsNotNull(exception);
        }

        [Then(@"the category name remains '(.*)'")]
        public void ThenTheCategoryNameRemains(string oldName)
        {
            var category = _categoryService.GetAll().FirstOrDefault(c => c.Id == 1);
            Assert.IsNotNull(category);
            Assert.AreEqual(oldName, category.Name, true);
        }
    }
}
