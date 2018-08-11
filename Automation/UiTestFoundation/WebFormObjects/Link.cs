using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace UiTestFoundation
{
    /// <summary>
    /// Represents a hypertext link on the page
    /// </summary>
    public class Link : WebFormObject
    {
        public Link(IWebElement element, [CallerMemberName] string creatorName = "") : base(element, creatorName) { }
        public Link(ISearchContext searchContext, By by, [CallerMemberName] string creatorName = "") : base(searchContext, by, creatorName) { }

        /// <summary>
        /// Clicks on the link
        /// </summary>
        public void Click()
        {
            Element.Click();
        }

        /// <summary>
        /// Gets the text of the link
        /// </summary>
        public string Text
        {
            get
            {
                return Element.Text;
            }
        }
    }
}
