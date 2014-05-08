using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;

namespace Quartz.API
{
    public static class QuartzAPI
    {
        internal static ConfigurationBuilder ConfigurationBuilder { get; set; }

        static QuartzAPI()
        {
            ConfigurationBuilder = new ConfigurationBuilder();
        }

        public static void Configure(Action<IConfigurationBuilder> configure)
        {
            configure(ConfigurationBuilder);
        }

        static void ValidateConfiguration()
        {
            ConfigurationBuilder.Validate();
        }

        public static void Start(string baseAddress)
        {
            WebApp.Start<Startup>(url: baseAddress);
        }
    }
}
