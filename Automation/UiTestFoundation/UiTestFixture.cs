using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFoundation;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace UiTestFoundation
{
    // all test case Fixtures will derive from this
    // has basic actions needed for UI testing
    // will contain webdriver

    public class UiTestFixture : TestFixture
    {
        public IWebDriver Driver { private set; get; }

        public T CreateWebPageModel<T>() where T : PageModel, new()
        {
            T pageModel = new T();
            pageModel.SetParentTestFixture(this);
            return pageModel;
        }

        public UiTestFixture() : base()
        {

        }

        [OneTimeSetUp]
        public void UiTestFixtureSetup()
        {
            Driver = new ChromeDriver();
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
