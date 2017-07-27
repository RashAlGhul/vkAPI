using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebdriverFramework.Framework.WebDriver;
using WebdriverFramework.VK.Steps;
using WebdriverFramework.VK.VkTaskUtils;

namespace WebdriverFramework.VK.Tests
{
    [TestClass]
    public class VkPageTest:BaseTest
    {
        private readonly VkPageSteps _steps = new VkPageSteps();

        private string message = RandomString.RandomName();

        [TestMethod]
        [DeploymentItem("chromedriver.exe")]
        public override void RunTest()
        {
            _steps.Login();
            _steps.GoToMyPage();
            _steps.CreatePost(message);
            message = RandomString.RandomName();
            _steps.EditPost(message);
            message = RandomString.RandomName();
            _steps.AddComment(message);
            _steps.UserLikesPost();
            _steps.DeletePost();
        }
    }
}
