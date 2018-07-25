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
    /// class for specific web form objects
    /// </summary>
    public abstract class WebFormObject
    {
        protected IWebElement Element { get; set; }

        public WebFormObject(IWebElement element)
        {
            Element = element ?? throw new NoSuchElementException("IWebElement is null, unable to create WebFormObject!");
        }

        public WebFormObject(ISearchContext searchContext, By by)
        {
            Element = searchContext.FindElementNull(by) ?? throw new NoSuchElementException("No IWebElement found, unable to createWebFormObject!");
        }

        /// <summary>
        /// Checks if the object is displayed on the page
        /// </summary>
        public bool Displayed
        {
            get { return Element != null && Element.Displayed; }
        }

        /// <summary>
        /// Checks if the object is enabled on the page
        /// </summary>
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

        public static TextBox Create(IWebElement element)
        {
            if (element != null)
                return new TextBox(element);
            else
                return null;
        }

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
                Element.SendKeys(value);
            }
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


    /// <summary>
    /// Represents a Button on a webpage
    /// Use WaitOnClick after create to set up simple waiting logic on clicks.
    /// </summary>
    public class Button : WebFormObject
    {
        private bool _waitOnClickEnabled = false;
        private By _waitOnClickLocator = null;
        private IWebDriver _driver = null;
        private TimeSpan _waitTime;

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

        /// <summary>
        /// if set up, button will wait for the element located at by after click to be visible
        /// </summary>
        /// <param name="driver">current webdriver instance</param>
        /// <param name="by">By Locator to element to wait for</param>
        public void WaitOnClick(IWebDriver driver, By by, TimeSpan timespan)
        {
            _waitOnClickEnabled = true;
            _driver = driver;
            _waitOnClickLocator = by;
            _waitTime = timespan;
        }

        /// <summary>
        /// Perform a click.  If WaitOnElement is enabled, will wait for set up element to be visible.
        /// If more complex waiting is required, think about wrapping it up in the page model, or inheriting and overriding the click method.
        /// </summary>
        public void Click()
        {
            Element.Click();

            if (_waitOnClickEnabled)
            {
                _driver.WaitForElementVisible(_waitOnClickLocator, _waitTime);
            }
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

    /// <summary>
    /// Used to represent animated loading images.  Provides waiting functionality.
    /// </summary>
    public class LoadingSpinner
    {
        private By _by;
        private IWebDriver _driver;

        /// <summary>
        /// Constructor for loading spinner object
        /// </summary>
        /// <param name="driver">Webdriver, needed for internal waiting methods</param>
        /// <param name="by">Locator to the visual loading object</param>
        public LoadingSpinner(IWebDriver driver, By by)
        {
            _driver = driver;
            _by = by;
        }

        /// <summary>
        /// Waits until the LoadingSpinner object appears on the page
        /// </summary>
        public void WaitUntilAppear(TimeSpan timeout)
        {
            _driver.WaitForElementVisible(_by, timeout);
        }

        /// <summary>
        /// Waits until the LoadingSpinner object vanishes from the page
        /// </summary>
        public void WaitUntilVanish(TimeSpan timeout)
        {
            _driver.WaitForElementInvisible(_by, timeout);
        }

        /// <summary>
        /// Waits until all ajax calls finish, or until the process times out
        /// </summary>
        public void WaitUntilAjaxFinishes(TimeSpan timeout)
        {
            _driver.WaitForAjaxComplete(timeout);
        }

        /// <summary>
        /// Waits until all ajax calls finish and the LoadingSpinner object vanishes from the page
        /// </summary>
        public void WaitUntilLoadingFinishes(TimeSpan timeout)
        {
            WaitUntilAjaxFinishes(timeout);
            WaitUntilVanish(timeout);
        }

        /// <summary>
        /// Checks if the object is displayed on the page
        /// </summary>
        public bool Displayed
        {
            get
            {
                try
                {
                    var elementToBeDisplayed = _driver.FindElementNull(_by);
                    return elementToBeDisplayed == null ? false : elementToBeDisplayed.Displayed;
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
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