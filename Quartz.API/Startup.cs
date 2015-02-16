using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.BuilderProperties;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
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
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute("MyROute", "jobs/{name}/trigger", new {controller = "Jobs", action = "PostTriggerJob"});


            appBuilder.UseFileServer(new FileServerOptions()
            {
                FileSystem = new PhysicalFileSystem("./Assets/assets"),
                RequestPath = new PathString("/assets")
            });

            
            appBuilder.Map("/quartzadmin", builder => builder.UseNancy());

            appBuilder.Map("/api", builder => builder.UseWebApi(config));

            


            //appBuilder.UseWebApi(config);
        }
    } 
}
