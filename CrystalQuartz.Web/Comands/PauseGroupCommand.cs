namespace CrystalQuartz.Web.Comands
{
    using CrystalQuartz.Core;
    using CrystalQuartz.Core.SchedulerProviders;
    using CrystalQuartz.Web.Comands.Inputs;
    using Quartz;
    using Quartz.Impl.Matchers;

    public class PauseGroupCommand : AbstractOperationCommand<GroupInput>
    {
        public PauseGroupCommand(ISchedulerProvider schedulerProvider, ISchedulerDataProvider schedulerDataProvider)
            : base(schedulerProvider, schedulerDataProvider)
        {
        }

        protected override void PerformOperation(GroupInput input)
        {
            var matcher = GroupMatcher<JobKey>.GroupEquals(input.Group);
            foreach (var scheduler in Scheduler)
            {
                if (scheduler.GetJobKeys(matcher) != null)
                    scheduler.PauseJobs(matcher);
            }
            //Scheduler.PauseJobs(GroupMatcher<JobKey>.GroupEquals(input.Group));
        }
    }
}