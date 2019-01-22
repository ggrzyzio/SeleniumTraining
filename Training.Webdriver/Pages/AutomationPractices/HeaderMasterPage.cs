using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Webdriver.Pages.AutomationPractices
{
    using NUnit.Framework;

    using OpenQA.Selenium;

    using Training.Webdriver.Common;

    public abstract class HeaderMasterPage : PageBase
    {
        private readonly By logutButton = By.CssSelector("a.logout"),
                            logedUserLocator = By.CssSelector(".account span");

        protected HeaderMasterPage(IWebDriver driver)
            : base(driver)
        {
        }

        public SignInPage ClickLogout()
        {
            this.Driver.GetElementWait(logutButton).Click();
            return new SignInPage(this.Driver);
        }

        public void CheckUserLoggedName(string name)
        {
             Assert.AreEqual(name.ToLower() ,this.Driver.GetElementWait(logedUserLocator).Text.Trim().ToLower());
        }

    }
}
