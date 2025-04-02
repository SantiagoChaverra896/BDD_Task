using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using SeleniumExtras.WaitHelpers;

namespace BDD_Task.Page_Objects
{
    internal class IndexPage
    {
        private static string Url { get; } = "https://www.epam.com";
        protected IWebDriver driver;
        protected WebDriverWait wait;
        protected Actions actions;

        //Locators
        private readonly By servicesTab = By.XPath("//li[@class='top-navigation__item epam'][1]");
        protected readonly By oreTitle = By.XPath("//span[contains(text(),'Our Related Expertise')]");
        protected By acceptCookiesBttn = By.Id("onetrust-accept-btn-handler");
        protected By cookiesBaner = By.XPath("//div[@id='onetrust-banner-sdk']");
        protected By loaderSpinner = By.CssSelector(".preloader");

        // Constructor
        public IndexPage(IWebDriver driver)
        {
            this.driver = driver;
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            this.actions = new Actions(driver);
        }
        public IndexPage Open() 
        {
            driver.Url = Url;
            driver.Manage().Window.Maximize();
            return this;
        }

         public IndexPage HoverServicesTab() 
        {
            actions.MoveToElement(driver.FindElement(servicesTab))
                   .Perform();
            return this;
        }

        public IndexPage ClickLink(string Link) 
        {
            actions.MoveToElement(driver.FindElement(By.XPath($"//a[@class='top-navigation__sub-link'][normalize-space()='{Link}']")))
                   .Click()
                   .Perform();

            return this;
        }

        //Wait helpers
        protected IWebElement WaitForElementToBeClickable(By locator)
        {
            return wait.Until(ExpectedConditions.ElementToBeClickable(locator));
        }

        protected void WaitForElementToDisapear(By locator)
        {
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(locator));
        }

        public void CookieHandler()
        {
            try
            {
                WaitForElementToBeClickable(acceptCookiesBttn);
                // Scroll the element into view
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", driver.FindElement(acceptCookiesBttn));
                // Click using JavaScript to avoid further interaction issues
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", driver.FindElement(acceptCookiesBttn));
                WaitForElementToDisapear(cookiesBaner);
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Cookies banner not found, proceeding...");
            }
        }


    }
}
