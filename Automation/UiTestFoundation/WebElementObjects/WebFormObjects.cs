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
    public abstract class WebElementObject
    {
        private IWebElement _element;
        private ISearchContext _searchContext { get; set; }
        private By _by { get; set; }
        private bool _staticObject { get; set; }
        protected string _creatingMethodName { get; set; }

        /// <summary>
        /// If the object is set up to be dynamic, search for the element if it is null, or if it has become stale
        /// </summary>
        private void QueryForElement()
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
        /// For use in statically created objects, checks if the base element does not exist or has gone stale.
        /// use to avoid null reference exceptions
        /// </summary>
        /// <returns></returns>
        public bool IsRequeryNeeded()
        {
            if (_staticObject)
            {
                if (_element == null || _element.IsStale())
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Get the element for the object.  Will throw an exception if null, use WebFormObject.Exists to check beforehand
        /// </summary>
        protected IWebElement Element
        {
            get
            {
                QueryForElement();
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
            if (string.IsNullOrEmpty(_creatingMethodName))
            {
                messageBuilder.AppendLine($"Expected {this.GetType().Name} object does not exist on the page!");
            }
            else
            {
                messageBuilder.AppendLine($"Expected {_creatingMethodName} as {this.GetType().Name} does not exist on the page!");
            }

            if (_by != null)
            {
                messageBuilder.AppendLine($"Locator: \"{_by}\" found no elements.");
            }

            return messageBuilder.ToString();
        }

        /// <summary>
        /// Create a static version of a webformObject that will not support waits or automatic requeries.
        ///  Use IsRequeryNeeded() in the page model's getters to check if it should be recreated.
        /// </summary>
        /// <param name="searchContext"></param>
        /// <param name="by"></param>
        /// <param name="creatorName">
        /// Optional, name of method or parameter that creates the object
        /// Typically the property name on the page or blob creating.
        /// Used in error messaging
        /// </param>
        public WebElementObject(IWebElement element, string creatorName = "")
        {
            _element = element;
            _staticObject = true;
            _creatingMethodName = creatorName;
        }

        /// <summary>
        /// Create a dynamic version of a webformObject that will support waits, and update if stale.  
        /// </summary>
        /// <param name="searchContext"></param>
        /// <param name="by"></param>
        /// <param name="creatorName">
        /// Optional, name of method or parameter that creates the object
        /// Typically the property name on the page or blob creating.
        /// Used in error messaging
        /// </param>
        public WebElementObject(ISearchContext searchContext, By by, string creatorName = "")
        {
            _by = by;
            _searchContext = searchContext ?? throw new ArgumentException($"searchContext cannot be null when creating a {this.GetType().Name}!");
            _staticObject = false;
            _creatingMethodName = creatorName;
        }

        /// <summary>
        /// Checks if the object is displayed on the page
        /// </summary>
        public bool Displayed
        {
            get
            {
                QueryForElement();
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
                QueryForElement();
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
                QueryForElement();
                return _element != null;
            }
        }

        /// <summary>
        /// Is the root element currently stale?  When created with element instead of a By, this determines if it should be recreated.
        /// </summary>
        public bool IsElementStale
        {
            get
            {
                return _element.IsStale();
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

        /// <summary>
        /// Builds the exception message when the wait methods fail.
        /// </summary>
        /// <returns></returns>
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
    }
}