using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTask.Helper
{
    public static class TraceHelper
    {
        private static ILog logger = LogManager.GetLogger("ServiceLogger");
        public static void WriteLine(string message)
        {
            logger.Info(message);
        }

        public static void Write(string message)
        {
            logger.Error(message);
        }
    }
}
