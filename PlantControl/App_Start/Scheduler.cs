using Quartz;
using Quartz.Impl;
using System;

namespace PlantControl
{
    public class Scheduler
    {
        public static void Start()
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            //JOB FOR LIMITS COMPARISSON
            IJobDetail jobLimitsCompare = JobBuilder.Create<JobLimitsComp>().Build();

            ITrigger jobLimitsCompareTrigger = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule
                  (s =>
                    s.WithIntervalInHours(24)
                    //s.WithIntervalInSeconds(60)
                    .OnEveryDay()
                    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(06, 00)))
                .Build();

            scheduler.ScheduleJob(jobLimitsCompare, jobLimitsCompareTrigger);


            //JOB FOR DATABASE BACKUP
            IJobDetail jobDBBackup = JobBuilder.Create<JobDBBackup>().Build();

            ITrigger jobDBBackuptrigger = TriggerBuilder.Create()
                .WithSchedule(CronScheduleBuilder
                    .WeeklyOnDayAndHourAndMinute(DayOfWeek.Saturday, 10, 00)
                    .InTimeZone(TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time")))
                .Build();

            scheduler.ScheduleJob(jobDBBackup, jobDBBackuptrigger);
        }
    }
}