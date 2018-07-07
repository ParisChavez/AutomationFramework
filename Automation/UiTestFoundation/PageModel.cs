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
    /// used to represent chunks of html that need to be grouped together, such as: pages, popups, search results, records, iframes, etc.
    /// </summary>
    public abstract class HtmlBlob
    {
        protected UiTestFixture TestFixture { get; set; }
        protected ISearchContext SearchContext { get; set; }

        public HtmlBlob(ISearchContext searchContext, UiTestFixture testFixture)
        {
            SearchContext = searchContext;
            TestFixture = testFixture;
        }

        protected TestConfiguration Config
        {
            get
            {
                if (TestFixture == null)
                {
                    throw new ArgumentException("Parent TextFixture was not set on object creation.");
                }

                return TestFixture.Config;
            }
        }
    }

    // all page models will derive from this 
    public abstract class PageModel : HtmlBlob
    {
        protected IWebDriver Driver
        {
            get
            {
                if (TestFixture == null)
                {
                    throw new ArgumentException("Parent TextFixture was not set on object creation.");
                }

                return TestFixture.Driver;
            }
        }

        public abstract void Go();

        public abstract bool IsAt();

        public PageModel(UiTestFixture testFixture) : base(testFixture.Driver, testFixture) { }

        public void GoToUrl(string url) => Driver.Navigate().GoToUrl(url);

        public string Title
        {
            get => Driver.Title;
        }
    }
}