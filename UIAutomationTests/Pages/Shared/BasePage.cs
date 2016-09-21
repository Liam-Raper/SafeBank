using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;

namespace UIAutomationTests.Pages.Shared
{
    public class BasePage
    {

        protected BrowserWindow _browser;
        protected HtmlCustom navbar;
        protected HtmlCustom mainpage;

        public BasePage(BrowserWindow browser)
        {
            _browser = browser;
            navbar = new HtmlCustom(_browser);
            navbar.SearchProperties.Add(HtmlCustom.PropertyNames.TagName, "UL");
            navbar.SearchProperties.Add(HtmlCustom.PropertyNames.TagInstance, "1");
            mainpage = new HtmlCustom(_browser);
            mainpage.SearchProperties.Add(HtmlCustom.PropertyNames.Id, "MainBody");
            mainpage.SearchProperties.Add(HtmlCustom.PropertyNames.ControlType, "Pane");
        }
        
        public HomePage NavigateToHome()
        {
            var link = new HtmlHyperlink(navbar);
            link.SearchProperties.Add(HtmlHyperlink.PropertyNames.InnerText, "Home");
            Mouse.Click(link);
            return new HomePage(_browser);
        }

        public AboutPage NavigateToAbout()
        {
            var link = new HtmlHyperlink(navbar);
            link.SearchProperties.Add(HtmlHyperlink.PropertyNames.InnerText, "About");
            Mouse.Click(link);
            return new AboutPage(_browser);
        }

        public LogInPage NavigateToLogIn()
        {
            var link = new HtmlHyperlink(navbar);
            link.SearchProperties.Add(HtmlHyperlink.PropertyNames.InnerText, "Log In");
            Mouse.Click(link);
            return new LogInPage(_browser);
        }

        public JoinPage NavigateToJoin()
        {
            var link = new HtmlHyperlink(navbar);
            link.SearchProperties.Add(HtmlHyperlink.PropertyNames.InnerText, "Join");
            Mouse.Click(link);
            return new JoinPage(_browser);
        }

        public void LogOut()
        {
            var link = new HtmlHyperlink(navbar);
            link.SearchProperties.Add(HtmlHyperlink.PropertyNames.InnerText, "Log Out");
            Mouse.Click(link);
        }

    }
}
