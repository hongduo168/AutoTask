using AutoTask.Helper;
using AutoTask.Helper.Model;
using log4net;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace AutoTask.Service
{
    public partial class SchedulerService : ServiceBase
    {
        StdSchedulerFactory schedulerFactory;

        public SchedulerService()
        {
            InitializeComponent();

            schedulerFactory = new StdSchedulerFactory();

        }

        public void Start(string[] args)
        {
            this.OnStart(args);
        }

        protected override void OnStart(string[] args)
        {
            Console.WriteLine("Starting scheduler...");
            Trace.WriteLine("Starting scheduler...");

            DbHelper.ExpiredDataClear();
            var list = DbHelper.GetSettingAll();

            CompilerHelper ch = new CompilerHelper();
            StringBuilder sb = new StringBuilder();
            foreach (var item in list)
            {
                //生成job代码
                var code = CodeHelper.CodeString(item.JOB_NAME, item.REQUESTHOST, item.REQUESTPATH);
                sb.Append(code);
                sb.AppendLine();

                ////编译为类对象
                //var type = CompilerHelper.BuildClass(code, item.JOB_NAME);
                //if (type == null)
                //{
                //    Trace.WriteLine("编译出现异常！");
                //    continue;
                //}
            }
            ch.BuildDLL(sb.ToString());

            foreach (var item in list)
            {
                var scheduler = schedulerFactory.GetScheduler();

                var jobKey = JobKey.Create(item.JOB_NAME, item.JOB_GROUP);

                var type = ch.GetType(item.JOB_NAME);
                if (type == null)
                {
                    Trace.WriteLine(item.JOB_NAME + "类型错误");
                    continue;

                }
                //创建人物
                var job = JobBuilder.Create(type)
                        .WithIdentity(jobKey)
                        .WithDescription(item.DESCRIPTION)
                        .StoreDurably(true)
                        .RequestRecovery(true)
                        .Build();

                DateTime _time1 = DateTime.Now;
                DateTime _time2 = DateTime.Now.AddYears(1);
                if (!DateTime.TryParse(item.START_TIME, out _time1))
                {
                    _time1 = DateTime.Now;
                }
                if (!DateTime.TryParse(item.END_TIME, out _time2))
                {
                    _time2 = DateTime.Now.AddYears(100);
                }

                var trigger = TriggerBuilder.Create()
                    .WithIdentity(item.TRIGGER_NAME, item.TRIGGER_GROUP)
                    .WithCronSchedule(item.CRON_EXPRESSION) //"*/10 * * * * ?" /*默认每月1号1点*/ "0 0 1 1 * ?" 
                    .ForJob(job)
                    .StartAt(_time1)
                    .EndAt(_time2)
                    .Build();

                List<TriggerKey> pauseKeys = new List<TriggerKey>();
                if (scheduler.CheckExists(jobKey))
                {
                    if (scheduler.GetTriggerState(trigger.Key) == TriggerState.Paused)
                    {
                        pauseKeys.Add(trigger.Key);
                    }
                    scheduler.DeleteJob(jobKey);
                    //scheduler.ScheduleJob(job, trigger);
                }
                scheduler.ScheduleJob(job, trigger);
                foreach (var key in pauseKeys)
                {
                    //暂停原来暂停的任务
                    scheduler.PauseTrigger(key);
                }
                scheduler.Start();

            }


        }

        protected override void OnStop()
        {
            Trace.WriteLine("Ending scheduler...");
            Trace.WriteLine("======================华丽的分隔线===========================");
            if (schedulerFactory != null)
            {
                var scheduler = schedulerFactory.GetScheduler();
                scheduler.Shutdown(true);
            }

        }

    }


}
