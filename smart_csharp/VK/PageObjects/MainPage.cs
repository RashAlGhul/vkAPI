using OpenQA.Selenium;
using System.Text.RegularExpressions;
using WebdriverFramework.Framework.WebDriver.Elements;
using WebdriverFramework.VK.VkTaskUtils;

namespace WebdriverFramework.VK.PageObjects
{
    internal class MainPage
    {
        #region Locators
        private readonly By _myPage = By.XPath(@"//span[contains(text(),'Моя Страница')]");
        private By _textInspector = By.XPath($@"//div[contains(text(),'{_postText}')]");
        private By _likeLocator = By.XPath($@"//*[@id='post{Regex.Match(TestData.UserId, $@"{TestData.GetNumberRegex}").
            Value}_{_postId}']//span[@class='post_like_link _link']");
        private By ImageLocator(string imageId) => By.XPath($@"//a[@href='/{imageId}']");

        #endregion

        private static string _postText;
        private string _commentText;
        private static int _postId;
        private readonly HttpUtil _util = new HttpUtil();
        private BaseElement _post;
        private BaseElement _comment;
        public void GoToMyPage()
        {
            var myPageLink = new Link(_myPage, "MyPage");
            myPageLink.Click();
        }

        public void CreatePost(string randomText)
        {
            _postText = randomText;
            string getResult = _util.POST(_util.PostMessage(_postText));
            int.TryParse(Regex.Match(getResult, $@"{TestData.GetNumberRegex}").Value, out _postId);
        }

        public void DeletePost()
        {
            _util.POST(_util.DeletePost(_postId));
        }

        public void EditPost(string newRandomText)
        {
            _postText = newRandomText;
            _util.POST(_util.EditPost(_postId,_postText));
        }

        public void AddComment(string randomComment)
        {
            _commentText = randomComment;
            _util.POST(_util.AddComment(_postId, _commentText));
        }

        public void LikePost()
        {
            BaseElement like = new Button(_likeLocator,"Like");
            like.Click();
        }

        public bool IsPostLiked()
        {
            string postResult = _util.POST(_util.LikedPost(_postId));
            int.TryParse(Regex.Match(postResult, $@"{TestData.GetNumberRegex}").Value,out int flagResult);
            return flagResult == 1;
        }

        public bool IsPostCreated()
        {
            _post = new MenuItem(_textInspector, "Post");
            return _post.IsPresent();
        }

        public bool IsPostDeleted()
        {
            bool flag = new MenuItem(_textInspector, "Delete").IsExists();
            return flag;
        }

        public bool IsPostEdited()
        {
            _post = new MenuItem(_textInspector, "EditPost");
            return _post.IsPresent() && new MenuItem(ImageLocator(_util.imageId), "Image").IsPresent();
        }

        public bool IsPostCommented()
        {
            _comment = new MenuItem(_textInspector, "Comment");
            return _comment.IsPresent();
        }
    }
}
