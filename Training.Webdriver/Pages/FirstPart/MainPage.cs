using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Webdriver
{
    using System.Security.Policy;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Interactions;

    using Training.Webdriver.Common;
    using Training.Webdriver.Pages;

    public class MainPage : PageBase
    {

        private readonly By loginInput = By.Name("username"),
                            passInput = By.Name("password"),
                            submitButton = By.CssSelector("[type=\"submit\"]"),
                            newWindow = By.CssSelector("[href^=\"/windows/new\"]"),
                            title = By.CssSelector("title"),
                            statusCodes = By.CssSelector("a[href^=\"status_codes\"]");

        public MainPage(IWebDriver driver)
            : base(driver)
        {
        }



        public MainPage GoToIframe()
        {
            this.Driver.Url = "http://the-internet.herokuapp.com/iframe";
            return this;
        }

        public MainPage GoToKendo()
        {
            this.Driver.Url = "https://demos.telerik.com/kendo-ui/dragdrop/index";
            return this;
        }

        public MainPage GoToMainPage()
        {
            this.Driver.Url = "http://the-internet.herokuapp.com/login";
            return this;
        }

        public MainPage GoToDropdown()
        {
            this.Driver.Url = "http://the-internet.herokuapp.com/dropdown";
            return this;
        }

        public MainPage GoToWait2()
        {
            this.Driver.Url = "http://the-internet.herokuapp.com/dynamic_loading/2";
            return this;
        }

        public MainPage GoToAlerts()
        {
            this.Driver.Url = "http://the-internet.herokuapp.com/javascript_alerts";
            return this;
        }

        public MainPage GoToWait1()
        {
            this.Driver.Url = "http://the-internet.herokuapp.com/dynamic_loading/1";
            return this;
        }

        public MainPage GoToStatusCodes()
        {
            this.Driver.Url = "http://the-internet.herokuapp.com/status_codes";
            return this;
        }

        public MainPage GoToNewWindowPage()
        {
            this.Driver.Url = "http://the-internet.herokuapp.com/windows";
            return this;
        }

        public CheckBoxPage GoToCheckboxPage()
        {
            this.Driver.Url = "http://the-internet.herokuapp.com/checkboxes";
            return new CheckBoxPage(this.Driver);
        }

        public MainPage GoToLinkedIn()
        {
            this.Driver.Url = "http://linkedin.com";
            return this;
        }

        private IWebElement GetLoginInput()
        {
            return this.Driver.FindElement(this.loginInput);
        }

        public List<IWebElement> GetStatusLinks()
        {
            return this.Driver.FindElements(this.statusCodes).ToList();
        }

        private IWebElement GetPasswordInput()
        {
            return this.Driver.FindElement(this.passInput);
        }

        private IWebElement GetTitlElement()
        {
            return this.Driver.FindElement(this.title);
        }

        private IWebElement GetNewWindow()
        {
            return this.Driver.FindElement(this.newWindow);
        }

        public MainPage FillUser()
        {
            this.GetLoginInput().SendKeys("tomsmith");
            return this;
        }

        public MainPage ClickNewWindow()
        {
            this.GetNewWindow().Click();
            return this;
        }

        public MainPage FillPassword()
        {
            this.GetPasswordInput().SendKeys("SuperSecretPassword!");
            return this;
        }

        public MainPage ClearAndFillPassword(string text)
        {
              this.GetLoginInput().ClearAndSend(text);
            return this;
        }

        public MainPage ClearAndFillUsername(string text)
        {
            this.GetLoginInput().ClearAndSend(text);
            return this;
        }

        private IWebElement GetSubmitButton()
        {
            return this.Driver.FindElement(this.submitButton);
        }

        public MainPage ClickSubmit()
        {
            this.GetSubmitButton().Click();
            return this;
        }

        public string GetTitle()
        {
            return this.Driver.Title;
        }


    }
}
