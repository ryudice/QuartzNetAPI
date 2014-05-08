using System.Web.Http;
using Owin;

namespace Quartz.API
{
    public class Startup
    {

        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = ConfigurationBuilder.HttpConfiguration;
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute("MyROute", "api/jobs/{name}/trigger", new {controller = "Jobs", action = "PostTriggerJob"});

            appBuilder.UseWebApi(config);
        }
    } 
}
