using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using WebdriverFramework.Framework.WebDriver;
using WebdriverFramework.Framework.WebDriver.Elements;

namespace WebdriverFramework.Tests
{
    [TestClass]
    public class ExampleMSTest : BaseTest
    {
        [TestMethod]
        [DeploymentItem("chromedriver.exe")]
        public override void RunTest()
        {
            LogStep(1, "Open main page");
            var txbSearch = new TextBox(By.XPath("//input[@id='lst-ib']"), "Search");
            txbSearch.SetText("Automation Testing");

            var btnSearch = new Button(By.XPath("//input[@name='btnK']"), "Search");
            btnSearch.ClickAndWaitForLoading();

            var lnkExpectedTestWikipedia =
                new Link(By.XPath("//a[contains(@href,'https://en.wikipedia.org/wiki/Test_automation')]"), "Test_automation Wikipedia");
            Assert.AreEqual(true, lnkExpectedTestWikipedia.IsPresent(), lnkExpectedTestWikipedia.GetName() + " is not present!");
        }
    }
}