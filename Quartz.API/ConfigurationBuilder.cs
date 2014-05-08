using System;
using System.Web.Http;
using Quartz.API.Contracts;

namespace Quartz.API
{
    public class ConfigurationBuilder : IConfigurationBuilder
    {
        private ILoggingProvider _loggingProvider;
        internal static IScheduler CurrentScheduler { get; private set; }

        internal static HttpConfiguration HttpConfiguration { get; set; }


        internal ILoggingProvider GetLoggingProvider()
        {
            return _loggingProvider;
        }

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

        public void UseLogProvider(ILoggingProvider loggingProvider)
        {
            _loggingProvider = loggingProvider;
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