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
        protected IWebElement Element { get; set; }

        public WebFormObject(IWebElement element)
        {
            Element = element ?? throw new NoSuchElementException("Object does not exist");
        }

        public WebFormObject(ISearchContext searchContext, By by)
        {
            Element = searchContext.FindElementNull(by) ?? throw new NoSuchElementException("Object does not exist");
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
        /// <summary>
        /// Ctr for Textbox
        /// </summary>
        /// <param name="element">will throw NoSuchElementException if element is null</param>
        public TextBox(IWebElement element) : base(element) { }

        /// <summary>
        /// ctr for textbox
        /// </summary>
        /// <param name="searchContext">the search context to find the element in</param>
        /// <param name="by">the search parameters.  If no element found NoSuchElementException will be thrown</param>
        public TextBox(ISearchContext searchContext, By by) : base(searchContext, by) { }
        
        /// <summary>
        /// Creates and returns a TextBox instance.  Will return null if element is null;
        /// </summary>
        /// <param name="element">element representing the TextBox</param>
        /// <returns>TextBox or null</returns>
        public static TextBox Create(IWebElement element)
        {
            if (element != null)
                return new TextBox(element);
            else
                return null;
        }

        /// <summary>
        /// Creates and returns a TextBox instance.  Will return null if elements resolves to null;
        /// </summary>
        /// <param name="element">element representing the TextBox</param>
        /// <returns>TextBox or null</returns>
        public static TextBox Create(ISearchContext searchContext, By by)
        {
            IWebElement element = searchContext.FindElementNull(by);
            return Create(element);
        }

        public string Text
        {
            get
            {
                return Element.GetAttribute("value");
            }
            set
            {
                SetText(value);
            }
        }

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
    /// Represents a Radio Button on a webpage
    /// </summary>
    public class RadioButton : WebFormObject
    {
        public RadioButton(IWebElement element) : base(element) { }
        public RadioButton(ISearchContext searchContext, By by) : base(searchContext, by) { }

        public static RadioButton Create(IWebElement element)
        {
            if (element != null)
                return new RadioButton(element);
            else
                return null;
        }

        public static RadioButton Create(ISearchContext searchContext, By by)
        {
            IWebElement element = searchContext.FindElementNull(by);
            return Create(element);
        }

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
        public CheckBox(ISearchContext searchContext, By by) : base(searchContext, by) { }

        public static CheckBox Create(IWebElement element)
        {
            if (element != null)
                return new CheckBox(element);
            else
                return null;
        }

        public static CheckBox Create(ISearchContext searchContext, By by)
        {
            IWebElement element = searchContext.FindElementNull(by);
            return Create(element);
        }

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

        public static Button Create(IWebElement element)
        {
            if (element != null)
                return new Button(element);
            else
                return null;
        }

        public static Button Create(ISearchContext searchContext, By by)
        {
            IWebElement element = searchContext.FindElementNull(by);
            return Create(element);
        }

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
        public TextArea(ISearchContext searchContext, By by) : base(searchContext, by) { }

        public static TextArea Create(IWebElement element)
        {
            if (element != null)
                return new TextArea(element);
            else
                return null;
        }

        public static TextArea Create(ISearchContext searchContext, By by)
        {
            IWebElement element = searchContext.FindElementNull(by);
            return Create(element);
        }

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
        public Link(ISearchContext searchContext, By by) : base(searchContext, by) { }

        public static Link Create(IWebElement element)
        {
            if (element != null)
                return new Link(element);
            else
                return null;
        }

        public static Link Create(ISearchContext searchContext, By by)
        {
            IWebElement element = searchContext.FindElementNull(by);
            return Create(element);
        }

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

    public class DropDownList : WebFormObject
    {
        private SelectElement _selectElement;

        public DropDownList(IWebElement element) : base(element)
        {
            AssignSelectElement();
        }

        public DropDownList(ISearchContext searchContext, By by) : base(searchContext, by)
        {
            AssignSelectElement();
        }

        public static DropDownList Create(IWebElement element)
        {
            if (element != null)
                return new DropDownList(element);
            else
                return null;
        }

        public static DropDownList Create(ISearchContext searchContext, By by)
        {
            IWebElement element = searchContext.FindElementNull(by);
            return Create(element);
        }

        private void AssignSelectElement()
        {
            _selectElement = new SelectElement(Element);
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