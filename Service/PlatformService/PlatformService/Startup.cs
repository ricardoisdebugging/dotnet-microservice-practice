using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PlatformService.Data;
using PlatformService.PlatformDomain;
using PlatformService.Utils.CommandService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlatformService
{
    public class Startup
    {
        public Startup(
            IConfiguration configuration,
            IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        private readonly IWebHostEnvironment _env;
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            if (_env.IsDevelopment())
            {
                Console.WriteLine(">>>Using InMem Db");
                services.AddDbContext<ApplicationDbContext>(opt =>
                    opt.UseInMemoryDatabase("InMemory"));
            }
            else if (_env.IsProduction())
            {
                Console.WriteLine(">>>Using SQL Server");
                services.AddDbContext<ApplicationDbContext>(opt =>
                    opt.UseSqlServer(Configuration.GetConnectionString("PlatformsConn")));
            }

            services.AddScoped<IPlatformRepository, PlatformRepository>();

            // Register AutoMapper
            var domainAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            services.AddAutoMapper(domainAssemblies);

            //Register HttpClient Factor
            services.AddHttpClient<ICommandClient, CommandClient>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PlatformService", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PlatformService v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            MockInMemoryDatabase.MockPopulation(app, _env.IsProduction());

            Console.WriteLine($">>> CommandService Endpoint {Configuration["CommandService"]}");
        }
    }
}
