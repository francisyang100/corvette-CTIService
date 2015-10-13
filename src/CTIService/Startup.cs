using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Routing;
using Microsoft.Framework.DependencyInjection;
using System.IO;
using ECS.Common;
using ECS.Common.Logging;
using log4net;
using System.Reflection;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CTIService
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            Global.DataPath = env.WebRootPath;
        }

        // This method gets called by a runtime.
        // Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().Configure<MvcOptions>(options =>
            {
                var serializerSetting = options.OutputFormatters
                    .OfType<JsonOutputFormatter>()
                    .First()
                    .SerializerSettings;

                serializerSetting.ContractResolver = new CamelCasePropertyNamesContractResolver();
                //serializerSetting.NullValueHandling = NullValueHandling.Ignore;
                serializerSetting.DateFormatHandling = DateFormatHandling.IsoDateFormat;
            });

            // Uncomment the following line to add Web API services which makes it easier to port Web API 2 controllers.
            // You will also need to add the Microsoft.AspNet.Mvc.WebApiCompatShim package to the 'dependencies' section of project.json.
            // services.AddWebApiConventions();
        }




        // Configure is called after ConfigureServices is called.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            System.IO.FileInfo log4netFI;

            // configure Log4Net for Loggins

            log4netFI = new System.IO.FileInfo(Path.Combine(Global.DataPath, "log4net.xml"));
            Log.Configure(log4netFI);

            // Configure the HTTP request pipeline.
            app.UseStaticFiles();

            // Add MVC to the request pipeline.
            app.UseMvc();
            // Add the following route for porting Web API 2 controllers.
            // routes.MapWebApiRoute("DefaultApi", "api/{controller}/{id?}");
        }
    }
}
