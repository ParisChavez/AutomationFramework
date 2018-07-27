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
        private IWebElement _element;
        private ISearchContext _searchContext { get; set; }
        private By _by { get; set; }
        private bool _staticObject;

        /// <summary>
        /// Get the element for the object.  Will throw an exception if null, use WebFormObject.Exists to check beforehand;
        /// </summary>
        protected IWebElement Element
        {
            get
            {
                if (_element == null || _element.IsStale())
                {
                    _element = _searchContext.FindElementNull(_by);
                }

                return _element ?? throw new InvalidOperationException($"Expected {this.GetType().Name} object does not exist on the page!");
            }
        }

        /// <summary>
        /// Create a static version of a webformObject that will support waits.  Will throw exception if a wait is attempted
        /// </summary>
        /// <param name="searchContext"></param>
        /// <param name="by"></param>
        public WebFormObject(IWebElement element)
        {
            _element = element ?? throw new ArgumentException("element cannot be null when creating a constant webformObject!");
            _staticObject = true;
        }

        /// <summary>
        /// Create a dynamic version of a webformObject that will support waits, and update on the fly
        /// </summary>
        /// <param name="searchContext"></param>
        /// <param name="by"></param>
        public WebFormObject(ISearchContext searchContext, By by)
        {
            _by = by;
            _searchContext = searchContext ?? throw new ArgumentException("searchContext cannot be null when creating a webformObject!");
            _staticObject = false;
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

        public bool Exists
        {
            get
            {
                return Element != null;
            }
        }

        public void WaitForVisible(int timeout = 15)
        {
            if (!_staticObject)
            {
                _searchContext.WaitForElementVisible(_by, TimeSpan.FromSeconds(15));
            }
        }

        public void WaitForInvisible(TimeSpan timeout)
        {
            if (!_staticObject)
            {
                _searchContext.WaitForElementInvisible(_by, timeout);
            }
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
    /// Used to represent animated images.  Provides waiting functionality.
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
            WebDriverWait elementInvisibleWait = new WebDriverWait(_driver, timeout);
            elementInvisibleWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(_by));
        }

        /// <summary>
        /// Waits until the LoadingSpinner object vanishes from the page
        /// </summary>
        public void WaitUntilVanish(TimeSpan timeout)
        {
            WebDriverWait elementInvisibleWait = new WebDriverWait(_driver, timeout);
            elementInvisibleWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(_by));
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

    /// <summary>
    /// TODO, figure this out
    /// </summary>
    public class DropDownList : WebFormObject
    {
        /// <summary>
        /// Do not use directly, use Property SelectElement instead.
        /// </summary>
        private SelectElement _selectElement = null;

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

        public DropDownList(IWebElement element) : base(element)
        {
        }

        public DropDownList(ISearchContext searchContext, By by) : base(searchContext, by)
        {

        }

        public void SelectByText(string selection)
        {
            SelectElement.SelectByText(selection);
        }

        public void SelectByValue(string value)
        {
            SelectElement.SelectByValue(value);
        }
    }
}