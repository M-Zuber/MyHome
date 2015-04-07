using System;
using System.Linq;
using System.Collections.Generic;
using LocalTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace MyHome.Spec
{
    [Binding]
    public class AddingACategorySteps
    {
        CatListTempMocker list;
        BaseCategory category;

        [BeforeScenario]
        public void Setup()
        {
            list = new CatListTempMocker();
            category = null;
        }

        [Given(@"I have entered ""(.*)"" as the name of the category")]
        public void GivenIHaveEnteredAsTheNameOfTheCategory(string name)
        {
            category = new BaseCategory { Name = name };
        }

        [Given(@"there is no other category with that name")]
        public void GivenThereIsNoOtherCategoryWithThatName()
        {
            list.RemoveCategory(category);
        }

        [Given(@"there is another category with the same name")]
        public void GivenThereIsAnotherCategoryWithTheSameName()
        {
            list.AddCategory(new BaseCategory { Name = category.Name});
        }

        [When(@"I press add")]
        public void WhenIPressAdd()
        {
                list.AddCategory(category);

        }

        [Then(@"the category should be added to the list")]
        public void ThenTheCategoryShouldBeAddedToTheList()
        {
            Assert.IsTrue(list.ContainsCategory(category));
        }

        [Then(@"the category is not added to the list")]
        public void ThenTheCategoryIsNotAddedToTheList()
        {
            Assert.IsFalse(list.ContainsCategory(category));
        }
    }

    class CatListTempMocker
    {
        List<BaseCategory> list { get; set; }

        public CatListTempMocker()
        {
            list = new List<BaseCategory>();
        }

        public void AddCategory(BaseCategory cat)
        {
            if (!list.Any(c => string.Equals(c.Name,cat.Name)) && !string.IsNullOrWhiteSpace(cat.Name))
            {
                list.Add(cat);
            }
        }

        public void RemoveCategory(BaseCategory cat)
        {
            list.Remove(cat);
        }

        public bool ContainsCategory(BaseCategory cat)
        {
            return list.Contains(cat);
        }
    }
}
