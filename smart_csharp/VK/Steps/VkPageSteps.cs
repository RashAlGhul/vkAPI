using WebdriverFramework.VK.PageObjects;

namespace WebdriverFramework.VK.Steps
{
    internal class VkPageSteps
    {
        private readonly LoginPage _lp = new LoginPage();
        private readonly MainPage _mp = new MainPage();

        public void Login()
        {
            
            _lp.Longin();
        }

        public void GoToMyPage()
        {
           _mp.GoToMyPage();
        }

        public void CreatePost(string randomText)
        {
            _mp.CreatePost(randomText);
        }

        public void DeletePost()
        {
             _mp.DeletePost();
        }

        public string EditPost(string newRandomText)
        {
            return _mp.EditPost(newRandomText);
        }

        public bool IsPostCreated()
        {
            return _mp.IsPostCreated();
        }

        public void AddComment(string randomComment)
        {
            _mp.AddComment(randomComment);
        }

        public void LikePost()
        {
            _mp.LikePost();
        }

        public bool IsUserLikesPost()
        {
            return _mp.IsPostLiked();
        }

        public bool IsPostEdited()
        {
            return _mp.IsPostEdited();
        }

        public bool IsPostDeleted()
        {
            return _mp.IsPostDeleted();
        }

        public bool IsPostCommented()
        {
            return _mp.IsPostCommented();
        }
    }
}
