using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StarterCore.Data;
using StarterCore.Helpers;
using StarterCore.Hubs;
using Swashbuckle.AspNetCore.Swagger;

namespace StarterCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            //mssql db
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<StarterCoreContext>(options =>
                options.UseSqlServer(connection));

            //in-memory
            //services.AddDbContext<StarterCoreContext>(opt => opt.UseInMemoryDatabase("StarterCore"));

            services.AddAutoMapper();
            services.AddCors();
            services.AddMemoryCache();
            services.AddSignalR();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = AdditionalData.Info.Swagger.Title.Value,
                    Version = AdditionalData.Info.Swagger.Version.Value,
                    Description = AdditionalData.Info.Swagger.Description.Value,
                    Contact = new Contact
                    {
                        Name = AdditionalData.Info.Swagger.Name.Value
                    }
                });
            });
            AdditionalData.Initialize(Environment.ContentRootPath);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
             
                app.UseHsts();
            }
            app.UseCors(builder =>
                builder.AllowAnyOrigin());
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(
                    AdditionalData.Info.Swagger.EndpointUrl.Value as string,
                    AdditionalData.Info.Swagger.EndpointName.Value as string);
                c.RoutePrefix = string.Empty;
            });
            app.UseSignalR(routes =>
            {
                routes.MapHub<UpdateHub>("/updateHub");
            });
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
