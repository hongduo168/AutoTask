namespace CrystalQuartz.Core.SchedulerProviders
{
    using Quartz;
    using Quartz.Impl;
    using System;
    using System.Collections.Specialized;

    public class RemoteSchedulerProvider : StdSchedulerProvider
    {
        public string SchedulerHost { get; set; }

        protected override bool IsLazy
        {
            get { return true; }
        }

        protected override NameValueCollection GetSchedulerProperties()
        {
            var properties = base.GetSchedulerProperties();
            properties["quartz.scheduler.proxy"] = "true";
            properties["quartz.scheduler.proxy.address"] = SchedulerHost;
            return properties;
        }

        protected override void LazyInit()
        {
            NameValueCollection properties = null;
            try
            {
                var hosts = SchedulerHost.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                foreach (var host in hosts)
                {
                    properties = GetSchedulerProperties();
                    properties["quartz.scheduler.proxy.address"] = host;
                    ISchedulerFactory schedulerFactory = new StdSchedulerFactory(properties);
                    _scheduler.Add(schedulerFactory.GetScheduler());
                    InitScheduler(_scheduler);
                }
            }
            catch (Exception ex)
            {
                throw new SchedulerProviderException("Could not initialize scheduler", ex, properties);
            }

            if (_scheduler == null || _scheduler.Count == 0)
            {
                throw new SchedulerProviderException(
                    "Could not initialize scheduler", properties);
            }
        }

    }
}