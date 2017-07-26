using OpenQA.Selenium;
using WebdriverFramework.VK.VkTaskUtils;

namespace WebdriverFramework.VK.PageObjects
{
    internal class MainPage
    {
        #region locators
        private By _myPage = By.XPath(@"//span[contains(text(),'Моя Страница')]");
        private By _pagePosts = By.XPath(@"//div[@class='_post_content']");
        #endregion
    }
}
