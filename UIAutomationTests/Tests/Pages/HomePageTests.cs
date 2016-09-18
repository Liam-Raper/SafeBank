using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomationTests.Pages;

namespace UIAutomationTests.Tests.Pages
{
    [CodedUITest]
    public class HomePageTests : BaseUITest
    {

        private HomePage page;
        
        [TestInitialize]
        public void LoadPage()
        {
            page = new HomePage(Browser);
            Browser.WaitForControlReady(1000);
        }

        [TestMethod]
        public void TheTitleShouldBe()
        {
            Assert.AreEqual("Welcome to Safe-Bank the safest way to bank", page.GetTitle().InnerText);
        }

        [TestMethod]
        public void TheStraplineShouldBe()
        {
            Assert.AreEqual("Safe-Bank is the best and securest bank ever.", page.GetStrapline().InnerText);
        }

        [TestMethod]
        public void TheListOfWhatIsOfferedShouldBe()
        {
            Assert.AreEqual("What we offer", page.GetWhatWeOfferTitle().InnerText);
            var list = page.GetWhatWeOfferList();
            Assert.AreEqual(3, list.Length);
            Assert.AreEqual("Current accounts", list[0].InnerText);
            Assert.AreEqual("Saving accounts", list[1].InnerText);
            Assert.AreEqual("Personal loans", list[2].InnerText);
        }

        [TestMethod]
        public void TheListOFWhatCanBeDoneShouldBe()
        {
            Assert.AreEqual("What can you do on are supper secure web site", page.GetWhatYouCanDoTitle().InnerText);
            var list = page.GetWhatYouCanDoList();
            Assert.AreEqual(3, list.Length);
            Assert.AreEqual("Manage your accounts", list[0].InnerText);
            Assert.AreEqual("Transfer money to another account", list[1].InnerText);
            Assert.AreEqual("Take out a loan", list[2].InnerText);
        }

    }
}
