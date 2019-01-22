namespace Training.Webdriver.Pages.AutomationPractices
{
    using System;
    using System.Globalization;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using OpenQA.Selenium;

    using Training.Webdriver.Common;
    using Training.Webdriver.Models;

    public class CreateUserPage : HeaderMasterPage
    {
        private By radioMr = By.CssSelector("input#id_gender1"),
                   radioMrs = By.CssSelector("input#id_gender2"),
                   inputFirstName = By.CssSelector("input#customer_firstname"),
                   inputLastName = By.CssSelector("input#customer_lastname"),
                   inputEmail = By.CssSelector("input#email"),
                   passwd = By.CssSelector("input#passwd"),
                   dropdownDays = By.CssSelector("select#days"),
                   dropdownMonths = By.CssSelector("select#months"),
                   dropdownYears = By.CssSelector("select#years"),
                   firstNameAddress = By.CssSelector("input#firstname"),
                   lastNameAddress = By.CssSelector("input#lastname"),
                   companyAddress = By.CssSelector("input#company"),
                   addressFirst = By.CssSelector("input#address1"),
                   addressSecond = By.CssSelector("input#address2"),
                   addressCity = By.CssSelector("input#city"),
                   state = By.CssSelector("select#id_state"),
                   postCode = By.CssSelector("input#postcode"),
                   infoInput = By.CssSelector("textarea#other"),
                   inputPhoneHome = By.CssSelector("input#phone"),
                   inputPhoneMobile = By.CssSelector("input#phone_mobile"),
                   registerButton = By.XPath("//button[span[contains(text(),\'Register\')]]");

        public CreateUserPage(IWebDriver driver) : base(driver)
        {
        }

        public UserPage FillUserData(User user)
        {
            return this.FillMrMrsRadio(user.IsMr)
                .FillFirstName(user.FirsName)
                .FillLastName(user.LastName)
                .CheckInputEmail(user.Email)
                .FillPassword(user.Password)
                .SelectDaysFromDropdown(user.DateOfBirth)
                .SelectMonthFromDropdown(user.DateOfBirth)
                .SelectYearFromDropdown(user.DateOfBirth)
                .CheckFirstNameInAddress(user.FirsName)
                .CheckLastNameAddress(user.LastName)
                .FillCompanyName(user.Company)
                .FillCity(user.City)
                .FillAddressLineOne(user.Address)
                .FillAddressLineTwo(user.Address2)
                .FillState(user.State)
                .FillPostCode(user.PostCode)
                .FillInfoInput(user.AdditionalInfo)
                .FillInputPhoneHome(user.PhoneHome)
                .FillInputPhoneMobile(user.PhoneMobile)
                .SendForm();

        }

        private CreateUserPage FillMrMrsRadio(bool isMr)
        {
            if (isMr)
            {
                this.GetRadioMr().Click();
                return this;
            }
            this.GetRadioMrs().Click();
            return this;
        }

        private CreateUserPage FillFirstName(string name)
        {
            this.GetInputFirstName().ClearAndSend(name);
            return this;
        }

        private CreateUserPage FillLastName(string name)
        {
            this.GetInputLastName().ClearAndSend(name);
            return this;
        }

        private CreateUserPage CheckInputEmail(string email)
        {
            var filledEmail = this.GetInputEmail().GetAttribute("value");
            Assert.AreEqual(email, filledEmail);
            return this;
        }

        private CreateUserPage FillPassword(string pass)
        {
            this.GetInputPassword().ClearAndSend(pass);
            return this;
        }

        private CreateUserPage SelectDaysFromDropdown(DateTime birthDate)
        {
            var drop = this.GetDropdownDays();
            drop.Click();
            var birthDay = birthDate.Day.ToString();
            var d = this.GetDropdownDays().FindElements(By.CssSelector("option"));
            d.FirstOrDefault(x => x.Text.Trim() == birthDay).Click();
            return this;
        }


        private CreateUserPage SelectMonthFromDropdown(DateTime birthDate)
        {
            var drop = this.GetDropdownMonths();
            drop.Click();
            var birthMonth = birthDate.ToString("MMMM", CultureInfo.InvariantCulture).ToLower();
            var d = drop.FindElements(By.CssSelector("option")).FirstOrDefault(x => x.Text.Trim().ToLower() == birthMonth);
            d.Click();
            return this;
        }

        private CreateUserPage SelectYearFromDropdown(DateTime birthDate)
        {
            var drop = this.GetDropdownYears();
            drop.Click();
            var birthYear = birthDate.Year;
            if (birthYear >= DateTime.Now.Year)
            {
                birthYear = new Random().Next(1900, 2000);
            }

            drop.FindElements(By.CssSelector("option")).FirstOrDefault(x => x.Text.Trim() == birthYear.ToString()).Click();
            return this;
        }

        private CreateUserPage CheckFirstNameInAddress(string name)
        {
            var filledName = this.GetAddressFirstName().GetAttribute("value");
            Assert.AreEqual(name, filledName);
            return this;
        }

        private CreateUserPage CheckLastNameAddress(string name)
        {
            var filledName = this.GetAddressLastName().GetAttribute("value");
            Assert.AreEqual(name, filledName);
            return this;
        }

        private CreateUserPage FillCompanyName(string company)
        {
            this.GetCompanyNameAddress().ClearAndSend(company);
            return this;
        }

        private CreateUserPage FillAddressLineOne(string text)
        {
            this.GetAddressFirstLine().ClearAndSend(text);
            return this;
        }

        private CreateUserPage FillAddressLineTwo(string text)
        {
            this.GetAddressSecondLine().ClearAndSend(text);
            return this;
        }

        private CreateUserPage FillCity(string text)
        {
            this.GetAddressCity().ClearAndSend(text);
            return this;
        }

        private CreateUserPage FillState(State state)
        {
            var drop = this.GetAddressStateDropdown();
            drop.Click();
            drop.FindElements(By.CssSelector("option")).FirstOrDefault(x=>x.Text == state.ToString()).Click();
            return this;
        }

        private CreateUserPage FillPostCode(int code)
        {
            this.GetAddressPostCode().ClearAndSend(code.ToString());
            return this;
        }

        private CreateUserPage FillInfoInput(string text)
        {
            this.GetInfoInput().ClearAndSend(text);
            return this;
        }

        private CreateUserPage FillInputPhoneHome(int text)
        {
            this.GetInputHomePhone().ClearAndSend(text.ToString());
            return this;
        }

        private CreateUserPage FillInputPhoneMobile(int text)
        {
            this.GetInputMobilePhone().ClearAndSend(text.ToString());
            return this;
        }

        private UserPage SendForm()
        {
            this.GetRegisterButton().Click();
            Helper.WaitForPage(this.Driver, 5);
            return new UserPage(this.Driver);
        }




        private IWebElement GetRadioMr()
        {
            return this.Driver.GetElementWait(this.radioMr);
        }

        private IWebElement GetRadioMrs()
        {
            return this.Driver.GetElementWait(this.radioMrs);
        }

        private IWebElement GetInputFirstName()
        {
            return this.Driver.GetElementWait(this.inputFirstName);
        }

        private IWebElement GetInputLastName()
        {
            return this.Driver.GetElementWait(this.inputLastName);
        }

        private IWebElement GetInputEmail()
        {
            return this.Driver.GetElementWait(this.inputEmail);
        }

        private IWebElement GetInputPassword()
        {
            return this.Driver.GetElementWait(this.passwd);
        }

        private IWebElement GetDropdownDays()
        {
            return this.Driver.GetElementWait(this.dropdownDays);
        }

        private IWebElement GetDropdownMonths()
        {
            return this.Driver.GetElementWait(this.dropdownMonths);
        }

        private IWebElement GetDropdownYears()
        {
            return this.Driver.GetElementWait(this.dropdownYears);
        }

        private IWebElement GetAddressFirstName()
        {
            return this.Driver.GetElementWait(this.firstNameAddress);
        }

        private IWebElement GetAddressLastName()
        {
            return this.Driver.GetElementWait(this.lastNameAddress);
        }

        private IWebElement GetCompanyNameAddress()
        {
            return this.Driver.GetElementWait(this.companyAddress);
        }

        private IWebElement GetAddressFirstLine()
        {
            return this.Driver.GetElementWait(this.addressFirst);
        }

        private IWebElement GetAddressSecondLine()
        {
            return this.Driver.GetElementWait(this.addressSecond);
        }

        private IWebElement GetAddressCity()
        {
            return this.Driver.GetElementWait(this.addressCity);
        }

        private IWebElement GetAddressStateDropdown()
        {
            return this.Driver.GetElementWait(this.state);
        }

        private IWebElement GetAddressPostCode()
        {
            return this.Driver.GetElementWait(this.postCode);
        }

        private IWebElement GetInfoInput()
        {
            return this.Driver.GetElementWait(this.infoInput);
        }

        private IWebElement GetInputHomePhone()
        {
            return this.Driver.GetElementWait(this.inputPhoneHome);
        }

        private IWebElement GetInputMobilePhone()
        {
            return this.Driver.GetElementWait(this.inputPhoneMobile);
        }

        private IWebElement GetRegisterButton()
        {
            return this.Driver.GetElementWait(this.registerButton);
        }
    }
}
