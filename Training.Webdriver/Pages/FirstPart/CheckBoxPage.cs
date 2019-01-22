using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Webdriver.Pages
{
    using NUnit.Framework;

    using OpenQA.Selenium;

    using Training.Webdriver.Common;

    public class CheckBoxPage : PageBase
    {
        private readonly By checkboxes = By.CssSelector("input[type=\"checkbox\"]");

        public CheckBoxPage(IWebDriver driver)
            : base(driver)
        {
        }

        public CheckBoxPage SelectCheckBOx(int whichCheckbox, out bool previousSelection)
        {
            var checkboxes = this.GetCheckBoxes();
            try
            {
                previousSelection = checkboxes[whichCheckbox].Selected;
                checkboxes[whichCheckbox].Click();
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new Exception(
                    $"Number of checkboxes on page is lower then {whichCheckbox + 1}, please change your test or check the page for changes");
            }

            return this;
        }

        public CheckBoxPage CheckIfCheckboxChangedStatusAfterClick(int whichCheckbox, bool previous)
        {
            Assert.IsTrue(previous != this.GetCheckBoxes()[whichCheckbox].Selected);
            return this;
        }

        private List<IWebElement> GetCheckBoxes()
        {
            return this.Driver.FindElements(this.checkboxes).ToList();
        }
    }
}
