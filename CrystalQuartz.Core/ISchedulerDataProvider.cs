namespace CrystalQuartz.Core
{
    using CrystalQuartz.Core.Domain;
    using Quartz;
    using System.Collections.Generic;

    /// <summary>
    /// Translates Quartz.NET entyties to CrystalQuartz objects graph.
    /// </summary>
    public interface ISchedulerDataProvider
    {
        List<SchedulerData> Data { get; }

        JobDetailsData GetJobDetailsData(string name, string group);

        TriggerData GetTriggerData(TriggerKey key);
    }
}