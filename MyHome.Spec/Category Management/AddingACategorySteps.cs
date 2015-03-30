using System;
using TechTalk.SpecFlow;

namespace MyHome.Spec
{
    [Binding]
    public class AddingACategorySteps
    {
        [Given(@"I have entered ""(.*)"" as the name of the category")]
public void GivenIHaveEnteredAsTheNameOfTheCategory(string p0)
{
    ScenarioContext.Current.Pending();
}

        [Given(@"there is no other category with that name")]
public void GivenThereIsNoOtherCategoryWithThatName()
{
    ScenarioContext.Current.Pending();
}

        [Given(@"there is another category with the same name")]
public void GivenThereIsAnotherCategoryWithTheSameName()
{
    ScenarioContext.Current.Pending();
}

        [When(@"I press add")]
public void WhenIPressAdd()
{
    ScenarioContext.Current.Pending();
}

        [Then(@"the category should be added to the list")]
public void ThenTheCategoryShouldBeAddedToTheList()
{
    ScenarioContext.Current.Pending();
}

        [Then(@"the category is not added to the list")]
public void ThenTheCategoryIsNotAddedToTheList()
{
    ScenarioContext.Current.Pending();
}
    }
}
