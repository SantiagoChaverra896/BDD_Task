using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using BDD_Task.Page_Objects;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace BDD_Task.Step_Definitions
{
    [Binding]
    public class IndexPageSteps
    {
        private IWebDriver driver;
        private IndexPage indexPage;
        private GenerativeAIPage genAiPage;
        private ResponsibleAIPage respeAIPage;

        public IndexPageSteps()
        {
            driver = new ChromeDriver();
            indexPage = new IndexPage(driver);
            genAiPage = new GenerativeAIPage(driver);
            respeAIPage = new ResponsibleAIPage(driver);
        }

        [Given(@"I navigate to the EPAM website")]
        public void GivenINavigateToTheEPAMWebsite()
        {
            indexPage.Open();
            indexPage.CookieHandler();
        }

        [When(@"I hover over the the Service tab on the Navigation bar")]
        public void WhenIHoverOverTheTheServiceTabOnTheNavigationBar()
        {
            indexPage.HoverServicesTab();
        }

        [When(@"Click one the '([^']*)' links")]
        public void WhenClickOneTheLinks(string p0)
        {
            indexPage.ClickLink(p0);
        }

        [Then(@"The displayed page title should match the '([^']*)' of the previously clicked link")]
        public void ThenTheDisplayedPageTitleShouldMatchTheOfThePreviouslyClickedLink(string p0)
        {
            var pageTitleMap = new Dictionary<string, Func<string>>
            {
                {"Generative AI", () => genAiPage.GetActualTitle()},
                {"Responsible AI", () => respeAIPage.GetActualTitle()}
            };

            if (pageTitleMap.TryGetValue(p0, out var getTitleFunc))
            {
                string actualTitle = getTitleFunc();
                Assert.AreEqual(p0, actualTitle, $"Expected title {p0}, but found {actualTitle}");
            }
            else
            {
                throw new ArgumentException($"No matching page found for title: {p0}");
            }

        }

        [Then(@"The page should display a section named '([^']*)'")]
        public void ThenThePageShouldDisplayASectionNamed(string p0)
        {
            if (p0.Equals("Generative AI")) 
            {
                var OreSection = genAiPage.GetOreSection();
                Assert.IsTrue(OreSection.Displayed, "The Our Related Expertice Section is not displayed.");
            } 
            else if (p0.Equals("Responsible AI"))
            {
                var OreSection = respeAIPage.GetOreSection();
                Assert.IsTrue(OreSection.Displayed, "The Our Related Expertice Section is not displayed.");
            }     
        }

        [AfterScenario]
        public void AfterScenario() 
        {
            driver.Quit();
        }

    }
}
