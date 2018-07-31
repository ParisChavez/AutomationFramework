using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TestFoundation;

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

        private void RequeryElement()
        {
            if (!_staticObject)
            {
                if (_element == null || _element.IsStale())
                {
                    _element = _searchContext.FindElementNull(_by);
                }
            }
        }

        /// <summary>
        /// Get the element for the object.  Will throw an exception if null, use WebFormObject.Exists to check beforehand;
        /// </summary>
        protected IWebElement Element
        {
            get
            {
                RequeryElement();
                return _element ?? throw new NoSuchElementException(NotFoundExceptionMessage());
            }
        }

        /// <summary>
        /// Builds the exception message when element is not found.
        /// </summary>
        /// <returns></returns>
        private string NotFoundExceptionMessage()
        {
            StringBuilder messageBuilder = new StringBuilder();
            messageBuilder.AppendLine("");
            messageBuilder.AppendLine($"Expected {this.GetType().Name} object does not exist on the page!");
            if (_by != null)
            {
                messageBuilder.AppendLine($"Locator: \"{_by}\" found no elements.");
            }

            return messageBuilder.ToString();
        }

        /// <summary>
        /// Create a static version of a webformObject that will no support waits.
        /// </summary>
        /// <param name="searchContext"></param>
        /// <param name="by"></param>
        public WebFormObject(IWebElement element)
        {
            _element = element ?? throw new ArgumentException($"element cannot be null when creating a constant {this.GetType().Name}!");
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
            _searchContext = searchContext ?? throw new ArgumentException($"searchContext cannot be null when creating a {this.GetType().Name}!");
            _staticObject = false;
        }

        /// <summary>
        /// Checks if the object is displayed on the page
        /// </summary>
        public bool Displayed
        {
            get
            {
                RequeryElement();
                return _element != null && Element.Displayed;
            }
        }

        /// <summary>
        /// Checks if the object is enabled on the page
        /// </summary>
        public bool Enabled
        {
            get
            {
                RequeryElement();
                return _element != null && Element.Enabled;
            }
        }

        /// <summary>
        /// Checks if the object exists on the page
        /// </summary>
        public bool Exists
        {
            get
            {
                RequeryElement();
                return _element != null;
            }
        }

        /// <summary>
        /// Waits for the object to appear on the page
        /// Requires that object be created with locator and search context in constructor
        /// </summary>
        public void WaitForVisible(int timeout = 15)
        {
            if (!_staticObject)
            {
                try
                {
                    _searchContext.WaitForElementVisible(_by, TimeSpan.FromSeconds(timeout));
                }
                catch (WebDriverTimeoutException ex)
                {
                    throw new WebDriverTimeoutException(TimeoutExceptionMessage(ex.Message, true));
                }
            }
        }

        private string TimeoutExceptionMessage(string message, bool waitingForVisible)
        {
            StringBuilder messageBuilder = new StringBuilder();
            messageBuilder.AppendLine("");
            messageBuilder.AppendLine(message);
            messageBuilder.AppendLine($"{this.GetType().Name} is still not {(waitingForVisible ? "visible" : "invisible")} after wait time.");
            if (!_staticObject)
            {
                messageBuilder.AppendLine($"Using locator: : \"{_by}\"");
            }
            return messageBuilder.ToString();

        }

        /// <summary>
        /// Waits for the object to vanish from the page
        /// Requires that object be created with locator and search context in constructor
        /// </summary>
        public void WaitForInvisible(TimeSpan timeout)
        {
            if (!_staticObject)
            {
                try
                {
                    _searchContext.WaitForElementInvisible(_by, timeout);
                }
                catch (WebDriverTimeoutException ex)
                {
                    throw new WebDriverTimeoutException(TimeoutExceptionMessage(ex.Message, false));
                }
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
                return Element.Text;
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
    /// Drop down menu objects
    /// </summary>
    public class DropDownList : WebFormObject
    {
        /// <summary>
        /// Do not use directly, use Property SelectElement instead.
        /// </summary>
        private SelectElement _selectElement = null;
        public DropDownList(IWebElement element) : base(element)
        {
        }

        public DropDownList(ISearchContext searchContext, By by) : base(searchContext, by)
        {

        }

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
        /// Selects an option by index
        /// </summary>
        /// <param name="value">Index of option to select</param>
        public void SelectByValue(string value)
        {
            SelectElement.SelectByValue(value);
        }
    }

    /// <summary>
    /// used to represent chunks of html that need to be grouped together, such as: popups, divs, search results, records, iframes, etc.
    /// </summary>
    public abstract class HtmlBlob : WebFormObject
    {
        /// <summary>
        /// do not use the Driver contained in here for searches, use Element
        /// </summary>
        protected UiTestFixture TestFixture { get; set; }

        public HtmlBlob(IWebElement enclosingElement, UiTestFixture testFixture) : base(enclosingElement)
        {
            TestFixture = testFixture ?? throw new ArgumentException("TestFixture is null.");
        }

        public HtmlBlob(ISearchContext searchContext, By by, UiTestFixture testFixture) : base(searchContext, by)
        {
            TestFixture = testFixture ?? throw new ArgumentException("TestFixture is null.");
        }

        /// <summary>
        /// UiSettings for the current test run.
        /// Use to check for browser, device, etc, which may affect element querying.
        /// </summary>
        protected UiConfiguration UIConfig
        {
            get
            {
                return TestFixture.UiSettings;
            }
        }

        /// <summary>
        /// Log steps, exceptions, decisions
        /// </summary>
        protected Logger Log
        {
            get
            {
                return TestFixture.Log;
            }
        }
    }
}