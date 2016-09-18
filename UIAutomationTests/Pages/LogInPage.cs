using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
using UIAutomationTests.Pages.Shared;

namespace UIAutomationTests.Pages
{
    public class LogInPage : BasePage
    {
        public LogInPage(BrowserWindow browser) : base(browser)
        {
            if (browser.Uri.LocalPath != "/login")
            {
                NavigateToLogIn();
            }
        }

        public HtmlControl GetTitle()
        {
            var element = new HtmlControl(mainpage);
            element.SearchProperties.Add(HtmlControl.PropertyNames.TagName, "h1");
            element.SearchProperties.Add(HtmlControl.PropertyNames.TagInstance, "1");
            return element;
        }

        public void EnterUsername(string username)
        {
            var element = new HtmlEdit(mainpage);
            element.SearchProperties.Add(HtmlEdit.PropertyNames.Id, "Username");
            element.SearchProperties.Add(HtmlEdit.PropertyNames.Name, "Username");
            element.SearchProperties.Add(HtmlEdit.PropertyNames.ControlType, "Edit");
            element.Text = username;
        }

        public void EnterPassword(string password)
        {
            var element = new HtmlEdit(mainpage);
            element.SearchProperties.Add(HtmlEdit.PropertyNames.Id, "Password");
            element.SearchProperties.Add(HtmlEdit.PropertyNames.Name, "Password");
            element.SearchProperties.Add(HtmlEdit.PropertyNames.ControlType, "Edit");
            element.Text = password;
        }

        public void ClickLogInButton()
        {
            var element = new HtmlButton(mainpage);
            element.SearchProperties.Add(HtmlButton.PropertyNames.TagName, "INPUT");
            element.SearchProperties.Add(HtmlButton.PropertyNames.Type, "submit");
            element.SearchProperties.Add(HtmlButton.PropertyNames.DisplayText, "Log In");
            Mouse.Click(element);
        }

    }
}
