using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
//using Microsoft.OpenAPI.UI
using PersonalAssistance.Data;

namespace PersonalAssistance
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //
            services.AddDbContext<ItemsDbContext>(option =>
                option.UseSqlServer(
                    @"Data Source=.\SQLEXPRESS01;Initial Catalog=Items;User Id=Trail;Password=Password;"));
            //services.AddDbContext<ItemsDbContext>(option =>
            //    option.UseSqlServer(Configuration.GetConnectionString("ItemsDb")));
            services.AddResponseCaching();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("V1",
                    new OpenApiInfo
                    {
                        Title = "Items API", Description = "For representational purpose to get all Items for you"
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)//, ItemsDbContext itemsDbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            //itemsDbContext.Database.Migrate();
            app.UseResponseCaching();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("Swagger/V1/swagger.json", "Items API Document");
                c.RoutePrefix = String.Empty;
            });

        }
    }
}
