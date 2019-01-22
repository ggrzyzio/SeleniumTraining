using System;

namespace Training.Webdriver
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Threading;

    using NUnit.Framework;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;

    using Training.Webdriver.Common;

    using OpenQA.Selenium.Remote;

    [TestFixture]
    public class SeleniumWebDriverTrainingTest : TestBase
    {
        public const string pass = "SuperSecretPassword!";
        public const string login = "tomsmith";
         
        [Test]
        public void TestLogin()
        {
            var mainPage = new MainPage(this.Driver);
            mainPage.GoToMainPage()
                .FillPassword()
                .FillUser()
                .ClickSubmit();
        }

        [Test]
        public void TestNewWindow()
        {
            var mainPage = new MainPage(this.Driver);
            var window = mainPage
                .GoToNewWindowPage()
                .ClickNewWindow();

            var current = this.Driver.CurrentWindowHandle;
            foreach (var item in this.Driver.WindowHandles)
            {
                if (item != current)
                {
                    this.Driver.SwitchTo().Window(item);
                }
            }
            var newPage = new MainPage(this.Driver).GetTitle();
            Assert.That(newPage == "New Window");
        }

        [Test]
        public void TestAssert()
        {
            var mainPage = new MainPage(this.Driver).GoToStatusCodes();

            List<string[]> list = new List<string[]>();
            var links = mainPage.GetStatusLinks();
            var first = links.FirstOrDefault();
            var currentHandle = this.Driver.CurrentWindowHandle;
            foreach (var link in links)
            {
                var linkText = link.Text;
                ((IJavaScriptExecutor)this.Driver).ExecuteScript("window.open();");
                var href = link.GetAttribute("href");
                this.Driver
                    .SwitchTo()
                    .Window(this.Driver.WindowHandles.FirstOrDefault(x => x != currentHandle));
                this.Driver.Url = href;
                Thread.Sleep(5000);
                list.Add(new string[] { href, linkText });
                this.Driver.Close();
                this.Driver.SwitchTo().Window(currentHandle);
                Thread.Sleep(5000);
            }

            foreach (var item in list)
            {
                Assert.IsTrue(item[0].Contains(item[1]));
            }

        }

        [Test]
        public void TestLinkedIn()
        {
            var mainPage = new MainPage(this.Driver)
                .GoToLinkedIn();

            By locator = By.CssSelector(".ghp-footer__links");

            var links = this.Driver.FindElement(locator).FindElements(By.CssSelector("li"));
            var sal = "Salaries";
            var salaries = links.FirstOrDefault(x => string.Equals(x.Text, sal, StringComparison.CurrentCultureIgnoreCase));

            salaries.Click();
            Thread.Sleep(5000);
        }

        [Test]
        public void TestCheckbox()
        {
            var mainPage = new MainPage(this.Driver).GoToCheckboxPage();
            Thread.Sleep(1000);
            var checkboxes = this.Driver.FindElements(By.CssSelector("input[type=\"checkbox\"]"));

            foreach (var item in checkboxes)
            {
                var previous = item.Selected;
                item.Click();
                Assert.IsTrue(previous!=item.Selected);
            }
        }

        [Test]
        public void TestClear()
        {
            var mainPage = new MainPage(this.Driver)
                .GoToMainPage()
                .ClearAndFillPassword(pass+"WRONG")
                .ClearAndFillUsername(login+"WRONG")
                .ClickSubmit()
                .ClearAndFillPassword(pass)
                .ClearAndFillUsername(login)
                .ClickSubmit();

        }

        [Test]
        public void TestDropdown()
        {
            var dropdown = By.CssSelector("#dropdown");
            var dropdownOptions = By.CssSelector("#dropdown option");

            var mainPage = new MainPage(this.Driver).GoToDropdown();

            this.Driver.FindElement(dropdown).Click();
            this.Driver.FindElements(dropdownOptions).FirstOrDefault(x=>x.GetAttribute("value") == "2").Click();
            
            Thread.Sleep(2000);
        }


        [Test]
        public void TestWait()
        {
            By start = By.CssSelector("div#start >button");
            By finish = By.CssSelector("div#finish h4");

            var mainPage = new MainPage(this.Driver).GoToWait1();
            this.Driver.GetElementWait(start).Click();
            var fText = this.Driver.GetElementWait(finish).Text;
            Assert.IsTrue(fText.Equals("Hello World!"));
            mainPage.GoToWait2();
            this.Driver.GetElementWait(start).Click();
            this.Driver.GetElementWait(finish);
            Assert.IsTrue(this.Driver.FindElement(finish).Text.Equals("Hello World!"));

        }

        [Test]
        public void TestAlerts()
        {
            By loc = By.CssSelector("ul li button");

            var mainPage = new MainPage(this.Driver).GoToAlerts();

            var list = this.Driver.FindElements(loc);
            var errors = new List<string>();
            foreach (var item in list)
            {
                item.Click();
                Thread.Sleep(1000);
                var errorText = this.Driver.SwitchTo().Alert().Text;

                this.Driver.SwitchTo().Alert().Accept();
                errors.Add(errorText);
                Thread.Sleep(1000);
            }

            foreach (var error in errors)
            {
                Assert.NotNull(error);
            }
        }

        [Test]
        public void TestJavascript()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)this.Driver;
            js.ExecuteScript("window.location.href = 'http://the-internet.herokuapp.com'");
            var title = js.ExecuteScript("return document.getElementsByTagName(\'title\')[0].innerHTML;");
            Thread.Sleep(1000);
        }

        [Test]
        public void TestJavaScriptRedirector()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)this.Driver;
            js.ExecuteScript("window.location.href = 'http://the-internet.herokuapp.com/redirector'");
             js.ExecuteScript("document.getElementById(\"redirect\").click();");
            var title = js.ExecuteScript("window.onload = function () { return document.getElementsByTagName(\'title\')[0].innerHTML; }");
            Debug.WriteLine(title);
            Thread.Sleep(1000);
        }

        [Test]
        public void TestIframe()
        {
            var mainPage = new MainPage(this.Driver).GoToIframe();

            var list = this.Driver.FindElements(By.CssSelector("iframe"));
            var first = list.FirstOrDefault();

            By loc = By.CssSelector("body");

            this.Driver.SwitchTo().Frame(first);
            this.Driver.GetElementWait(loc).ClearAndSend("Pomelo");
            Thread.Sleep(7000);

        }

        [Test]
        public void TestKendoActions()
        {
            var complete = "You did great!";
            var mainPage = new MainPage(this.Driver).GoToKendo();
            By loc = By.CssSelector("div#draggable");
            By targ = By.CssSelector("div#droptarget");
            var actions = mainPage.actions;
            actions.ClickAndHold(this.Driver.FindElement(loc)).Perform();
            var size = this.Driver.Manage().Window.Size;
            var x = size.Width / 2;
            var y = size.Height / 2;
            for (var i = 0; i < 10; i++)
            {
                if (i==0)
                {
                    actions.MoveByOffset(-x, -y).Perform();
                }
                if (i % 4 == 0)
                {
                    actions.MoveByOffset(0, y).Perform();
                }
                if (i % 4 == 1)
                {
                    actions.MoveByOffset(x, 0).Perform();
                }
                if (i % 4 == 2)
                {
                    actions.MoveByOffset(0, -y).Perform();
                }
                if (i % 4 == 3)
                {
                    actions.MoveByOffset(-x, 0).Perform();
                }
                Thread.Sleep(10);
            }
            mainPage.actions.MoveToElement(this.Driver.GetElementWait(targ)).Release().Perform();
            Assert.AreEqual(complete, this.Driver.GetElementWait(targ).Text);
        }


    }

}

