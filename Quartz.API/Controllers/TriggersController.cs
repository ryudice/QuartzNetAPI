using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Quartz.Impl.Matchers;
using Quartz.Impl.Triggers;

namespace Quartz.API.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/triggers")]
    public class TriggersController : ApiController
    {
        private IScheduler _scheduler;

        public TriggersController()
        {
            _scheduler = ConfigurationBuilder.CurrentScheduler;

        }

        [Route("{key}/resume")]
        public void PostRestartTrigger(string key)
        {
            var triggerKey = new TriggerKey(key);
            var trigger = _scheduler.GetTrigger(triggerKey);
            if (trigger == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            _scheduler.ResumeTrigger(triggerKey);
        }


        [Route("{key}/pause")]
        public void PostStopTrigger(string key)
        {
            var triggerKey = new TriggerKey(key);
            var trigger = _scheduler.GetTrigger(triggerKey);
            
            if (trigger == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _scheduler.PauseTrigger(triggerKey);
            
        }

        public object GetAllTriggers()
        {
            var triggerKeys = _scheduler.GetTriggerKeys(GroupMatcher<TriggerKey>.GroupEquals(SchedulerConstants.DefaultGroup));

            var triggers = triggerKeys.Select(key =>
            {
                
                var trigger = _scheduler.GetTrigger(key);
                
                string cronExpression = "";

                if (trigger is CronTriggerImpl)
                    cronExpression = ((CronTriggerImpl) trigger).CronExpressionString;

               

                return new TriggerDto
                {
                    Id = trigger.Key.Name.ToString(),
                    Name = trigger.Key.Name,
                    Description = trigger.Description,
                    Job = trigger.JobKey.ToString(),
                    PreviousFireTime=  trigger.GetPreviousFireTimeUtc(),
                    CronExpression= cronExpression,
                    State = _scheduler.GetTriggerState(key).ToString(),
                    Group= trigger.Key.Group
                };

            });

            return new {triggers};
        }
    }

    public class TriggerDto
    {
        public string State { get; set; }
        public string Job { get; set; }
        public string Name { get; set; }
        public string CronExpression { get; set; }
        public string Id { get; set; }
        public string Group { get; set; }
        public string Description { get; set; }
        public DateTimeOffset? PreviousFireTime { get; set; }
    }
}
