using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Webdriver.Common
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Interactions;

    public abstract class PageBase
    {
        public Actions actions;

        protected IWebDriver Driver { get; set; }

        protected PageBase(IWebDriver driver)
        {
            this.Driver = driver;
            this.actions = new Actions(this.Driver);
        }

    }
}
