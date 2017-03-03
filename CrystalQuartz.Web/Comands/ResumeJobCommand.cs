namespace CrystalQuartz.Web.Comands
{
    using CrystalQuartz.Core;
    using CrystalQuartz.Core.SchedulerProviders;
    using CrystalQuartz.Web.Comands.Inputs;
    using Quartz;

    public class ResumeJobCommand : AbstractOperationCommand<JobInput>
    {
        public ResumeJobCommand(ISchedulerProvider schedulerProvider, ISchedulerDataProvider schedulerDataProvider)
            : base(schedulerProvider, schedulerDataProvider)
        {
        }

        protected override void PerformOperation(JobInput input)
        {
            var key = new JobKey(input.Job, input.Group);
            foreach (var scheduler in Scheduler)
            {
                if (scheduler.GetJobDetail(key) != null)
                    scheduler.ResumeJob(key);
            }
            //Scheduler.ResumeJob(new JobKey(input.Job, input.Group));
        }
    }
}