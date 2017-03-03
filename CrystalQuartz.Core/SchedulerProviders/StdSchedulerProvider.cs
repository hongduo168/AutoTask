namespace CrystalQuartz.Core.SchedulerProviders
{
    using System;
    using System.Collections.Specialized;
    using Quartz;
    using Quartz.Impl;
    using System.Collections.Generic;

    public class StdSchedulerProvider : ISchedulerProvider
    {
        protected List<IScheduler> _scheduler = new List<IScheduler>();

        protected virtual bool IsLazy
        {
            get { return false; }
        }

        public void Init()
        {
            if (!IsLazy)
            {
                LazyInit();
            }
        }

        protected virtual void LazyInit()
        {
            NameValueCollection properties = null;
            try
            {
                properties = GetSchedulerProperties();
                ISchedulerFactory schedulerFactory = new StdSchedulerFactory(properties);
                _scheduler.Add(schedulerFactory.GetScheduler());
                InitScheduler(_scheduler);
            }
            catch (Exception ex)
            {
                throw new SchedulerProviderException("Could not initialize scheduler", ex, properties);
            }

            if (_scheduler == null)
            {
                throw new SchedulerProviderException(
                    "Could not initialize scheduler", properties);
            }
        }

        protected virtual void InitScheduler(List<IScheduler> scheduler)
        {
        }

        protected virtual NameValueCollection GetSchedulerProperties()
        {
            return new NameValueCollection();
        }

        public virtual List<IScheduler> Scheduler
        {
            get
            {
                if (_scheduler == null || _scheduler.Count == 0)
                {
                    LazyInit();
                }

                return _scheduler;
            }
        }
    }
}