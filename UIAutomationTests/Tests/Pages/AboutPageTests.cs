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
    public class AboutPageTests : BaseUITest
    {

        private AboutPage page;

        [TestInitialize]
        public void LoadPage()
        {
            page = new AboutPage(Browser);
            Browser.WaitForControlReady(1000);
        }
        
        [TestMethod]
        public void TheTitleShouldBe()
        {
            Assert.AreEqual("About", page.GetTitle().InnerText);
        }

        [TestMethod]
        public void TheStraplineShouldBe()
        {
            Assert.AreEqual("Safe-Bank was setup in " + DateTime.Today.AddYears(-7).Year + " as the worlds first supper secure online bank. Today you are bleave we have got to the point where we have acheave are goal so sign up, login and start securing your money.", page.GetText().InnerText);
        }
        
    }
}
