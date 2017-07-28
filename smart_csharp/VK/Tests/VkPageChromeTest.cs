﻿using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebdriverFramework.Framework.WebDriver;
using WebdriverFramework.VK.Steps;
using WebdriverFramework.VK.VkTaskUtils;

namespace WebdriverFramework.VK.Tests
{
    [TestClass]
    public class VkPageChromeTest:BaseTest
    {
        private readonly VkPageSteps _steps = new VkPageSteps();

        private string x, _message = RandomString.RandomName();

        [TestMethod]
        [DeploymentItem("chromedriver.exe")]
        public override void RunTest()
        {
            _steps.Login();
            _steps.GoToMyPage();
            _steps.CreatePost(_message);
            _message = RandomString.RandomName();
            Assert.IsTrue(_steps.IsPostCreated());
            x =_steps.EditPost(_message);
            Assert.IsTrue(_steps.IsPostEdited());
            _message = RandomString.RandomName();
            _steps.AddComment(_message);
            Assert.IsTrue(_steps.IsPostCommented());
            _steps.LikePost();
            Assert.IsTrue(_steps.IsUserLikesPost());
            Thread.Sleep(5000);
            _steps.DeletePost();
            Assert.IsTrue(_steps.IsPostDeleted());
        }
    }
}
