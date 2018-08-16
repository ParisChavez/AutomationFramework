using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace UiTestFoundation
{
    /// <summary>
    /// Drop down menu objects
    /// </summary>
    public class DropDownList : WebElementObject
    {
        /// <summary>
        /// Do not use directly, use Property SelectElement instead.
        /// </summary>
        private SelectElement _selectElement = null;

        public DropDownList(IWebElement element, [CallerMemberName] string creatorName = "") : base(element, creatorName) { }
        public DropDownList(ISearchContext searchContext, By by, [CallerMemberName] string creatorName = "") : base(searchContext, by, creatorName) { }

        /// <summary>
        /// The underlying OpenQA.Selenium.Support.UI.SelectElement for the drop down
        /// </summary>
        protected SelectElement SelectElement
        {
            get
            {
                if (_selectElement == null)
                {
                    _selectElement = new SelectElement(Element);
                }
                return _selectElement;
            }
        }

        /// <summary>
        /// The list of available options for the drop down list
        /// </summary>
        public string[] Options
        {
            get
            {
                var options = _selectElement.Options;
                List<string> optionList = new List<string>();
                foreach (var option in options)
                {
                    optionList.Add(option.Text);
                }

                return optionList.ToArray();
            }
        }

        /// <summary>
        /// Selects an option based on option name
        /// </summary>
        /// <param name="selection">Option to select: Case sensitive</param>
        public void SelectByText(string selection)
        {
            SelectElement.SelectByText(selection);
        }

        /// <summary>
        /// Selects an option by value
        /// </summary>
        /// <param name="value">value of option to select</param>
        public void SelectByValue(string value)
        {
            SelectElement.SelectByValue(value);
        }
    }
}
