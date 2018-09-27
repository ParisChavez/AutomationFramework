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
    /// A collection of radio buttons which share the same name
    /// </summary>
    public class RadioButtonSet
    {
        private Dictionary<string, RadioButton> _radioButtons;
        private ISearchContext _searchContext;
        private readonly string _name;

        /// <summary>
        /// Initializes a collection of radio buttons that are a set on the webpage.  These are linked by a common name attribute.
        /// Value is assumed to exist on each input element, and is unique to each element
        /// </summary>
        /// <param name="searchContext"></param>
        /// <param name="name">Name is assumed to be unique on the page to this set.</param>
        public RadioButtonSet(ISearchContext searchContext, string name)
        {
            _searchContext = searchContext ?? throw new ArgumentException("Search Context for radio buttons cannot be null!");
            _name = name;
        }

        /// <summary>
        /// Queries the ISearchContext for all 
        /// </summary>
        private void PopulateButtons()
        {
            _radioButtons = new Dictionary<string, RadioButton>();

            var elements = _searchContext.FindElements(By.Name(_name));
            if (elements == null)
            {
                return;
            }

            foreach (var element in elements)
            {
                string title = element.GetAttribute("value");
                _radioButtons.Add(title, new RadioButton(element));
            }
        }

        /// <summary>
        /// Determines if the collection should be requeried.
        /// </summary>
        /// <returns></returns>
        private bool NeedToRequeryRadioButtons()
        {
            if (_radioButtons == null)
                return true;

            if (!_radioButtons.Any())
                return true;

            if (_radioButtons.Any(rb => rb.Value.IsElementStale == true))
                return true;

            return false;
        }

        private void RepopulateIfNeeded()
        {
            if (NeedToRequeryRadioButtons())
                PopulateButtons();
        }

        /// <summary>
        /// Gets a collection of the radiobuttons in this set
        /// </summary>
        public IEnumerable<RadioButton> RadioButtons
        {
            get
            {
               RepopulateIfNeeded();
               return _radioButtons.Values;
            }
        }

        /// <summary>
        /// Gets a collection of the available options in this set
        /// </summary>
        public IEnumerable<string> Values
        {
            get
            {
                RepopulateIfNeeded();
                return _radioButtons.Keys;
            }
        }

        /// <summary>
        /// the count of buttons in the set
        /// </summary>
        public int Count
        {
            get
            {
                RepopulateIfNeeded();
                return _radioButtons.Count;
            }
        }

        /// <summary>
        /// gets the radiobutton associated with the provided value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public RadioButton GetRadioButtonByValue(string value)
        {
            RepopulateIfNeeded();
            return _radioButtons[value];
        }

        /// <summary>
        /// selects or deselects the radio button associated with the provided value
        /// </summary>
        /// <param name="value"></param>
        /// <param name="selected"></param>
        public void Select(string value, bool selected)
        {
            RepopulateIfNeeded();
            if (_radioButtons.ContainsKey(value))
            {
                _radioButtons[value].Selected = selected;
            }
        }

        /// <summary>
        /// Checks that all buttons in the set are visible.
        /// Not equivelent to !AllInvisible()
        /// </summary>
        /// <returns></returns>
        public bool AllVisible()
        {
            return !RadioButtons.Any(rb => rb.Displayed == false);
        }

        /// <summary>
        /// Checks that all buttons in the set are invisible.
        /// Not equivelent to !AllVisible()
        /// </summary>
        /// <returns></returns>
        public bool AllInvisible()
        {
            return !RadioButtons.Any(rb => rb.Displayed == true);
        }
    }
}
