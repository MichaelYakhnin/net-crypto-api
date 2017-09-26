using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;
using Quartz.Logging;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TickerMonitor
{
    public class Scheduler
    {
        private static StdSchedulerFactory factory;
        private static IScheduler scheduler;
        private static List<JobConfig> _job_list;

        public static void LoadJobsFromDB(List<JobConfig> job_list)
        {
            _job_list = job_list;
        }

        public static async Task RunJobs()
        {
            try
            {
                // Grab the Scheduler instance from the Factory
                NameValueCollection props = new NameValueCollection
                {
                    { "quartz.serializer.type", "binary" }
                };
                 factory = new StdSchedulerFactory(props);
                 scheduler = await factory.GetScheduler();

                // and start it off
                await scheduler.Start();
                
                foreach (var jobAsset in _job_list)
                {
                    if (jobAsset.Provider.Equals("Bitfinex"))
                    {
                        // define the job and tie it to our HelloJob class
                        await CreateJobSchedule(jobAsset, new BitfinexJob().GetType());
                    }

                    if (jobAsset.Provider.Equals("Bitstamp"))
                    {
                        await CreateJobSchedule(jobAsset, new BitstampJob().GetType());
                    }

                }
                   

            }
            catch (SchedulerException se)
            {
                Console.WriteLine(se);
            }
        }

        private static async Task CreateJobSchedule(JobConfig jobAsset, Type type)
        {
            IJobDetail job = JobBuilder.Create(type)
                .WithIdentity(jobAsset.Asset, jobAsset.Provider)
                .UsingJobData("url", jobAsset.Url)
                .UsingJobData("OrderNumber", jobAsset.OrderNumber)
                .Build();

            // Trigger the job to run now, and then repeat every 3 seconds
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity(jobAsset.Asset, jobAsset.Provider)
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(jobAsset.TimeJobTimer)
                    .RepeatForever())
                .Build();
            //// Tell quartz to schedule the job using our trigger

            await scheduler.ScheduleJob(job, trigger);
            scheduler.ListenerManager.AddJobListener(new RateJobListener(), KeyMatcher<JobKey>.KeyEquals(new JobKey(jobAsset.Asset, jobAsset.Provider)));
        }
    

        public static async Task StopJobs()
        {
            try
            {
                 await scheduler.Shutdown();
            }
            catch (SchedulerException se)
            {
                Console.WriteLine(se);
            }
        }

        // simple log provider to get something to the console
        public class ConsoleLogProvider : ILogProvider
        {
            public Logger GetLogger(string name)
            {
                return (level, func, exception, parameters) =>
                {
                    if (level >= LogLevel.Info && func != null)
                    {
                        Console.WriteLine("[" + DateTime.Now.ToLongTimeString() + "] [" + level + "] " + func(), parameters);
                    }
                    return true;
                };
            }

            public IDisposable OpenNestedContext(string message)
            {
                throw new NotImplementedException();
            }

            public IDisposable OpenMappedContext(string key, string value)
            {
                throw new NotImplementedException();
            }
        }
    }
}
