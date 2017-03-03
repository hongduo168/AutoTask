namespace CrystalQuartz.Web.Comands
{
    using CrystalQuartz.Core;
    using CrystalQuartz.Core.SchedulerProviders;
    using CrystalQuartz.Web.Comands.Inputs;
    using Quartz;

    public class ExecuteNowCommand : AbstractOperationCommand<JobInput>
    {
        public ExecuteNowCommand(ISchedulerProvider schedulerProvider, ISchedulerDataProvider schedulerDataProvider) : base(schedulerProvider, schedulerDataProvider)
        {
        }

        protected override void PerformOperation(JobInput input)
        {
            foreach (var scheduler in Scheduler)
            {
                var key = new JobKey(input.Job, input.Group);
                if (scheduler.GetJobDetail(key) != null)
                    scheduler.TriggerJob(key);
            }
            //Scheduler.TriggerJob(new JobKey(input.Job, input.Group));
        }
    }
}