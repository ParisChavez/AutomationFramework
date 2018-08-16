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
    /// Used to represent animated images.  Provides waiting functionality.
    /// </summary>
    public class LoadingSpinner : WebElementObject
    {
        private By _by;
        private ISearchContext _searchContext;

        /// <summary>
        /// Constructor for loading spinner object
        /// </summary>
        /// <param name="driver">Webdriver, needed for internal waiting methods</param>
        /// <param name="by">Locator to the visual loading object</param>
        public LoadingSpinner(ISearchContext searchContext, By by, [CallerMemberName] string creatorName = "") : base(searchContext, by, creatorName)
        {
            _searchContext = searchContext;
            _by = by;
        }

        /// <summary>
        /// Waits until all ajax calls finish, or until the process times out
        /// </summary>
        //public void WaitUntilAjaxFinishes(TimeSpan timeout)
        //{
        //    _driver.WaitForAjaxComplete(timeout);
        //}

        /// <summary>
        /// Waits until all ajax calls finish and the LoadingSpinner object vanishes from the page
        /// </summary>
        //public void WaitUntilLoadingFinishes(TimeSpan timeout)
        //{
        //    WaitUntilAjaxFinishes(timeout);
        //    WaitUntilVanish(timeout);
        //}

        /// <summary>
        /// Checks if the object is displayed on the page
        /// </summary>
        //public bool Displayed
        //{
        //    get
        //    {
        //        try
        //        {
        //            var elementToBeDisplayed = _driver.FindElementNull(_by);
        //            return elementToBeDisplayed == null ? false : elementToBeDisplayed.Displayed;
        //        }
        //        catch (StaleElementReferenceException)
        //        {
        //            return false;
        //        }
        //    }
        //}
    }
}
