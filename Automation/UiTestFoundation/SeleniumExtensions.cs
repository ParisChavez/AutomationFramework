using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace UiTestFoundation
{
    public static class SeleniumExtensions
    {
        /// <summary>
        /// Finds the first IWebElement given the search parameters, null if not found
        /// </summary>
        public static IWebElement FindElementNull(this IWebDriver driver, By by)
        {
            var elements = driver.FindElements(by);
            return elements.Any() ? elements[0] : null;
        }

        /// <summary>
        /// Finds the first IWebElement given the search parameters, null if not found
        /// </summary>
        public static IWebElement FindElementNull(this IWebElement element, By by)
        {
            var elements = element.FindElements(by);
            return elements.Any() ? elements[0] : null;
        }

        /// <summary>
        /// Finds the first IWebElement given the search parameters, null if not found
        /// </summary>
        public static IWebElement FindElementNull(this ISearchContext context, By by)
        {
            var elements = context.FindElements(by);
            return elements.Any() ? elements[0] : null;
        }

        /// <summary>
        /// Returns the wrapping element of the current element
        /// </summary>
        public static IWebElement ParentElement(this IWebElement element)
        {
            return element.FindElementNull(By.XPath(".."));
        }
        
        
        /// <summary>
        /// Custom extension:
        /// Returns the first sibling of the current element
        /// </summary>
        public static IWebElement FirstSibling(this IWebElement element, string tagOfSibling)
        {
            return element.FindElementNull(By.XPath($"following-sibling::{tagOfSibling}"));
        }

        /// <summary>
        /// Custom extension:
        /// Waits until all ajax calls are complete.  
        /// Uses the javascript method: "return jQuery.active == 0"
        /// </summary>
        public static void WaitForAjaxComplete(this IWebDriver driver, TimeSpan timeout)
        {
            WebDriverWait ajaxWait = new WebDriverWait(driver, timeout);
            ajaxWait.Until(d => (bool)(d as IJavaScriptExecutor).ExecuteScript("return jQuery.active == 0"));
        }

        /// <summary>
        /// Custom extension:
        /// Waits until the element located with the by is visible
        /// </summary>
        public static void WaitForElementVisible(this IWebDriver driver, By by, TimeSpan timeout)
        {
            WebDriverWait elementInvisibleWait = new WebDriverWait(driver, timeout);
            elementInvisibleWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(by));
        }

        /// <summary>
        /// Custom extension:
        /// Waits until the element located with the by is invisible
        /// </summary>
        public static void WaitForElementInvisible(this IWebDriver driver, By by, TimeSpan timeout)
        {
            WebDriverWait elementInvisibleWait = new WebDriverWait(driver, timeout);
            elementInvisibleWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(by));
        }
    }
}
