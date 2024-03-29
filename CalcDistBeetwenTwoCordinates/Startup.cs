﻿using CalcDistBeetwenTwoCordinates.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CalcDistBeetwenTwoCordinates
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureServices();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.Configure(env);
        }
    }
}
