using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace TestFoundation
{
    public class Logger
    {
        private readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Constructor
        /// </summary>
        public Logger()
        {
            string applicationDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var log4NetConfig = new FileInfo(applicationDir + @"\log4net.config");
            log4net.Config.XmlConfigurator.Configure(log4NetConfig);
        }

        /// <summary>
        /// Write an Info level message to the log
        /// </summary>
        /// <param name="message"></param>
        public void Info(object message)
        {
            _log.Info(message);
        }

        /// <summary>
        /// Write an Info level message and exception message to the log
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public void Info(object message, Exception exception)
        {
            _log.Info(message, exception);
        }

        /// <summary>
        /// Write a Debug level message to the log
        /// </summary>
        /// <param name="message"></param>
        public void Debug(object message)
        {
            _log.Debug(message);
        }
    }
}
