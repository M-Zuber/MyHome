using System;
using TechTalk.SpecFlow;

namespace MyHome.Spec.Category_Management
{
    [Binding]
    public class UpdatingCategorySteps
    {
        [Given(@"A category with the name ""(.*)""")]
        public void GivenACategoryWithTheName(string p0)
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"a category with the same name")]
        public void GivenACategoryWithTheSameName()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"I press change name")]
        public void WhenIPressChangeName()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"provide ""(.*)"" as the new name")]
        public void WhenProvideAsTheNewName(string p0)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the name is changed")]
        public void ThenTheNameIsChanged()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the name stays the same")]
        public void ThenTheNameStaysTheSame()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
