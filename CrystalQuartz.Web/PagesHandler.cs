namespace CrystalQuartz.Web
{
    using System.Collections.Generic;
    using CrystalQuartz.Core;
    using CrystalQuartz.Core.SchedulerProviders;
    using CrystalQuartz.Web.FrontController;
    using CrystalQuartz.WebFramework.Request;
    using System.Web;
    using System.Web.Security;

    public class PagesHandler : FrontControllerHandler
    {
        private static readonly ISchedulerDataProvider SchedulerDataProvider;

        private static readonly ISchedulerProvider SchedulerProvider;

        static PagesHandler()
        {
            SchedulerProvider = Configuration.ConfigUtils.SchedulerProvider;
            SchedulerProvider.Init();
            SchedulerDataProvider = new DefaultSchedulerDataProvider(SchedulerProvider);

        }

        public PagesHandler() : base(GetProcessors())
        {
        }

        private static IList<IRequestHandler> GetProcessors()
        {
            HttpContext ctx = HttpContext.Current;

            var result = new List<IRequestHandler>();
            result.Add(new FileRequestHandler(typeof(PagesHandler).Assembly, "CrystalQuartz.Web.Content."));

            if (ctx.User.Identity.IsAuthenticated)
            {
                var handlers = new CrystalQuartzPanelApplication(SchedulerProvider, SchedulerDataProvider).Config.Handlers;
                result.AddRange(handlers);
            }
            else
            {
                //Î´ÊÚÈ¨
            }


            return result;
        }
    }
}