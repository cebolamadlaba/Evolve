using Assessment.Web.Data;
using Assessment.Web.Infrastructure;
using Assessment.Web.Views;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using NServiceBus;
using System.IO;

namespace Assessment.Web
{
    public class Startup
    {
        IEndpointInstance endpoint;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IViewCalculationHeaders, ViewCalculationHeaders>();
            services.AddScoped<IViewCalculationResults, ViewCalculationResults>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
            
            services.Configure<FormOptions>(x =>
            {
                //20 GB should be sufficient
                x.MultipartBodyLengthLimit = 20971520000;
            });
            services.AddSingleton<IFileParser, FileParser>();
            services.AddDbContextPool<EvolveContext>(builder => 
            {
                builder.UseSqlServer(Configuration.GetConnectionString("Data"));
            });
            var endpointConfiguration = new EndpointConfiguration("Assessment.Web");
            endpointConfiguration.UseContainer<ServicesBuilder>(
               customizations: customizations =>
               {
                   customizations.ExistingServices(services);
               });
            var trans = endpointConfiguration.UseTransport<LearningTransport>();
            trans.StorageDirectory("Bus");
            //trans.Routing().RouteToEndpoint(typeof(Infrastructure.CalculationCommand), "Assessment.Web");
            endpoint = Endpoint.Start(endpointConfiguration).GetAwaiter().GetResult();
            services.AddSingleton<IMessageSession>(endpoint);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime applicationLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
            applicationLifetime.ApplicationStopping.Register(OnShutdown);
        }
        void OnShutdown()
        {
            endpoint?.Stop().GetAwaiter().GetResult();
        }
    }
}
