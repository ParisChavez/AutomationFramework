using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFoundation;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace UiTestFoundation
{

    public enum Browser { Chrome, Firefox, IE }
    public enum Device { Desktop, Tablet, Phone }

    // all test case Fixtures will derive from this
    // has basic actions needed for UI testing
    // will contain webdriver
    public class UiTestFixture : TestFixture
    {

        public IWebDriver Driver { private set; get; }
        public UiConfiguration UiSettings { get; private set; }

        public T CreateWebPageModel<T>() where T : PageModel, new()
        {
            T pageModel = new T();
            pageModel.SetParentTestFixture(this);
            return pageModel;
        }

        public UiTestFixture() : base()
        {
            UiSettings = new UiConfiguration(this);
        }

        [OneTimeSetUp]
        public void UiTestFixtureSetup()
        {
            if (UiSettings.Browser == Browser.Chrome)
            {
                Driver = new ChromeDriver();
            }
            else if (UiSettings.Browser == Browser.Firefox)
            {
                Driver = new FirefoxDriver();
            }
            else
            {
                Driver = new InternetExplorerDriver();
            }

            Driver.Manage().Window.Maximize();
        }

        [SetUp]
        public void UiTestSetup()
        {
            int i = 10;
        }

        [TearDown]
        public void UiTestTearDown()
        {
            int i = 10;
        }

        [OneTimeTearDown]
        public void UiTestFixtureTearDown()
        {
            int i = 10;
            Driver.Quit();
        }
    }
}
