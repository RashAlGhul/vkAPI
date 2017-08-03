using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using OpenQA.Selenium;
using WebdriverFramework.Framework.WebDriver.BrowserBuilder;
using WebdriverFramework.Framework.WebDriver.Elements;

namespace WebdriverFramework.VK.PageObjects
{
    internal class LoginPage
    {
        private readonly By _loginField = By.XPath(@"//input[@id='index_email']");
        private readonly By _passwordField = By.XPath(@"//input[@id='index_pass']");
        private readonly By _loginButton = By.XPath(@"//button[@id='index_login_button']");

        public void Login()
        {
            var login = new TextBox(_loginField, "Login");
            var pass = new TextBox(_passwordField,"Password");
            var lButton = new Button(_loginButton,"submit");
            login.SetText(TestData.Login);
            pass.SetText(TestData.Password);
            lButton.Click();
        }
    }
}
