using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace UiTestFoundation
{
    public static class SeleniumExtensions
    {
        /// <summary>
        /// Custom extension:
        /// Finds the first IWebElement given the search parameters, null if not found
        /// </summary>
        //public static IWebElement FindElementNull(this ISearchContext context, By by)
        //{
        //    var elements = context.FindElements(by);
        //    return elements.Any() ? elements[0] : null;
        //}

        /// <summary>
        /// Custom extension:
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
        /// Waits for the element found using the by to become visible 
        /// </summary>
        public static void WaitForElementVisible(this ISearchContext context, By by, TimeSpan timeout)
        {
            var wait = new DefaultWait<ISearchContext>(context);
            wait.Timeout = timeout;
            wait.Until(ctx =>
            {
                var elem = ctx.FindElementNull(by);
                if (elem == null)
                    return false;

                return elem.Displayed;
            });
        }

        /// <summary>
        /// Custom extension:
        /// Waits for the element found using the by to become invisible 
        /// </summary>
        public static void WaitForElementInvisible(this ISearchContext context, By by, TimeSpan timeout)
        {
            var wait = new DefaultWait<ISearchContext>(context);
            wait.Timeout = timeout;
            wait.Until(ctx =>
            {
                var elem = ctx.FindElementNull(by);
                if (elem == null)
                    return true;

                return !elem.Displayed;
            });
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

        /// <summary>
        /// Custom extension:
        /// Detects if an element is stale, and needs to be found again.
        /// Staleness can happen after a page change, such as a refresh or navigation.
        /// </summary>
        /// <returns>True if stale, false otherwise</returns>
        public static bool IsStale(this IWebElement element)
        {
            try
            {
                bool enabled = element.Enabled;
                return false;
            }
            catch (StaleElementReferenceException)
            {
                return true;
            }
        }

        /// <summary>
        /// Gets the value of a javascript object and returns it as a dictionary
        /// </summary>
        /// <param name="objectName">The name of the javascript object to find</param>
        /// <returns>Dictionary of the values stored in the javascript object.</returns>
        public static Dictionary<string, object> GetJavascriptObject(this IWebDriver driver, string objectName)
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            Dictionary<string, object> jsObject = null;
            try
            {
                jsObject = (Dictionary<string, object>)executor.ExecuteScript($"return {objectName};");
            }
            catch (WebDriverException)
            {
            }

            return jsObject;
        }
    }
}
