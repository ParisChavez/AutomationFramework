using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFoundation;
using UiTestFoundation;
using NUnit.Framework;
using GooglePageModels;

namespace ExampleTests
{
    [TestFixture]
    public class GoogleUITests : UiTestFixture
    {
        public GoogleUITests() : base()
        {
            Config.AddSetting("Browse", "Chrome");
            Config.AddSetting("device", "desktop");
        }

        /// <summary>
        /// Will be run once before all tests in this class
        /// </summary>
        [OneTimeSetUp]
        public void GoogleUITestFixtureSetup()
        {

        }

        /// <summary>
        /// Will be run before each tests in this class
        /// </summary>
        [SetUp]
        public void GoogleUITestSetup()
        { }

        [Test]
        public void GoogleTitleContainsSearchTerm()
        {
            GoogleHomepage homepage = CreateWebPageModel<GoogleHomepage>();
            homepage.GoToUrl("http://www.google.com");
            GoogleResultsPage resultsPage = homepage.EnterTextAndSearch("HP Lovecraft");

            Assert.IsTrue(resultsPage.Title.Contains("HP Lovecraft"));
        }

        /// <summary>
        /// Will be run before each tests in this class
        /// </summary>
        [TearDown]
        public void GoogleUITestTearDown()
        { }

        /// <summary>
        /// Will be run once before all tests in this class
        /// </summary>
        [OneTimeTearDown]
        public void GoogleUITestFixtureTearDown()
        { }
    }
}
