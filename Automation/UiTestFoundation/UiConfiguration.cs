using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiTestFoundation
{
    public enum TestBrowser { Chrome, Firefox, IE }
    public enum TestDevice { Desktop, Tablet, Phone }
    public enum TakeScreenshot { Never, OnFail, Always }

    /// <summary>
    /// Class containing all UI test configuration
    /// will query TestFixtures config, app configs or database as needed
    /// </summary>
    public class UiConfiguration
    {
        private UiTestFixture _uiTestFixture;

        internal UiConfiguration(UiTestFixture testFixture) => _uiTestFixture = testFixture;

        public TestBrowser Browser
        {
            get
            {
                switch (_uiTestFixture.Config.GetSetting("Browser"))
                {
                    case "internetexplorer":
                    case "ie":
                        return UiTestFoundation.TestBrowser.IE;

                    case "firefox":
                        return UiTestFoundation.TestBrowser.Firefox;

                    case "chrome":
                    default:
                        return UiTestFoundation.TestBrowser.Chrome;
                }
            }
        }

        public TestDevice Device
        {
            get
            {
                switch (_uiTestFixture.Config.GetSetting("Device").ToLower())
                {
                    case "tablet":
                        return UiTestFoundation.TestDevice.Tablet;

                    case "phone":
                        return UiTestFoundation.TestDevice.Phone;

                    case "desktop":
                    default:
                        return UiTestFoundation.TestDevice.Desktop;
                }
            }
        }

        public string GetDeviceEmulationString(TestDevice device)
        {
            switch (device)
            {
                case TestDevice.Tablet:
                    return "iPad";
                case TestDevice.Phone:
                    return "iPhone 6";
                case TestDevice.Desktop:
                default:
                    return "none";
            }
        }

        public int CommandTimeout
        {
            get
            {
                string timeoutStr = _uiTestFixture.Config.GetSetting("commandTimeout");
                if (!string.IsNullOrEmpty(timeoutStr))
                {
                    int timeout;
                    if (int.TryParse(timeoutStr, out timeout))
                    {
                        return timeout;
                    }
                }

                return 120;
            }
        }

        public string ScreenShotPath
        {
            get
            {
                return _uiTestFixture.Config.GetSetting("screenshotPath");
            }
        }

        public TakeScreenshot SaveScreenshotSetting
        {
            get
            {
                switch (_uiTestFixture.Config.GetSetting("Screenshot").ToLower())
                {
                    case "onfail":
                        return TakeScreenshot.OnFail;

                    case "Always":
                        return TakeScreenshot.Always;

                    case "never":
                    default:
                        return TakeScreenshot.Never;
                }
            }
        }
    }
}
