using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using UiTestFoundation;

namespace GooglePageModels
{
    public class GoogleHomepage : PageModel
    {
        public GoogleHomepage(UiTestFixture testFixture) : base(testFixture) { }

        private TextBox _searchBox;
        private Button _searchButton;
        public TextBox SearchBox
        {
            get
            {
                if (_searchBox == null)
                {
                    if (TestFixture.UiSettings.Device == TestDevice.Phone)
                    {
                        _searchBox = new TextBox(Driver, By.ClassName("gLFyf"));
                    }
                    else
                    {
                        _searchBox = new TextBox(Driver, By.Id("lst-ib"));
                    }
                }

                return _searchBox;
            }
        }

        public Button SearchButton
        {
            get
            {
                if (_searchButton == null)
                {
                    _searchButton = new Button(Driver, By.Name("btnK"));
                }

                return _searchButton;
            }
        }

        public override void Go()
        {
            Navigate.GoToUrl("http://www.google.com");
        }

        public override bool IsAt()
        {
            return Driver.Title.Equals("Google");
        }

        /// <summary>
        /// Enters text in the search box and clicks search
        /// </summary>
        /// <param name="searchText">Text to search</param>
        /// <returns>a page model of the google results page</returns>
        public GoogleResultsPage EnterTextAndSearch(string searchText)
        {
            SearchBox.Text = searchText;
            SearchBox.PressEnter();
            return new GoogleResultsPage(TestFixture);
        }
    }

    public class GoogleResultsPage : PageModel
    {
        public GoogleResultsPage(UiTestFixture testFixture) : base(testFixture) { }

        public override void Go()
        {
            Navigate.GoToUrl("http://www.google.com");
        }

        public override bool IsAt()
        {
            return true;
        }
    }

}
