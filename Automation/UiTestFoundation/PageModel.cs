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
            SearchContext = searchContext ?? throw new ArgumentException("Search context is null, when creating HTML blob!");
            TestFixture = testFixture ?? throw new ArgumentException("Test Fixture is null, when creating HTML blob!"); ;
        }

        protected TestConfiguration Config
        {
            get
            {
                return TestFixture.Config;
            }
        }

        protected UiConfiguration UIConfig
        {
            get
            {
                return TestFixture.UiSettings;
            }
        }

        protected Logger Log
        {
            get
            {
                return TestFixture.Log;
            }
        }
    }

    /// <summary>
    /// The basis for all page models, assumed to be the entire range of the Driver root
    /// </summary>
    public abstract class PageModel : HtmlBlob
    {

        /// <summary>
        /// Webdriver.  Used to find all elements needed to perform actions on the page
        /// </summary>
        protected IWebDriver Driver
        {
            get
            {
                if (TestFixture == null)
                {
                    throw new ArgumentException("Parent TextFixture was not set on page creation.");
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