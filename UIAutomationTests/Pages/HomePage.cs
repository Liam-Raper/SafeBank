using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
using System.Collections.Generic;
using UIAutomationTests.Pages.Shared;

namespace UIAutomationTests.Pages
{
    public class HomePage : BasePage
    {
        public HomePage(BrowserWindow browser) : base(browser)
        {
            browser.WaitForControlReady(500);
            if (browser.Uri.LocalPath != "/home" && browser.Uri.LocalPath != "/")
            {
                NavigateToHome();
            }
        }

        public HtmlControl GetTitle()
        {
            var element = new HtmlControl(mainpage);
            element.SearchProperties.Add(HtmlControl.PropertyNames.TagName, "h1");
            element.SearchProperties.Add(HtmlControl.PropertyNames.TagInstance, "1");
            return element;
        }

        public HtmlControl GetStrapline()
        {
            var element = new HtmlControl(mainpage);
            element.SearchProperties.Add(HtmlControl.PropertyNames.TagName, "p");
            element.SearchProperties.Add(HtmlControl.PropertyNames.TagInstance, "1");
            return element;
        }

        public HtmlControl GetWhatWeOfferTitle()
        {
            var element = new HtmlControl(mainpage);
            element.SearchProperties.Add(HtmlControl.PropertyNames.TagName, "H2");
            element.SearchProperties.Add(HtmlControl.PropertyNames.TagInstance, "1");
            return element;
        }

        public HtmlControl[] GetWhatWeOfferList()
        {
            var elements = new List<HtmlControl>();
            var listelement = new HtmlControl(mainpage);
            listelement.SearchProperties.Add(HtmlControl.PropertyNames.TagName, "OL");
            listelement.SearchProperties.Add(HtmlControl.PropertyNames.TagInstance, "1");
            for (var index = 1; index <= listelement.GetChildren().Count; index++)
            {
                var element = new HtmlCustom(listelement);
                element.SearchProperties.Add(HtmlControl.PropertyNames.TagName, "LI");
                element.SearchProperties.Add(HtmlControl.PropertyNames.TagInstance, index.ToString());
                elements.Add(element);
            }
            return elements.ToArray();
        }

        public HtmlControl GetWhatYouCanDoTitle()
        {
            var element = new HtmlControl(mainpage);
            element.SearchProperties.Add(HtmlControl.PropertyNames.TagName, "H2");
            element.SearchProperties.Add(HtmlControl.PropertyNames.TagInstance, "2");
            return element;
        }

        public HtmlControl[] GetWhatYouCanDoList()
        {
            var elements = new List<HtmlControl>();
            var listelement = new HtmlControl(mainpage);
            listelement.SearchProperties.Add(HtmlControl.PropertyNames.TagName, "OL");
            listelement.SearchProperties.Add(HtmlControl.PropertyNames.TagInstance, "2");
            for (var index = 1; index <= listelement.GetChildren().Count; index++)
            {
                var element = new HtmlCustom(listelement);
                element.SearchProperties.Add(HtmlControl.PropertyNames.TagName, "LI");
                element.SearchProperties.Add(HtmlControl.PropertyNames.TagInstance, index.ToString());
                elements.Add(element);
            }
            return elements.ToArray();
        }

    }
}
