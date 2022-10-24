using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherFetcher.Constants;
using WeatherFetcher.ServiceContracts;
using WeatherFetcher.Services;

namespace WeatherFetcher
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WeatherFetcher", Version = "v1" });
            });
            services
                .AddHttpClient(NamedHttpClients.WeatherApi, httpClient =>
                {
                    httpClient.BaseAddress = new Uri(Configuration.GetSection("OpenWeatherServices")
                        .GetSection("WeatherApiUrl").Value
                            + $"?appid={Configuration.GetSection("OpenWeatherServices").GetSection("ApiKey").Value}");
                });
            services
                .AddHttpClient(NamedHttpClients.GeocodingApi, httpClient =>
                {
                    httpClient.BaseAddress = new Uri(Configuration.GetSection("OpenWeatherServices")
                        .GetSection("GeocodingApiUrl").Value
                            + $"?appid={Configuration.GetSection("OpenWeatherServices").GetSection("ApiKey").Value}");
                });
            
            services.AddTransient<IWeatherService, WeatherService>();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    policyBuilder => policyBuilder
                        .AllowCredentials()
                        .SetIsOriginAllowed(origin => true)
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WeatherFetcher v1"));
            }
            app.UseCors("CorsPolicy");
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
