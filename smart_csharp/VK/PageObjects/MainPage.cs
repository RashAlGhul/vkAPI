using System.Text.RegularExpressions;
using OpenQA.Selenium;
using WebdriverFramework.Framework.WebDriver.Elements;
using WebdriverFramework.VK.VkTaskUtils;

namespace WebdriverFramework.VK.PageObjects
{
    internal class MainPage
    {
        #region locators
        private readonly By _myPage = By.XPath(@"//span[contains(text(),'Моя Страница')]");
        private readonly By _pagePosts = By.XPath(@"//div[@class='_post_content']");

        private By PostInspector(string text)
        {
            return By.XPath($@"//div[@class='wall_post_text'][contains(text(),'{text})]");
        }

        #endregion

        private string _postText;
        private string _commentText;
        private int _postId;
        private readonly RequestResponce _rr = new RequestResponce();

        public void GoToMyPage()
        {
            var myPageLink = new Link(_myPage, "MyPage");
            myPageLink.Click();
        }

        public void CreatePost(string randomText)
        {
            _postText = randomText;
            string getResitlt = _rr.GET(_rr.PostMessage(_postText));
            int.TryParse(Regex.Match(getResitlt, $@"{TestData.GetNumberRegex}").Value, out _postId);
        }

        public void DeletePost()
        {
            _rr.POST(_rr.DeletePost(_postId));
        }

        public void EditPost(string newRandomText)
        {
            _postText = newRandomText;
            _rr.POST(_rr.EditPost(_postId,_postText));
        }

        public void AddComment(string randomComment)
        {
            _commentText = randomComment;
            _rr.POST(_rr.AddComment(_postId, _commentText));
        }

        public string PostLiked()
        {
            return _rr.POST(_rr.LikedPost(_postId));
        }

        public bool IsPostCreated()
        {
            BaseElement post = new MenuItem(PostInspector(_postText), "Post");
            return post.IsPresent();
        }
    }
}
