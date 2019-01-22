using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Webdriver.Common
{
    using NUnit.Framework;
    using NUnit.Framework.Internal.Execution;

    public abstract class TestBase
    {
        public IWebDriver Driver;

        [SetUp]
        public void SetUp()
        {
            this.Driver = new ChromeDriver();
            this.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);
            this.Driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromMilliseconds(1000);
            this.Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(7);
            this.Driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void TearDown()
        {
            this.Driver.Quit();
            this.Driver.Dispose();
        }
    }
}
