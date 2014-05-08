using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Quartz.API.Contracts;

namespace Quartz.API
{
    public interface IConfigurationBuilder
    {
        void EnableCors();

        HttpConfiguration HttpConfiguration { get; set; }

        void UseScheduler(IScheduler scheduler);

        void Validate();
        void UseLogProvider(ILoggingProvider loggingProvider);
    }
}
