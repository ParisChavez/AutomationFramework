using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TestFoundation
{
    public class Configuration
    {
        private Dictionary<string, string> _testSettings = new Dictionary<string, string>();

        public void AddSetting(string setting, string value)
        {
            _testSettings.Add(setting.ToLower(), value);
        }

        public string GetSetting(string setting)
        {
            return _testSettings[setting];
        }
    }

    public class TestFixture
    {
        public Configuration Config { get; protected set; }

        public TestFixture()
        {
            Config = new Configuration();
        }

        [OneTimeSetUp]
        public void TestFixtureSetup()
        {
            int i = 10;
        }

        [SetUp]
        public void TestSetup()
        {
            int i = 10;
        }

        [TearDown]
        public void TestTearDown()
        {
            int i = 10;
        }

        [OneTimeTearDown]
        public void TestFixtureTeardown()
        {
            int i = 10;
        }
    }
}
