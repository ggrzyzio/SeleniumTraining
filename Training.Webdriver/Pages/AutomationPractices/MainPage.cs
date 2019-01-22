namespace Training.Webdriver.Pages.AutomationPractices
{
    using OpenQA.Selenium;

    using Training.Webdriver.Common;

    public class MainPage : HeaderMasterPage
    {
        private By signInButton = By.CssSelector("a.login");

        public MainPage(IWebDriver driver) : base(driver)
        { 
        }

        public MainPage GoToMainPage()
        {
            this.Driver.Url = "http://automationpractice.com/index.php";
            return this;
        }

        public SignInPage ClickSignInButton()
        {
            GetSignInButton().Click();
            Helper.WaitForPage(this.Driver, 5);
            return new SignInPage(this.Driver);
        }

        private IWebElement GetSignInButton()
        {
            return this.Driver.GetElementWait(this.signInButton);
        }


    }
}
