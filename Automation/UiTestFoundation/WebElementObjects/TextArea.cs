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
    /// Represents an area of text on the page (h tags, dt, dd, spans, divs, footers, etc)
    /// </summary>
    public class TextArea : WebElementObject
    {
        public TextArea(IWebElement element, [CallerMemberName] string creatorName = "") : base(element, creatorName) { }
        public TextArea(ISearchContext searchContext, By by, [CallerMemberName] string creatorName = "") : base(searchContext, by, creatorName) { }

        /// <summary>
        /// Gets the text displayed in the text area
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
