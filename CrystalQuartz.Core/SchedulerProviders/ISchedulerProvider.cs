namespace CrystalQuartz.Core.SchedulerProviders
{
    using Quartz;
    using System.Collections.Generic;

    public interface ISchedulerProvider
    {
        /// <summary>
        /// Initializes provider and creates all necessary instances 
        /// (scheduler factory and scheduler itself).
        /// </summary>
        void Init();

        /// <summary>
        /// Gets scheduler instance. Should return same instance on every call.
        /// </summary>
        List<IScheduler> Scheduler { get; }
    }
}