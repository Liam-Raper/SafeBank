using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
using System.Collections.Generic;
using UIAutomationTests.Pages.Shared;

namespace UIAutomationTests.Pages
{
    public class JoinPage : BasePage
    {
        public JoinPage(BrowserWindow browser) : base(browser)
        {
            browser.WaitForControlReady(500);
            if (browser.Uri.LocalPath != "/join")
            {
                NavigateToJoin();
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

        public void EnterConfermePassword(string password)
        {
            var element = new HtmlEdit(mainpage);
            element.SearchProperties.Add(HtmlEdit.PropertyNames.Id, "ConfermePassword");
            element.SearchProperties.Add(HtmlEdit.PropertyNames.Name, "ConfermePassword");
            element.SearchProperties.Add(HtmlEdit.PropertyNames.ControlType, "Edit");
            element.Text = password;
        }

        public void EnterEmail(string email)
        {
            var element = new HtmlEdit(mainpage);
            element.SearchProperties.Add(HtmlEdit.PropertyNames.Id, "Email");
            element.SearchProperties.Add(HtmlEdit.PropertyNames.Name, "Email");
            element.SearchProperties.Add(HtmlEdit.PropertyNames.ControlType, "Edit");
            element.Text = email;
        }

        public void SelectQuestion(int questionIndex)
        {
            var element = new HtmlComboBox(mainpage);
            element.SearchProperties.Add(HtmlEdit.PropertyNames.Id, "Question");
            element.SearchProperties.Add(HtmlEdit.PropertyNames.Name, "Question");
            element.SearchProperties.Add(HtmlEdit.PropertyNames.ControlType, "ComboBox");
            element.SearchProperties.Add(HtmlEdit.PropertyNames.TagName, "SELECT");
            element.SelectedIndex = questionIndex;
        }

        public void EnterAnswer(string answer)
        {
            var element = new HtmlEdit(mainpage);
            element.SearchProperties.Add(HtmlEdit.PropertyNames.Id, "Answer");
            element.SearchProperties.Add(HtmlEdit.PropertyNames.Name, "Answer");
            element.SearchProperties.Add(HtmlEdit.PropertyNames.ControlType, "Edit");
            element.Text = answer;
        }

        public void ClickJoinButton()
        {
            var element = new HtmlButton(mainpage);
            element.SearchProperties.Add(HtmlButton.PropertyNames.TagName, "INPUT");
            element.SearchProperties.Add(HtmlButton.PropertyNames.Type, "submit");
            element.SearchProperties.Add(HtmlButton.PropertyNames.DisplayText, "Join");
            Mouse.Click(element);
        }

        public HtmlControl[] GetValidation()
        {
            var elements = new List<HtmlControl>();
            var elementWrapper = new HtmlControl(mainpage);
            elementWrapper.SearchProperties.Add(HtmlControl.PropertyNames.Class, "validation-summary-errors");
            var list = new HtmlControl(elementWrapper);
            list.SearchProperties.Add(HtmlControl.PropertyNames.TagName, "UL");
            list.SearchProperties.Add(HtmlControl.PropertyNames.TagInstance, "1");
            var kids = list.GetChildren().Count;
            for (var index = 1; index <= list.GetChildren().Count; index++)
            {
                var element = new HtmlCustom(list);
                element.SearchProperties.Add(HtmlControl.PropertyNames.TagName, "LI");
                element.SearchProperties.Add(HtmlControl.PropertyNames.TagInstance, index.ToString());
                elements.Add(element);
            }
            return elements.ToArray();
        }

    }
}
