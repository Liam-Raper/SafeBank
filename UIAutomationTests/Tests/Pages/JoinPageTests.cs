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
    public class JoinPageTests : BaseUITest
    {
        
        private JoinPage page;

        [TestInitialize]
        public void LoadPage()
        {
            page = new JoinPage(Browser);
            Browser.WaitForControlReady(1000);
        }

        [TestMethod]
        public void TheTitleShouldBe()
        {
            Assert.AreEqual("Join", page.GetTitle().InnerText);
        }
        
        [TestMethod]
        public void WhenNoValuesAreEntered()
        {
            page.ClickJoinButton();
            var validation = page.GetValidation();
            Assert.AreEqual(4, validation.Length);
        }

        [TestMethod]
        public void WhenAUserThatExistsAlreadyIsJoining()
        {
            var dateAndTimeOfTest = DateTime.Now.ToString("yyyyMMddHHmmss");
            page.EnterUsername("AutomationCustomer_" + dateAndTimeOfTest);
            page.EnterPassword("Password123");
            page.EnterConfermePassword("Password123");
            page.EnterEmail("t@t.com");
            page.SelectQuestion(1);
            page.EnterAnswer("Test");
            page.ClickJoinButton();
            Browser.WaitForControlReady(5000);
            page = page.NavigateToJoin();
            var startingUrl = Browser.Uri.ToString();
            page.EnterUsername("AutomationCustomer_" + dateAndTimeOfTest);
            page.EnterPassword("Password123");
            page.EnterConfermePassword("Password123");
            page.EnterEmail("t@t.com");
            page.SelectQuestion(1);
            page.EnterAnswer("Test");
            page.ClickJoinButton();
            Browser.WaitForControlReady(5000);
            var validation = page.GetValidation();
            Assert.AreEqual(1, validation.Length);
            Assert.AreEqual("Unable to create a user with the details you gave are you user your not already in the system?", validation[0].InnerText);
        }

    }
}
