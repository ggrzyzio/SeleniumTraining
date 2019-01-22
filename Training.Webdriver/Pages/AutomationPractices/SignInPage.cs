namespace Training.Webdriver.Pages.AutomationPractices
{
    using System;
    using OpenQA.Selenium;

    using Training.Webdriver.Common;

    public class SignInPage : HeaderMasterPage
    {
        private By createAccountEmailInput = By.CssSelector("input#email_create"),
                   createAccountButton = By.CssSelector("button#SubmitCreate"),
                   inputEmail = By.CssSelector("input#email"),
                   inputPass = By.Id("passwd"),
                   submitLogin = By.Id("SubmitLogin");
        




        public SignInPage(IWebDriver driver)
            : base(driver)
        {
        }

        public CreateUserPage FillEmailInputToCreateAccountAndSubmit(string text)
        {
            this.GetCreateAccountEmailInput().ClearAndSend(text);
            this.GetCreateAccountButton().Click();
            Helper.WaitForPage(this.Driver, 5);
            return new CreateUserPage(this.Driver);
        }

        public UserPage LogInWithCredentials(string email, string pass)
        {
            this.Driver.GetElementWait(this.inputEmail).ClearAndSend(email);
            this.Driver.GetElementWait(this.inputPass).ClearAndSend(pass);
            this.Driver.GetElementWait(this.submitLogin).Click();
            return new UserPage(this.Driver);
        }


        private IWebElement GetCreateAccountEmailInput()
        {
            return this.Driver.GetElementWait(this.createAccountEmailInput);
        }

        private IWebElement GetCreateAccountButton()
        {
            return this.Driver.GetElementWait(this.createAccountButton);
        }

        private IWebElement GetSignInLoginInput()
        {
            return this.Driver.GetElementWait(this.inputEmail);
        }

        private IWebElement GetPassInput()
        {
            return this.Driver.GetElementWait(this.inputPass);
        }
    }
}
