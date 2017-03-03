using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoTask.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace AutoTask.Service.Tests
{
    [TestClass()]
    public class SchedulerServiceTests
    {
        [TestMethod()]
        public void SchedulerServiceTest()
        {
            log4net.Config.XmlConfigurator.Configure();
            Trace.Listeners.Add(new MSTraceListener());

            SchedulerService s = new SchedulerService();
            s.Start(new string[] { });
            Assert.Fail();
        }
    }
}