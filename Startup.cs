using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using WebsiteManager.DatabaseContext;
using WebsiteManager.Factories;
using WebsiteManager.Factories.Interfaces;
using WebsiteManager.Helpers;
using WebsiteManager.Helpers.Interfaces;
using WebsiteManager.Repository;
using WebsiteManager.Repository.Interfaces;
using WebsiteManager.Services;
using WebsiteManager.Services.Interfaces;

namespace WebsiteManager
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Website Manager", Version = "v1" });
            });

            services.AddDbContext<WebsiteManagerContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            RegisterBusinessLogicServices(services);
            RegisterHelperMethods(services);
            RegisterRepositories(services);
            RegisterFactories(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }

        private static void RegisterBusinessLogicServices(IServiceCollection services)
        {
            services.AddTransient<IWebsiteService, WebsiteService>(); 
        }

        private static void RegisterRepositories(IServiceCollection services)
        {
            services.AddTransient<IWebsiteRepository, WebsiteRepository>();
        }

        private static void RegisterFactories(IServiceCollection services)
        {
            services.AddTransient<IWebsiteFactory, WebsiteFactory>();
        }

        private static void RegisterHelperMethods(IServiceCollection services)
        {
            services.AddTransient<IStringHash, StringHash>();
        }
    }
}