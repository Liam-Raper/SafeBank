using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
using UIAutomationTests.Pages.Shared;
using Microsoft.VisualStudio.TestTools.UITesting;

namespace UIAutomationTests.Pages
{
    public class AboutPage : BasePage
    {
        public AboutPage(BrowserWindow browser) : base(browser)
        {
            if (browser.Uri.LocalPath != "/about")
            {
                NavigateToAbout();
            }
        }

        public HtmlControl GetTitle()
        {
            var element = new HtmlControl(mainpage);
            element.SearchProperties.Add(HtmlControl.PropertyNames.TagName, "h1");
            element.SearchProperties.Add(HtmlControl.PropertyNames.TagInstance, "1");
            return element;
        }

        public HtmlControl GetText()
        {
            var element = new HtmlControl(mainpage);
            element.SearchProperties.Add(HtmlControl.PropertyNames.TagName, "p");
            element.SearchProperties.Add(HtmlControl.PropertyNames.TagInstance, "1");
            return element;
        }


    }
}
