using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiTestFoundation
{
    /// <summary>
    /// Class containing all UI test configuration
    /// will query TestFixtures config, app configs or database as needed
    /// </summary>
    public class UiConfiguration
    {
        private UiTestFixture _uiTestFixture;

        internal UiConfiguration(UiTestFixture testFixture) => _uiTestFixture = testFixture;

        public Browser Browser
        {
            get
            {
                switch (_uiTestFixture.Config.GetSetting("Browser").ToLower())
                {
                    case "internetexplorer":
                    case "ie":
                        return UiTestFoundation.Browser.IE;

                    case "firefox":
                        return UiTestFoundation.Browser.Firefox;

                    case "chrome":
                    default:
                        return UiTestFoundation.Browser.Chrome;
                }
            }
        }

        public Device Device
        {
            get
            {
                switch (_uiTestFixture.Config.GetSetting("Device").ToLower())
                {
                    case "tablet":
                        return UiTestFoundation.Device.Tablet;

                    case "phone":
                        return UiTestFoundation.Device.Phone;

                    case "desktop":
                    default:
                        return UiTestFoundation.Device.Desktop;
                }
            }
        }

        public string GetDeviceEmulationString(Device device)
        {
            switch (device)
            {
                case Device.Tablet:
                    return "iPad";
                case Device.Phone:
                    return "iPhone 6";
                case Device.Desktop:
                default:
                    return "none";
            }
        }
    }
}
