using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;

namespace IntegrationTesting.Jobs
{
    class DummyJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine("I'm running");
        }
    }
}
