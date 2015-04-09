using System;
using System.Linq;
using System.Collections.Generic;
using LocalTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using BusinessLogic;
using Data;

namespace MyHome.Spec
{
    [Binding]
    public class AddingACategorySteps
    {
        BaseCategoryHandler handler;
        string m_categoryType;
        string categoryName;

        [BeforeScenario]
        public void Setup()
        {
            handler = null;
            m_categoryType = "";
            categoryName = "";
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

        [Given(@"I have entered '(.*)' as the name")]
        public void GivenIHaveEnteredAsTheName(string name)
        {
            categoryName = name;
        }

        [Given(@"there is no other category with that name")]
        public void GivenThereIsNoOtherCategoryWithThatName()
        {
            switch (m_categoryType)
            {
                case "expense":
                    {
                        var expenseCatRow = Cache.SDB.t_expenses_category.Where(r => r.NAME.ToLower() == categoryName.ToLower()).FirstOrDefault();
                        if (expenseCatRow != null)
                        {
                            Cache.SDB.t_expenses_category.Removet_expenses_categoryRow(expenseCatRow);
                        }
                        break;
                    }
                case "income":
                    {
                        var incomeCatRow = Cache.SDB.t_incomes_category.Where(r => r.NAME.ToLower() == categoryName.ToLower()).FirstOrDefault();
                        if (incomeCatRow != null)
                        {
                            Cache.SDB.t_incomes_category.Removet_incomes_categoryRow(incomeCatRow);
                        }
                        break;
                    }
                case "paymentmethod":
                    {
                        var paymentMethodRow = Cache.SDB.t_payment_methods.Where(r => r.NAME.ToLower() == categoryName.ToLower()).FirstOrDefault();
                        if (paymentMethodRow != null)
                        {
                            Cache.SDB.t_payment_methods.Removet_payment_methodsRow(paymentMethodRow);
                        }
                        break;
                    }
                default:
                    break;
            }
        }

        [Given(@"there is another category with the same name")]
        public void GivenThereIsAnotherCategoryWithTheSameName()
        {
            handler.AddNewCategory(categoryName);
        }

        [Given(@"I have entered nothing for the name")]
        public void GivenIHaveEnteredNothingForTheName()
        {
            var result = handler.AddNewCategory(categoryName);
            ScenarioContext.Current.Add("add_category_result", result);
        }

        #endregion

        #region When

        [When(@"I press add")]
        public void WhenIPressAdd()
        {
            var result = handler.AddNewCategory(categoryName);
            ScenarioContext.Current.Add("add_category_result", result);
        }

        #endregion

        #region Then

        [Then(@"the handler returns an error indicator (.*)")]
        public void TheHandlerReturnsAnErrorIndicator(string error)
        {
            var errorFromContext = ScenarioContext.Current.Get<int>("add_category_result");
            Assert.AreEqual(int.Parse(error), errorFromContext);
        }

        [Then(@"the category should be added to the list")]
        public void ThenTheCategoryShouldBeAddedToTheList()
        {
            var result = handler.LoadAll().FirstOrDefault(c => string.Equals(c.Name, categoryName));
            Assert.IsNotNull(result);
        }

        #endregion
    }
}
