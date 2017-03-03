namespace CrystalQuartz.Web.Comands
{
    using CrystalQuartz.Core;
    using CrystalQuartz.Core.SchedulerProviders;
    using CrystalQuartz.Web.Comands.Inputs;
    using Quartz;

    public class PauseTriggerCommand : AbstractOperationCommand<TriggerInput>
    {
        public PauseTriggerCommand(ISchedulerProvider schedulerProvider, ISchedulerDataProvider schedulerDataProvider) : base(schedulerProvider, schedulerDataProvider)
        {
        }

        protected override void PerformOperation(TriggerInput input)
        {
            var triggerKey = new TriggerKey(input.Trigger, input.Group);

            foreach (var scheduler in Scheduler)
            {
                if (scheduler.GetTrigger(triggerKey) != null)
                    scheduler.PauseTrigger(triggerKey);
            }
            //Scheduler.PauseTrigger(triggerKey);
        }
    }
}