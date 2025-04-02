using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDD_Task.Page_Objects
{
    internal class ResponsibleAIPage: IndexPage
    {
        public ResponsibleAIPage(IWebDriver driver) : base(driver) { }

        public string GetActualTitle()
        {
            string linkActualTitle = driver.FindElement(By.XPath($"//span[@class='museo-sans-500 gradient-text']")).Text;
            return linkActualTitle;
        }

        public IWebElement GetOreSection()
        {
            return driver.FindElement(oreTitle);
        }


    }
}
