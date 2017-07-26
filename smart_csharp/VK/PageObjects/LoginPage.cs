using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using WebdriverFramework.Framework.WebDriver.BrowserBuilder;

namespace WebdriverFramework.VK.PageObjects
{
    internal class LoginPage:BasePage
    {
        private By _loginField = By.XPath(@"//input[@id='index_email']");
        private By _passwordField = By.XPath(@"//input[@id='index_email']");

        public void Longin()
        {
            
        }

        public LoginPage(IWebDriverBuilder driver) : base(driver)
        {
        }
    }
}
