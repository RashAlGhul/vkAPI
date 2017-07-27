using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebdriverFramework.Framework.util;

namespace WebdriverFramework.Framework.WebDriver
{
    /// <summary>
    /// Base test class. Does browser initialization and closes it after test is finished.
    /// </summary>
    [TestClass]
    public abstract class BaseTest: BaseEntity
    {
        /// <summary>
        /// allow marks test as failure or success
        /// if HasWarn has true value test will be failed in the end
        /// this variable used in the Checker class for make soft assertions
        /// </summary>
        public static bool HasWarn;
        /// <summary>
        /// Context of the current test execution
        /// </summary>
        public virtual TestContext TestContext { get; set; }
        
        /// <summary>
        /// override method toString()
        /// </summary>
        /// <returns>name</returns>
        public override string ToString()
        {
            return GetType().Name;
        }

        /// <summary>
        /// Initialization before test case.
        /// </summary>
        [TestInitialize]
        public virtual void InitBeforeTest()
        {
            Checker.Messages = new CheckMessList();
            HasWarn = false;
            Browser = Browser.Instance;
            Browser.NavigateToStartPage();
            try
            {
                Browser.WindowMaximise();
            }
            catch (InvalidOperationException e)
            {
                if (e.Message.Contains("Session not started or terminated")) // Sometimes browserstack terminates connection unexpectedly. Just workaroud below
                {
                    var type = typeof(Browser);
                    type.GetField("_currentInstance", BindingFlags.Static | BindingFlags.NonPublic).SetValue(null, null);
                    type.GetField("_currentDriver", BindingFlags.Static | BindingFlags.NonPublic).SetValue(null, null);
                    type.GetField("_firstInstance", BindingFlags.Static | BindingFlags.NonPublic).SetValue(null, null);
                    InitBeforeTest();
                }
            }
        }

        /// <summary>
        /// should be implemented in childs
        /// </summary>
        public abstract void RunTest();

        /// <summary>
        /// получает список некритичных сообщений и выводит в лог
        /// </summary>
        protected void ProcessingErrors()
        {
            if (HasWarn)
            {
                String allMessages = "";
                Info("====================== See autotest errors below ===============");
                for (int i = 0; i < Checker.Messages.Count; i++)
                {
                    allMessages = allMessages + Checker.Messages[i];
                    Error("Error " + (i + 1) +" : " + Checker.Messages[i]);
                }
                Info("====================== end of errors ======================");
                Assert.IsFalse(HasWarn, allMessages);
            }
        }

        /// <summary>
        /// closing browser
        /// </summary>
        [TestCleanup]
        public virtual void CleanAfterTest()
        {
            try
            {
                ProcessingErrors();
            }
            catch (Exception e)
            {
                Logger.Instance.Fail("Test was finished but there are some issues with result analyzing. Please check that all right configured\r\n" +
                                     "Exception:" + e.Message);
            }
            finally
            {
                SQLConnection.CloseConnectons();
                Browser?.Quit();
            }
        }


        /// <summary>
        /// formats the value for logging "name test - log splitter - the message"
        /// </summary>
        /// <param name="message">message for format</param>
        /// <returns>a formatted value for logging "element type - name - log splitter - the message"</returns>
        protected override string FormatLogMsg(string message)
        {
            return message;
        }
    }
}
