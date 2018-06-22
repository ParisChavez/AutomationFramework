using System.Linq;
using OpenQA.Selenium;

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
        /// Returns the wrapping element of the current element
        /// </summary>
        public static IWebElement ParentElement(this IWebElement element)
        {
            return element.FindElementNull(By.XPath(".."));
        }
    }
}