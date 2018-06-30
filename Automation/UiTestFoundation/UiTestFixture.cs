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
using OpenQA.Selenium.Remote;
using System.Reflection;
using System.IO;

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

        private void InitializeWebDriver()
        {
            if (UiSettings.Browser == Browser.Chrome)
            {
                ChromeOptions options = new ChromeOptions();
                options.AddArgument("--disable-extensions");
                options.AddArgument("disable-infobars");
                options.AddArgument("--no-sandbox");

                if (UiSettings.Device != Device.Desktop)
                {
                    options.EnableMobileEmulation(UiSettings.GetDeviceEmulationString(UiSettings.Device));
                }
                Driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Drivers", options, TimeSpan.FromSeconds(120));
            }
            else if (UiSettings.Browser == Browser.Firefox)
            {
                Driver = new FirefoxDriver();
            }
            else
            {
                Driver = new InternetExplorerDriver();
            }
        }

        [OneTimeSetUp]
        public void UiTestFixtureSetup()
        {
            InitializeWebDriver();

            //Driver.Manage().Cookies.DeleteAllCookies();
            Driver.Manage().Window.Maximize();
        }

        [SetUp]
        public void UiTestSetup()
        {
            Log.Debug("Ui Setup Method");
        }

        [TearDown]
        public void UiTestTearDown()
        {
            //Driver.Close();
            Driver.Manage().Cookies.DeleteAllCookies();
        }

        [OneTimeTearDown]
        public void UiTestFixtureTearDown()
        {
            Driver.Quit();
        }
    }
}
