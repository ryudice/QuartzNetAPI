using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegrationTesting.Jobs;
using Quartz;
using Quartz.API;
using Quartz.Impl;

namespace IntegrationTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Setting up scheduler");
            var scheduler = SetupScheduler();

            scheduler.Start();

            QuartzAPI.Configure(builder =>
            {
                builder.UseScheduler(scheduler);
                builder.EnableCors();
            });

            QuartzAPI.Start("http://localhost:2000");

            Console.WriteLine("Scheduler is running...");
            Console.ReadLine();
        }



        private static IScheduler SetupScheduler()
        {
            var stdSchedulerFactory = new StdSchedulerFactory();
            var scheduler = stdSchedulerFactory.GetScheduler();

            var jobDetail = new JobDetailImpl("Dummy Job", typeof(DummyJob));

            var trigger = TriggerBuilder.Create().WithIdentity("Dummy Job Trigger").WithCronSchedule("0 0/1 * ? * *").ForJob(jobDetail).StartNow().Build();

            scheduler.ScheduleJob(jobDetail, trigger);

            return scheduler;
        }
    }
}
