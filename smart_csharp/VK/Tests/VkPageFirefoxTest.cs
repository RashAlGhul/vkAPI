
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebdriverFramework.Framework.WebDriver;
using WebdriverFramework.VK.Steps;
using WebdriverFramework.VK.VkTaskUtils;

namespace WebdriverFramework.VK.Tests
{
    [TestClass]
    public class VkPageFirefoxTest:BaseTest
    {
        private readonly VkPageSteps _steps = new VkPageSteps();

        private string _message = RandomString.RandomName();

        [TestMethod]
        [DeploymentItem("geckodriver.exe")]
        public override void RunTest()
        {
            _steps.Login();
            _steps.GoToMyPage();
            _steps.CreatePost(_message);
            _message = RandomString.RandomName();
            Assert.IsTrue(_steps.IsPostCreated());
            _steps.EditPost(_message);
            Assert.IsTrue(_steps.IsPostEdited());
            _message = RandomString.RandomName();
            _steps.AddComment(_message);
            Assert.IsTrue(_steps.IsPostCommented());
            _steps.LikePost();
            Assert.IsTrue(_steps.IsUserLikesPost());
            _steps.DeletePost();
            Assert.IsTrue(_steps.IsPostDeleted());
        }
    }
}
