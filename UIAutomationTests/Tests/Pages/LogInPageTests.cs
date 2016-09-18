using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIAutomationTests.Pages;

namespace UIAutomationTests.Tests.Pages
{
    [CodedUITest]
    public class LogInPageTests : BaseUITest
    {

        private LogInPage page;

        [TestInitialize]
        public void LoadPage()
        {
            page = new LogInPage(Browser);
            Browser.WaitForControlReady(1000);
        }

        [TestMethod]
        public void TheTitleShouldBe()
        {
            Assert.AreEqual("LogIn", page.GetTitle().InnerText);
        }

        [TestMethod]
        public void LogInAsSystemAdmin()
        {
            var startURI = Browser.Uri.ToString();
            page.EnterUsername("SafeBankAdmin");
            page.EnterPassword("Admin1234");
            page.ClickLogInButton();
            Browser.WaitForControlReady(5000);
            Assert.AreNotEqual(startURI, Browser.Uri.ToString());
            page.LogOut();
        }

        [TestMethod]
        public void LogInAsAUserThatDoseNotExist()
        {
            var startURI = Browser.Uri.ToString();
            page.EnterUsername("NOT_A_USER");
            page.EnterPassword("Admin1234");
            page.ClickLogInButton();
            Browser.WaitForControlReady(5000);
            Assert.AreEqual(startURI, Browser.Uri.ToString());
        }

    }
}
