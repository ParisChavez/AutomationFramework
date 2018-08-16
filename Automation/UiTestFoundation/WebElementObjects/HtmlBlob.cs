using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TestFoundation;

namespace UiTestFoundation
{
    /// <summary>
    /// used to represent chunks of html that need to be grouped together, such as: popups, divs, search results, records, iframes, etc.
    /// </summary>
    public abstract class HtmlBlob : WebElementObject
    {
        /// <summary>
        /// Test fixture used for Logs, finding test settings, and creating pages
        /// do NOT use the Driver contained in here for searches, use HtmlBlob.Element
        /// </summary>
        protected UiTestFixture TestFixture { get; set; }

        public HtmlBlob(IWebElement enclosingElement, UiTestFixture testFixture, [CallerMemberName] string creatorName = "") : base(enclosingElement, creatorName)
        {
            TestFixture = testFixture ?? throw new ArgumentException("TestFixture is null.");
        }

        public HtmlBlob(ISearchContext searchContext, By by, UiTestFixture testFixture, [CallerMemberName] string creatorName = "") : base(searchContext, by, creatorName)
        {
            TestFixture = testFixture ?? throw new ArgumentException("TestFixture is null.");
        }

        /// <summary>
        /// UiSettings for the current test run.
        /// Use to check for browser, device, etc, which may affect element querying.
        /// </summary>
        protected UiConfiguration UIConfig
        {
            get
            {
                return TestFixture.UiSettings;
            }
        }

        /// <summary>
        /// Log steps, exceptions, decisions
        /// </summary>
        protected Logger Log
        {
            get
            {
                return TestFixture.Log;
            }
        }
    }
}
