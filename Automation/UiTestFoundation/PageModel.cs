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
    // all page models will derive from this 
    public abstract class PageModel
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

        protected Configuration Config
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

        protected UiTestFixture TestFixture { get; private set; }

        public PageModel() { }

        public PageModel(UiTestFixture testFixture) => TestFixture = testFixture;

        internal void SetParentTestFixture(UiTestFixture testFixture) => TestFixture = testFixture;

        public void GoToUrl(string url) => Driver.Navigate().GoToUrl(url);

        public string Title
        {
            get => Driver.Title;
        }
    }
}
