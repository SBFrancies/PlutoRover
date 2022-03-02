using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using PlutoRover.Api.Interface;
using PlutoRover.Api.Services;
using PlutoRover.Api.Data;

namespace PlutoRover.Api
{
    public class Startup
    {
        private const string ApiTitle = "Pluto Rover API";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(a =>
            {
                a.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                a.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                a.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            services.AddSingleton<ICommandParser, CommandParser>();
            services.AddSingleton<IIdentifierGenerator, IdentifierGenerator>();
            services.AddSingleton<IRoverCommandModule, RoverCommandModule>();
            services.AddSingleton<IRoverRepository, RoverRepository>();
            services.AddSingleton<IRoverLauncher, RoverLauncher>();
            services.AddSingleton<IRoverService, RoverService>();

            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", ApiTitle);
                c.DocumentTitle = ApiTitle;
                c.EnableDeepLinking();

            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
