using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Quartz.Impl.Matchers;
using Quartz.Impl.Triggers;

namespace Quartz.API.Controllers
{
    public class TriggersController : ApiController
    {
        private IScheduler _scheduler;

        public TriggersController()
        {
            _scheduler = ConfigurationBuilder.CurrentScheduler;

        }

        public IEnumerable<TriggerDto> GetAllTriggers()
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
                    Name = trigger.Key.ToString(),
                    Job = trigger.JobKey.ToString(),
                    CronExpression= cronExpression,
                    State = _scheduler.GetTriggerState(key).ToString()
                };

            });
            
            return triggers;
        }
    }

    public class TriggerDto
    {
        public string State { get; set; }
        public string Job { get; set; }
        public string Name { get; set; }
        public string CronExpression { get; set; }
    }
}
