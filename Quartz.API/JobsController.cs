using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Quartz;
using Quartz.API;
using Quartz.Impl.Matchers;


namespace Saon.Tecoloco.EmailJobs.Api
{
    [EnableCors("*","*","*")]
    public class JobsController : ApiController
    {
        public class ProductDto
	{
            public string Name { get; set; }
            public string Schedule { get; set; }
	}

        
        private IScheduler _scheduler;

        public JobsController()
        {

            _scheduler = ConfigurationBuilder.CurrentScheduler;
        }

        public HttpResponseMessage PostTriggerJob(string name)
        {
            JobKey jobKey = new JobKey(name);

            var jobDetail = _scheduler.GetJobDetail(jobKey);
            var trigger = _scheduler.GetTrigger(new TriggerKey("sfa"));
            _scheduler.TriggerJob(jobKey);

            return new HttpResponseMessage();
        }

        public IEnumerable<ProductDto> GetAllJobs()
        {
            Quartz.Collection.ISet<JobKey> jobKeys = _scheduler.GetJobKeys(GroupMatcher<JobKey>.GroupEquals(SchedulerConstants.DefaultGroup));
            var jobDetail = _scheduler.GetJobDetail(jobKeys.First());
            var triggerKeys = _scheduler.GetTriggerKeys(GroupMatcher<TriggerKey>.GroupEquals(SchedulerConstants.DefaultGroup));

            return jobKeys.Select(key => new ProductDto() { Name = key.Name, Schedule = string.Join( ", ",_scheduler.GetTriggersOfJob(key).Select(trigger => trigger.StartTimeUtc.ToString())) }).ToList();
        } 


        public HttpResponseMessage Get(int id)
        {
            
            return new HttpResponseMessage()
            {
                Content = new StringContent("Hola mamon")
            };
        }
    }
}
