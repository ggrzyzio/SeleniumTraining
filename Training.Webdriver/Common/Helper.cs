using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Webdriver
{
    using System.Text.RegularExpressions;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;

    public static class Helper
    {

        public static string GenerateEmail()
        {
            var newGuid = Guid.NewGuid().ToString().Substring(0, 10);
            return $"{newGuid}@objectivity.co.uk";
        }

        public static int GeneratePostCode()
        {
            var rand = new Random().Next(10000, 99999);
            return rand;
        }

        public static string GenerateStringNoDigits()
        {
            var newGuid = Guid.NewGuid().ToString().Substring(0, 30);
            string replacedstar = Regex.Replace(newGuid, "[0-9]{0,}", "").Replace("-", string.Empty);
            return replacedstar;
        }

        public static void ClearAndSend(this IWebElement element, string text)
        {
            element.Clear();
            element.SendKeys(text);
        }

        public static IWebElement GetElementWait(this IWebDriver driver, By by)
        {
            var tries = 0;
            while (tries < 5)
            {
                try
                {
                    var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5)) { PollingInterval = TimeSpan.FromMilliseconds(200) };
                    var myDynamicElement = wait.Until(d => d.FindElement(by).Enabled);
                    return driver.FindElement(by);
                }
                catch (Exception)
                {
                    tries++;
                }
            }
            throw new ElementNotVisibleException("Did not found element in 5 tries");
        }

        public static bool WaitForPage(IWebDriver webDriver, double timeout)
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeout));

            Func<IWebDriver, bool> angularLoad = driver =>
            {
                try
                {
                    var javaScriptExecutor = driver as IJavaScriptExecutor;
                    var x = ((long)javaScriptExecutor.ExecuteScript(
                         "return angular.element(document).injector().get('$http').pendingRequests.length === 0")
                     == 0);
                    return x;
                }
                catch (Exception)
                {
                    return true;
                }
            };

            Func<IWebDriver, bool> jQueryLoad = driver =>
            {
                try
                {
                    var javaScriptExecutor = driver as IJavaScriptExecutor;
                    return ((long)javaScriptExecutor.ExecuteScript("return jQuery.active") == 0);
                }
                catch (Exception)
                {
                    return true;
                }
            };

            Func<IWebDriver, bool> ajaxLoad = driver =>
            {
                try
                {
                    var javaScriptExecutor = driver as IJavaScriptExecutor;
                    return javaScriptExecutor.ExecuteScript("return document.readyState").ToString().Equals("complete");
                }

                catch (Exception)
                {
                    return true;
                }
            };

            Func<IWebDriver, bool> angularReady = driver =>
            {
                try
                {
                    var javaScriptExecutor = driver as IJavaScriptExecutor;
                    Boolean angularUnDefined = (Boolean)javaScriptExecutor.ExecuteScript("return window.angular === undefined");
                    if (!angularUnDefined)
                    {
                        return (Boolean)javaScriptExecutor.ExecuteScript(
                            "return angular.element(document).injector() === undefined");
                    }

                    return true;
                }
                catch (Exception)
                {
                    return true;
                }
            };

            return wait.Until(jQueryLoad) && wait.Until(ajaxLoad) && wait.Until(angularLoad) && wait.Until(angularReady);
        }
    }
}
