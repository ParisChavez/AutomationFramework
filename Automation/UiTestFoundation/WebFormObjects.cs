using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace UiTestFoundation
{

    /// <summary>
    /// Abstract class for specific web form objects
    /// </summary>
    public abstract class WebFormObject
    {
        protected IWebElement _element;
        protected IWebElement Element
        {
            get
            {
                if (_element == null)
                {
                    throw new NoSuchElementException("Object does not exist");
                }

                return _element;
            }

            set => _element = value;
        }

        public WebFormObject(IWebElement element) => Element = element;

        public WebFormObject(ISearchContext searchContext, By by) => Element = searchContext.FindElementNull(by);

        // include the following constructors in any derived classes, replacing the class name:
        /*
        public DerivedWebFormObject(IWebElement element) : base(element) { }
        public DerivedWebFormObject(ISearchContext searchContext, By by) : base(searchContext, by) { }
        */

        /// <summary>
        /// Checks if object exists on the webpage
        /// </summary>
        public bool Exists
        {
            get
            {
                return _element != null;
            }

        }

        /// <summary>
        /// Checks if the object is displayed on the page
        /// </summary>
        public bool Displayed
        {
            get { return Element != null && Element.Displayed; }
        }

        public bool Enabled
        {
            get { return Element != null && Element.Displayed; }
        }
    }

    /// <summary>
    /// Represents a text field on a web page
    /// </summary>
    public class TextBox : WebFormObject
    {
        public TextBox(IWebElement element) : base(element) { }
        /*
        public TextBox(IWebElement parentElement, By by) : base(parentElement, by) { }
        public TextBox(IWebDriver driver, By by) : base(driver, by) { }
        */
        public TextBox(ISearchContext searchContext, By by) : base(searchContext, by) { }

        public void SetText(string text)
        {
            Element.SendKeys(text);
        }

        public void PressEnter()
        {
            Element.SendKeys(Keys.Return);
        }

        public void ClearText()
        {
            Element.Clear();
        }
    }

    /// <summary>
    /// Represents a Checkbox on a webpage
    /// </summary>
    public class RadioButton : WebFormObject
    {
        public RadioButton(IWebElement element) : base(element) { }
        public RadioButton(ISearchContext searchContext, By by) : base(searchContext, by) { }

        public bool Selected
        {
            get
            {
                return Element.Selected;
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

    /// <summary>
    /// Represents a Checkbox on a webpage
    /// </summary>
    public class CheckBox : WebFormObject
    {
        public CheckBox(IWebElement element) : base(element) { }
        public CheckBox(IWebElement parentElement, By by) : base(parentElement, by) { }
        public CheckBox(IWebDriver driver, By by) : base(driver, by) { }

        public bool Selected
        {
            get
            {
                return Element.Selected;
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

    public class Button : WebFormObject
    {
        public Button(IWebElement element) : base(element) { }
        public Button(ISearchContext searchContext, By by) : base(searchContext, by) { }

        public void Click()
        {
            Element.Click();
        }

        public string Text
        {
            get
            {
                return Element.Text;
            }
        }
    }

    public class TextArea : WebFormObject
    {
        public TextArea(IWebElement element) : base(element) { }
        public TextArea(IWebElement parentElement, By by) : base(parentElement, by) { }
        public TextArea(IWebDriver driver, By by) : base(driver, by) { }

        public string Text
        {
            get
            {
                return Element != null ? Element.Text : null;
            }
        }
    }

    public class Link : WebFormObject
    {
        public Link(IWebElement element) : base(element) { }
        public Link(IWebElement parentElement, By by) : base(parentElement, by) { }
        public Link(IWebDriver driver, By by) : base(driver, by) { }
    }

    public class DropDownList : WebFormObject
    {
        private SelectElement _selectElement;

        public DropDownList(IWebElement element) : base(element)
        {
            AssignSelectElement();
        }
        public DropDownList(IWebElement parentElement, By by) : base(parentElement, by)
        {
            AssignSelectElement();
        }
        public DropDownList(IWebDriver driver, By by) : base(driver, by)
        {
            AssignSelectElement();
        }

        private void AssignSelectElement()
        {
            if (_element != null)
            {
                _selectElement = new SelectElement(_element);
            }
        }

        public void SelectByText(string selection)
        {
            _selectElement.SelectByText(selection);
        }

        public void SelectByValue(string value)
        {
            _selectElement.SelectByValue(value);
        }
    }
}
