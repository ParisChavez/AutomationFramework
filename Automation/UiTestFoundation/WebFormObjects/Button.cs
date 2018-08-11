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
    /// Represents a Button on a webpage
    /// Use WaitOnClick after create to set up simple waiting logic on clicks.
    /// </summary>
    public class Button : WebFormObject
    {
        public Button(IWebElement element, [CallerMemberName] string creatorName = "") : base(element, creatorName) { }
        public Button(ISearchContext searchContext, By by, [CallerMemberName] string creatorName = "") : base(searchContext, by, creatorName) { }

        /// <summary>
        /// Perform a click.  
        /// </summary>
        public void Click()
        {
            Element.Click();
        }

        /// <summary>
        /// Get text of button if it is present
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
