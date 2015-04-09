using System;
using System.Linq;
using BusinessLogic;
using Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace MyHome.Spec.Category_Management
{
    [Binding]
    [Scope(Feature = "UpdatingCategory")]

    public class UpdatingCategorySteps
    {
        const string ADD_CATEGORY_RESULT_KEY = "add_category_result";
        BaseCategoryHandler handler;
        string m_categoryType;
        string categoryName;
        string m_currentName;
        string m_newName;
        int categoryID;

        [BeforeScenario]
        public void Setup()
        {
            handler = null;
            m_categoryType = "";
            categoryName = "";
            m_currentName = "";
            m_newName = "";
            categoryID = -1;
            Cache.SDB.Clear();
        }

        #region Given

        [Given(@"The category type is '(.*)'")]
        public void GivenTheCategoryTypeIs(string categoryType)
        {
            m_categoryType = categoryType;
            switch (categoryType)
            {
                case "expense":
                    handler = new ExpenseCategoryHandler();
                    break;
                case "income":
                    handler = new IncomeCategoryHandler();
                    break;
                case "paymentmethod":
                    handler = new PaymentMethodHandler();
                    break;
                default:
                    break;
            }
        }

        [Given(@"the current name is '(.*)'")]
        public void GivenTheCurrentNameIs(string currentName)
        {
            categoryName = m_currentName = currentName;
            categoryID = handler.AddNewCategory(categoryName);
        }

        [Given(@"there is another category with the same name")]
        public void GivenThereIsAnotherCategoryWithTheSameName()
        {
            handler.AddNewCategory(categoryName);
        }

        [Given(@"the '(.*)' already exists")]
        public void GivenTheAlreadyExists(string newName)
        {
            handler.AddNewCategory(newName);
        }

        #endregion

        #region When

        [When(@"I have entered nothing for the name")]
        public void GivenIHaveEnteredNothingForTheName()
        {
            var cat = handler.LoadAll().Where(c => string.Equals(c.Name, categoryName, StringComparison.CurrentCultureIgnoreCase)).First();
            cat.Name = "";
            var result = handler.Save(cat);
            ScenarioContext.Current.Add(ADD_CATEGORY_RESULT_KEY, result);
        }

        [When(@"I change the name to '(.*)'")]
        public void WhenIChangeTheNameTo(string newName)
        {
            var cat = handler.LoadAll().Where(c => string.Equals(c.Name, categoryName, StringComparison.CurrentCultureIgnoreCase)).First();
            cat.Name = m_newName = newName;
            var result = handler.Save(cat);
            ScenarioContext.Current.Add(ADD_CATEGORY_RESULT_KEY, result);
        }

        #endregion

        #region Then

        [Then(@"the category is updated")]
        public void ThenTheCategoryIsUpdated()
        {
            var cat = handler.LoadAll().Where(c => c.Id == categoryID).FirstOrDefault();
            Assert.AreEqual(m_newName, cat.Name, true);
        }

        [Then(@"the handler returns an error indicator (.*)")]
        public void TheHandlerReturnsAnErrorIndicator(string error)
        {
            var errorFromContext = ScenarioContext.Current.Get<object>(ADD_CATEGORY_RESULT_KEY);
            Assert.AreEqual(error, errorFromContext.ToString());
        }

        [Then(@"the category name remains '(.*)'")]
        public void ThenTheCategoryNameRemains(string oldName)
        {
            var cat = handler.LoadAll().Where(c => c.Id == categoryID).FirstOrDefault();
            Assert.AreEqual(oldName, cat.Name, true);
        }

        #endregion
    }
}
