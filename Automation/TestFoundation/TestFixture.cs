using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Configuration;

namespace TestFoundation
{
    public class TestConfiguration
    {
        private Dictionary<string, string> _testSettings = new Dictionary<string, string>();

        public TestConfiguration()
        {
        }

        public void AddNunitParams()
        {
            var nunitParams = TestContext.Parameters.Names.ToList();
            foreach (string parameterName in nunitParams)
            {
                AddSetting(parameterName.ToLower(), TestContext.Parameters[parameterName]);
            }
        }

        private void AddSetting(string setting, string value)
        {
            _testSettings.Add(setting.ToLower(), value);
        }

        public string GetSetting(string setting)
        {
            // check if setting exists in _testSettings. 
            // If not, return in app.config.
            // If not there, return null;
            if (_testSettings.ContainsKey(setting))
            {
                return _testSettings[setting.ToLower()].ToLower();
            }

            // return null if key does not exist
            return ConfigurationManager.AppSettings[setting];
        }
    }

    public class TestFixture
    {
        public TestConfiguration Config { get; protected set; }
        public Logger Log;

        public TestFixture()
        {
            Config = new TestConfiguration();
            Log = new Logger();
        }

        [OneTimeSetUp]
        public void TestFixtureSetup()
        {
            Log.Debug("TestFixture Onetime Setup");
        }

        [SetUp]
        public void TestSetup()
        {
            Log.Debug("TestFixture test setup");
        }

        [TearDown]
        public void TestTearDown()
        {
            Log.Debug("TestFixture test teardown");
        }

        [OneTimeTearDown]
        public void TestFixtureTeardown()
        {
            Log.Debug("TestFixture One Time teardown");
        }
    }
}
