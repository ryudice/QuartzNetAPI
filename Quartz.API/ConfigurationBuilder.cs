using System;
using System.Web.Http;

namespace Quartz.API
{
    public class ConfigurationBuilder : IConfigurationBuilder
    {
        internal static IScheduler CurrentScheduler { get; private set; }

        internal static HttpConfiguration HttpConfiguration { get; set; }



        static ConfigurationBuilder()
        {
            HttpConfiguration = new HttpConfiguration();
        }

        public void EnableCors()
        {
            HttpConfiguration.EnableCors();
        }

        public void BaseAddress(string url)
        {
            
        }

        HttpConfiguration IConfigurationBuilder.HttpConfiguration
        {
            get { return HttpConfiguration; }
            set { HttpConfiguration = value; }
        }


        public void UseScheduler(IScheduler scheduler)
        {
            CurrentScheduler = scheduler;
        }

        public void Validate()
        {
            if (CurrentScheduler == null)
                throw new Exception("You must configure a scheduler");
        }
    }
}