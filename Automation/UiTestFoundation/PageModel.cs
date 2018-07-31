using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TestFoundation;

namespace UiTestFoundation
{
    /// <summary>
    /// Represents the pages of the site.
    /// </summary>
    public abstract class PageModel
    {
        protected UiTestFixture TestFixture { get; set; }

        /// <summary>
        /// Access the browser navagation methods.
        /// </summary>
        public navigate Navigate { get; private set; }

        /// <summary>
        /// Check to see if user is on the page.  Override for each page.
        /// </summary>
        public PageModel(UiTestFixture testFixture)
        {
            TestFixture = testFixture ?? throw new ArgumentException("TestFixture is null.");
            Navigate = new navigate(Driver);
        }

        /// <summary>
        /// Test configuration object containing all test setttings.
        /// </summary>
        protected TestConfiguration Config => TestFixture.Config;

        /// <summary>
        /// UiSettings for the current test run.
        /// Use to check for browser, device, etc, which may affect element querying.
        /// </summary>
        protected UiConfiguration UIConfig => TestFixture.UiSettings;

        /// <summary>
        /// Log steps, exceptions, decisions
        /// </summary>
        protected Logger Log => TestFixture.Log;

        /// <summary>
        /// The driver to use in locating all elements on the page.
        /// </summary>
        protected IWebDriver Driver => TestFixture.Driver;

        /// <summary>
        /// Go directly to the page.  Override for each page.
        /// </summary>
        public abstract void Go();

        /// <summary>
        /// Check to see if user is on the page.  Override for each page.
        /// </summary>
        public abstract bool IsAt();

        /// <summary>
        /// Get the title of the current page.
        /// </summary>
        public string Title => Driver.Title;

        public class navigate
        {
            private IWebDriver _driver;
            public navigate(IWebDriver driver) => _driver = driver;

            /// <summary>
            /// Go to arbritrary url
            /// </summary>
            public void GoToUrl(string url) => _driver.Navigate().GoToUrl(url);

            /// <summary>
            /// Refresh the page
            /// </summary>
            public void Refresh() => _driver.Navigate().Refresh();

            /// <summary>
            /// Use the browser's Go Back button
            /// </summary>
            public void GoBack() => _driver.Navigate().Back();

            /// <summary>
            /// Use the browser's Go Forward button
            /// </summary>
            public void GoForward() => _driver.Navigate().Forward();
        }
    }
}