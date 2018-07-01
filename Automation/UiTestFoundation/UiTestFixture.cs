﻿using System;
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
using NUnit.Framework.Interfaces;

namespace UiTestFoundation
{
    // all test case Fixtures will derive from this
    // has basic actions needed for UI testing
    // will contain webdriver
    public class UiTestFixture : TestFixture
    {
        public IWebDriver Driver { private set; get; }
        public UiConfiguration UiSettings { get; private set; }

        public UiTestFixture() : base()
        {
            UiSettings = new UiConfiguration(this);
        }

        private void InitializeWebDriver()
        {
            TimeSpan commandTimeout = TimeSpan.FromSeconds(UiSettings.CommandTimeout);
            string driverDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Drivers"; // configurable?

            if (UiSettings.Browser == TestBrowser.Chrome)
            {
                ChromeOptions options = new ChromeOptions();
                options.AddArgument("--disable-extensions");
                options.AddArgument("disable-infobars");
                options.AddArgument("--no-sandbox");

                if (UiSettings.Device != TestDevice.Desktop)
                {
                    options.EnableMobileEmulation(UiSettings.GetDeviceEmulationString(UiSettings.Device));
                }
                Driver = new ChromeDriver(driverDir, options, commandTimeout);
            }
            else if (UiSettings.Browser == TestBrowser.Firefox)
            {
                FirefoxOptions options = new FirefoxOptions();
                Driver = new FirefoxDriver(driverDir, options, commandTimeout);
            }
            else
            {
                InternetExplorerOptions options = new InternetExplorerOptions();
                Driver = new InternetExplorerDriver(driverDir, options, commandTimeout);
            }
        }

        public void SaveScreenshot(string saveLocation)
        {
            string testName = TestContext.CurrentContext.Test.MethodName;

            try
            {
                Screenshot ss = ((ITakesScreenshot)Driver).GetScreenshot();
                ss.SaveAsFile(saveLocation + $"{testName}.png", ScreenshotImageFormat.Png);
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                throw;
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

            bool takeScreenshot = false;
            if (UiSettings.SaveScreenshotSetting == TakeScreenshot.Always)
            {
                takeScreenshot = true;
            }


            if (UiSettings.SaveScreenshotSetting == TakeScreenshot.OnFail && TestContext.CurrentContext.Result.Outcome == ResultState.Failure)
            {
                takeScreenshot = true;
            }

            if (takeScreenshot)
            {
                SaveScreenshot(UiSettings.ScreenShotPath);
            }

            Driver.Manage().Cookies.DeleteAllCookies();
            Driver.Close();
        }

        [OneTimeTearDown]
        public void UiTestFixtureTearDown()
        {
            Driver.Quit();
        }
    }
}
