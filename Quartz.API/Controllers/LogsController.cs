using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Quartz.API.Contracts;

namespace Quartz.API.Controllers
{
    //[RoutePrefix("api/logs")]
    public class LogsController : ApiController
    {

        
        public IEnumerable<string> GetAllLogs()
        {
            ILoggingProvider loggingProvider = QuartzAPI.ConfigurationBuilder.GetLoggingProvider();
            if (loggingProvider!=null)
            {
                return loggingProvider.GetLogs();
            }

            throw new HttpResponseException(HttpStatusCode.InternalServerError); 
        } 

    }
}
