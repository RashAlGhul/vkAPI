using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using WebdriverFramework.Framework.WebDriver.BrowserBuilder;

namespace WebdriverFramework.VK.PageObjects
{
    internal class BasePage
    {
        protected IWebDriverBuilder Driver;
        public BasePage(IWebDriverBuilder driver)
        {
            this.Driver = driver;
        }

        public void NavigateHere()
        {
            
        }
    }
}
