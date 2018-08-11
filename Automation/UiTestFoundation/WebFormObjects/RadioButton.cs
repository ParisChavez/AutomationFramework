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
    /// Represents a Radio Button on a webpage
    /// </summary>
    public class RadioButton : WebFormObject
    {
        public RadioButton(IWebElement element, [CallerMemberName] string creatorName = "") : base(element, creatorName) { }
        public RadioButton(ISearchContext searchContext, By by, [CallerMemberName] string creatorName = "") : base(searchContext, by, creatorName) { }

        /// <summary>
        /// Gets the selection status, or sets selection status of radio button
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
        /// Clicks the radio button
        /// </summary>
        public void Click()
        {
            Element.Click();
        }
    }
}
