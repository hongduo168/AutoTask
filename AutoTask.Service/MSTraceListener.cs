using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTask.Service
{
    public class MSTraceListener : TraceListener
    {
        private ILog logger = LogManager.GetLogger("ServiceLogger");
        public override void Write(string message)
        {
            logger.Error(message);
        }

        public override void WriteLine(string message)
        {
            logger.Info(message);
        }
    }
}
