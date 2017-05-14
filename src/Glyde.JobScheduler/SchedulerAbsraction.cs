using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NLog;
using Quartz;
using Quartz.Impl.Matchers;
using Quartz.Spi;

namespace Glyde.JobScheduler
{
    public class SchedulerAbsraction : IScheduler
    {
        private readonly ILogger _log;
        private readonly IScheduler _schedulerImplementation;

        public SchedulerAbsraction(ISchedulerFactory schedulerFactory, IJobFactory jobFactory, ILogger log)
        {
            _log = log;
            _schedulerImplementation = schedulerFactory.GetScheduler().Result;
            _schedulerImplementation.JobFactory = jobFactory;

        }

        public async Task<bool> IsJobGroupPaused(string groupName)
        {
            return await _schedulerImplementation.IsJobGroupPaused(groupName);
        }

        public async Task<bool> IsTriggerGroupPaused(string groupName)
        {
            return await _schedulerImplementation.IsTriggerGroupPaused(groupName);
        }

        public async Task<SchedulerMetaData> GetMetaData()
        {
            return await _schedulerImplementation.GetMetaData();
        }

        public async Task<IReadOnlyList<IJobExecutionContext>> GetCurrentlyExecutingJobs()
        {
            return await _schedulerImplementation.GetCurrentlyExecutingJobs();
        }

        public async Task<IReadOnlyList<string>> GetJobGroupNames()
        {
            return await _schedulerImplementation.GetJobGroupNames();
        }

        public async Task<IReadOnlyList<string>> GetTriggerGroupNames()
        {
            return await _schedulerImplementation.GetTriggerGroupNames();
        }

        public async Task<ISet<string>> GetPausedTriggerGroups()
        {
            return await _schedulerImplementation.GetPausedTriggerGroups();
        }

        public async Task Start()
        {
            await _schedulerImplementation.Start();
        }

        public async Task StartDelayed(TimeSpan delay)
        {
            await _schedulerImplementation.StartDelayed(delay);
        }

        public async Task Standby()
        {
            await _schedulerImplementation.Standby();
        }

        public async Task Shutdown()
        {
            await _schedulerImplementation.Shutdown();
        }

        public async Task Shutdown(bool waitForJobsToComplete)
        {
            await _schedulerImplementation.Shutdown(waitForJobsToComplete);
        }

        public async Task<DateTimeOffset> ScheduleJob(IJobDetail jobDetail, ITrigger trigger)
        {
            if (!IsStarted)
            {
                _log.Info("Scheduler was not started, starting now ...");
                await Start();
            }
            return await _schedulerImplementation.ScheduleJob(jobDetail, trigger);
        }

        public async Task<DateTimeOffset> ScheduleJob(ITrigger trigger)
        {
            return await _schedulerImplementation.ScheduleJob(trigger);
        }

        public async Task ScheduleJobs(IDictionary<IJobDetail, ISet<ITrigger>> triggersAndJobs, bool replace)
        {
            await _schedulerImplementation.ScheduleJobs(triggersAndJobs, replace);
        }

        public async Task ScheduleJob(IJobDetail jobDetail, ISet<ITrigger> triggersForJob, bool replace)
        {
            await _schedulerImplementation.ScheduleJob(jobDetail, triggersForJob, replace);
        }

        public async Task<bool> UnscheduleJob(TriggerKey triggerKey)
        {
            return await _schedulerImplementation.UnscheduleJob(triggerKey);
        }

        public async Task<bool> UnscheduleJobs(IList<TriggerKey> triggerKeys)
        {
            return await _schedulerImplementation.UnscheduleJobs(triggerKeys);
        }

        public async Task<DateTimeOffset?> RescheduleJob(TriggerKey triggerKey, ITrigger newTrigger)
        {
            return await _schedulerImplementation.RescheduleJob(triggerKey, newTrigger);
        }

        public async Task AddJob(IJobDetail jobDetail, bool replace)
        {
            await _schedulerImplementation.AddJob(jobDetail, replace);
        }

        public async Task AddJob(IJobDetail jobDetail, bool replace, bool storeNonDurableWhileAwaitingScheduling)
        {
            await _schedulerImplementation.AddJob(jobDetail, replace, storeNonDurableWhileAwaitingScheduling);
        }

        public async Task<bool> DeleteJob(JobKey jobKey)
        {
            return await _schedulerImplementation.DeleteJob(jobKey);
        }

        public async Task<bool> DeleteJobs(IList<JobKey> jobKeys)
        {
            return await _schedulerImplementation.DeleteJobs(jobKeys);
        }

        public async Task TriggerJob(JobKey jobKey)
        {
            await _schedulerImplementation.TriggerJob(jobKey);
        }

        public async Task TriggerJob(JobKey jobKey, JobDataMap data)
        {
            await _schedulerImplementation.TriggerJob(jobKey, data);
        }

        public async Task PauseJob(JobKey jobKey)
        {
            await _schedulerImplementation.PauseJob(jobKey);
        }

        public async Task PauseJobs(GroupMatcher<JobKey> matcher)
        {
            await _schedulerImplementation.PauseJobs(matcher);
        }

        public async Task PauseTrigger(TriggerKey triggerKey)
        {
            await _schedulerImplementation.PauseTrigger(triggerKey);
        }

        public async Task PauseTriggers(GroupMatcher<TriggerKey> matcher)
        {
            await _schedulerImplementation.PauseTriggers(matcher);
        }

        public async Task ResumeJob(JobKey jobKey)
        {
            await _schedulerImplementation.ResumeJob(jobKey);
        }

        public async Task ResumeJobs(GroupMatcher<JobKey> matcher)
        {
            await _schedulerImplementation.ResumeJobs(matcher);
        }

        public async Task ResumeTrigger(TriggerKey triggerKey)
        {
            await _schedulerImplementation.ResumeTrigger(triggerKey);
        }

        public async Task ResumeTriggers(GroupMatcher<TriggerKey> matcher)
        {
            await _schedulerImplementation.ResumeTriggers(matcher);
        }

        public async Task PauseAll()
        {
            await _schedulerImplementation.PauseAll();
        }

        public async Task ResumeAll()
        {
            await _schedulerImplementation.ResumeAll();
        }

        public async Task<ISet<JobKey>> GetJobKeys(GroupMatcher<JobKey> matcher)
        {
            return await _schedulerImplementation.GetJobKeys(matcher);
        }

        public async Task<IReadOnlyList<ITrigger>> GetTriggersOfJob(JobKey jobKey)
        {
            return await _schedulerImplementation.GetTriggersOfJob(jobKey);
        }

        public async Task<ISet<TriggerKey>> GetTriggerKeys(GroupMatcher<TriggerKey> matcher)
        {
            return await _schedulerImplementation.GetTriggerKeys(matcher);
        }

        public async Task<IJobDetail> GetJobDetail(JobKey jobKey)
        {
            return await _schedulerImplementation.GetJobDetail(jobKey);
        }

        public async Task<ITrigger> GetTrigger(TriggerKey triggerKey)
        {
            return await _schedulerImplementation.GetTrigger(triggerKey);
        }

        public async Task<TriggerState> GetTriggerState(TriggerKey triggerKey)
        {
            return await _schedulerImplementation.GetTriggerState(triggerKey);
        }

        public async Task AddCalendar(string calName, ICalendar calendar, bool replace, bool updateTriggers)
        {
            await _schedulerImplementation.AddCalendar(calName, calendar, replace, updateTriggers);
        }

        public async Task<bool> DeleteCalendar(string calName)
        {
            return await _schedulerImplementation.DeleteCalendar(calName);
        }

        public async Task<ICalendar> GetCalendar(string calName)
        {
            return await _schedulerImplementation.GetCalendar(calName);
        }

        public async Task<IReadOnlyList<string>> GetCalendarNames()
        {
            return await _schedulerImplementation.GetCalendarNames();
        }

        public async Task<bool> Interrupt(JobKey jobKey)
        {
            return await _schedulerImplementation.Interrupt(jobKey);
        }

        public async Task<bool> Interrupt(string fireInstanceId)
        {
            return await _schedulerImplementation.Interrupt(fireInstanceId);
        }

        public async Task<bool> CheckExists(JobKey jobKey)
        {
            return await _schedulerImplementation.CheckExists(jobKey);
        }

        public async Task<bool> CheckExists(TriggerKey triggerKey)
        {
            return await _schedulerImplementation.CheckExists(triggerKey);
        }

        public async Task Clear()
        {
            await _schedulerImplementation.Clear();
        }

        public string SchedulerName => _schedulerImplementation.SchedulerName;

        public string SchedulerInstanceId => _schedulerImplementation.SchedulerInstanceId;

        public SchedulerContext Context => _schedulerImplementation.Context;

        public bool InStandbyMode => _schedulerImplementation.InStandbyMode;

        public bool IsShutdown => _schedulerImplementation.IsShutdown;

        public IJobFactory JobFactory
        {
            set { _schedulerImplementation.JobFactory = value; }
        }

        public IListenerManager ListenerManager => _schedulerImplementation.ListenerManager;

        public bool IsStarted => _schedulerImplementation.IsStarted;
    }
}