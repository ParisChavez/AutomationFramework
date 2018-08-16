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
    /// Represents an editable text field on a web page
    /// </summary>
    public class TextBox : WebElementObject
    {
        /// <summary>
        /// Ctr for Textbox
        /// </summary>
        /// <param name="element">will throw NoSuchElementException if element is null</param>
        public TextBox(IWebElement element, [CallerMemberName] string creatorName = "") : base(element, creatorName) { }

        /// <summary>
        /// ctr for textbox
        /// </summary>
        /// <param name="searchContext">the search context to find the element in</param>
        /// <param name="by">the search parameters.  If no element found NoSuchElementException will be thrown</param>
        public TextBox(ISearchContext searchContext, By by, [CallerMemberName] string creatorName = "") : base(searchContext, by, creatorName) { }

        /// <summary>
        /// Gets and sets the text currently inside this textbox
        /// </summary>
        public string Text
        {
            get
            {
                return Element.GetAttribute("value");
            }
            set
            {
                ClearText();
                Element.SendKeys(value);
            }
        }

        /// <summary>
        /// Sends an enter key press to this textbox
        /// </summary>
        public void PressEnter()
        {
            Element.SendKeys(Keys.Return);
        }

        /// <summary>
        /// Clears all text from this textbox
        /// </summary>
        public void ClearText()
        {
            Element.Clear();
        }
    }
}
