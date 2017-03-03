using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoTaskTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            AutoTask.Helper.RequestHelper.POSTAsync("http://localhost:58066/", "/SAAS/OrderAutoDo");
        }
    }
}
