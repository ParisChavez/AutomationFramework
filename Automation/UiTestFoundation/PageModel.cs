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
    public interface IPageInfo
    {
        /// <summary>
        /// The HTML source of the current page.
        /// </summary>
        string PageSource { get; }

        /// <summary>
        /// Query the page to get any javascript object
        /// </summary>
        /// <param name="jsObjectName">Name of the javascript object</param>
        /// <returns>A dictionary containing the name and objects.  Typically a set of nested dictionaries.</returns>
        Dictionary<string, object> GetJavascriptObject(string jsObjectName);
    }

    public interface IPageNavigate
    {
        void GoToUrl(string url);

        /// <summary>
        /// Refresh the page
        /// </summary>
        void Refresh();

        /// <summary>
        /// Use the browser's Go Back button
        /// </summary>
        void GoBack();

        /// <summary>
        /// Use the browser's Go Forward button
        /// </summary>
        void GoForward();
    }

    /// <summary>
    /// Represents the pages of the site.
    /// </summary>
    public abstract class PageModel : IPageInfo, IPageNavigate
    {
        protected UiTestFixture TestFixture { get; set; }

        /// <summary>
        /// Get information composing the source of the page
        /// </summary>
        public IPageInfo PageInfo
        {
            get => this;
        }

        /// <summary>
        /// Access the browser navagation methods.
        /// </summary>
        public IPageNavigate Navigate
        {
            get => this;
        }

        /// <summary>
        /// Check to see if user is on the page.  Override for each page.
        /// </summary>
        public PageModel(UiTestFixture testFixture)
        {
            TestFixture = testFixture ?? throw new ArgumentException("TestFixture is null.");
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
        /// Error message to display in asserts using the IsAt() method.
        /// </summary>
        public abstract string IsAtErrorMessage();

        /// <summary>
        /// Get the title of the current page.
        /// </summary>
        public string Title => Driver.Title;

        /// <summary>
        /// Query the page to get any javascript object
        /// </summary>
        /// <param name="jsObjectName">Name of the javascript object</param>
        /// <returns>A dictionary containing the name and objects.  Typically a set of nested dictionaries.</returns>
        Dictionary<string, object> IPageInfo.GetJavascriptObject(string jsObjectName) => Driver.GetJavascriptObject(jsObjectName);

        /// <summary>
        /// The HTML source of the current page.
        /// </summary>
        string IPageInfo.PageSource => Driver.PageSource;

        /// <summary>
        /// Go to arbritrary url
        /// </summary>
        void IPageNavigate.GoToUrl(string url) => Driver.Navigate().GoToUrl(url);

        /// <summary>
        /// Refresh the page
        /// </summary>
        void IPageNavigate.Refresh() => Driver.Navigate().Refresh();

        /// <summary>
        /// Use the browser's Go Back button
        /// </summary>
        void IPageNavigate.GoBack() => Driver.Navigate().Back();

        /// <summary>
        /// Use the browser's Go Forward button
        /// </summary>
        void IPageNavigate.GoForward() => Driver.Navigate().Forward();
    }
}