using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Quartz.Impl.Matchers;

namespace Quartz.API.Controllers
{
    [EnableCors("*","*","*")]
    [RoutePrefix("api/jobs")]
    public class JobsController : ApiController
    {
        public class ProductDto
	{
            public string Name { get; set; }
            public string Schedule { get; set; }
            public string Id { get; set; }
            public string Group { get; set; }
	}

        
        private IScheduler _scheduler;

        public JobsController()
        {

            _scheduler = ConfigurationBuilder.CurrentScheduler;
        }

        [HttpPost]
        [ActionName("trigger")]
        [Route("{id}/trigger")]
        public HttpResponseMessage PostTriggerJob(string id)
        {
            JobKey jobKey = new JobKey(id);

  
            _scheduler.TriggerJob(jobKey);

            return new HttpResponseMessage();
        }

        public object GetAllJobs()
        {
            Quartz.Collection.ISet<JobKey> jobKeys = _scheduler.GetJobKeys(GroupMatcher<JobKey>.GroupEquals(SchedulerConstants.DefaultGroup));
            var jobDetail = _scheduler.GetJobDetail(jobKeys.First());
            var triggerKeys = _scheduler.GetTriggerKeys(GroupMatcher<TriggerKey>.GroupEquals(SchedulerConstants.DefaultGroup));

            return
                new
            {
                jobs = jobKeys.Select(key => 
                    new ProductDto()
                    {
                        Name = key.Name, 
                        Schedule = string.Join( ", ",_scheduler.GetTriggersOfJob(key).Select(trigger => trigger.StartTimeUtc.ToString())),
                        Id= key.Name.ToString(),
                        Group = key.Group


                    }).ToList()
            }
            ;

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
