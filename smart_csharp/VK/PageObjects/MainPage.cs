using OpenQA.Selenium;
using System.Text.RegularExpressions;
using WebdriverFramework.Framework.WebDriver.Elements;
using WebdriverFramework.VK.VkTaskUtils;

namespace WebdriverFramework.VK.PageObjects
{
    internal class MainPage
    {
        #region locators
        private readonly By _myPage = By.XPath(@"//span[contains(text(),'Моя Страница')]");
        private By TextInspector(string text) => By.XPath($@"//div[contains(text(),'{text}')]");
        private By LikeLocator(int postId) => By.XPath($@"//*[@id='post{Regex.Match(TestData.UserId, $@"{TestData.GetNumberRegex}").
            Value}_{postId}']//span[@class='post_like_link _link']");

        #endregion

        private string _postText;
        private string _commentText;
        private int _postId;
        private readonly RequestResponce _rr = new RequestResponce();
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
            string getResult = _rr.POST(_rr.PostMessage(_postText));
            int.TryParse(Regex.Match(getResult, $@"{TestData.GetNumberRegex}").Value, out _postId);
        }

        public void DeletePost()
        {
            _rr.POST(_rr.DeletePost(_postId));
        }

        public string EditPost(string newRandomText)
        {
            _postText = newRandomText;
            return _rr.POST(_rr.EditPost(_postId,_postText));
        }

        public void AddComment(string randomComment)
        {
            _commentText = randomComment;
            _rr.POST(_rr.AddComment(_postId, _commentText));
        }

        public void LikePost()
        {
            BaseElement like = new Button(LikeLocator(_postId),"Like");
            like.Click();
        }

        public bool IsPostLiked()
        {
            string postResult = _rr.POST(_rr.LikedPost(_postId));
            int.TryParse(Regex.Match(postResult, $@"{TestData.GetNumberRegex}").Value,out int flagResult);
            return flagResult == 1;
        }

        public bool IsPostCreated()
        {
            _post = new MenuItem(TextInspector(_postText), "Post");
            return _post.IsPresent();
        }

        public bool IsPostDeleted()
        {
            bool flag = !_post.IsExists();
            return flag;
        }

        public bool IsPostEdited()
        {
            _post = new MenuItem(TextInspector(_postText), "EditPost");
            return _post.IsPresent();
        }

        public bool IsPostCommented()
        {
            _comment = new MenuItem(TextInspector(_commentText), "Comment");
            return _comment.IsPresent();
        }
    }
}
