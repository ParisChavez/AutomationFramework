using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace UiTestFoundation
{
    public static class CustomAsserts
    {
        //        public static void WaitForAjaxComplete(this IWebDriver driver, TimeSpan timeout)

        public static void Exists(this Assert assert, WebFormObject webObject, string message)
        {
            if (webObject.Exists == false)
                throw new AssertionException(message);
        }

        public static void DoesNotExist(this Assert assert, WebFormObject webObject, string message)
        {
            if (webObject.Exists == true)
                throw new AssertionException(message);
        }

        public static void IsDisplayed(this Assert assert, WebFormObject webObject, string message)
        {
            if (webObject.Displayed == false)
                throw new AssertionException(message);
        }

        public static void IsNotDisplayed(this Assert assert, WebFormObject webObject, string message)
        {

        }

        public static void IsAtPage(this Assert assert, PageModel page, string message)
        {

        }
    }
}
