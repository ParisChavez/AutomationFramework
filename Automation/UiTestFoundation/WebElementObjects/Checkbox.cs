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
    /// Represents a Checkbox on a webpage
    /// </summary>
    public class CheckBox : WebElementObject
    {
        public CheckBox(IWebElement element, [CallerMemberName] string creatorName = "") : base(element, creatorName) { }
        public CheckBox(ISearchContext searchContext, By by, [CallerMemberName] string creatorName = "") : base(searchContext, by, creatorName) { }

        /// <summary>
        /// Gets the selection status, or sets selection status of checkbox
        /// </summary>
        public bool Selected
        {
            get
            {
                return Element.Selected;
            }
            set
            {
                if (value != Selected)
                {
                    Click();
                }
            }
        }

        /// <summary>
        /// Clicks the checkbox
        /// </summary>
        public void Click()
        {
            Element.Click();
        }
    }
}
