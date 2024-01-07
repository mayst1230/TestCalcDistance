using CalcDistBeetwenTwoCordinates.Interfaces;
using CalcDistBeetwenTwoCordinates.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CalcDistBeetwenTwoCordinates.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Настройка служб в контейнере DI.
        /// </summary>
        /// <param name="services">Коллекция сервисов в контейнере DI.</param>
        public static void ConfigureServices(this IServiceCollection services)
        {
            ConfigureBase(services);
            ConfigureApiBase(services);
            ConfigureCustomServices(services);
            ConfigureSwagger(services);
        }

        /// <summary>
        /// Настройка самописных сервисов.
        /// </summary>
        /// <param name="services">Коллекция сервисов в контейнере DI.</param>
        private static void ConfigureCustomServices(IServiceCollection services)
        {
            services.AddScoped<ICalculateDistanceService, CalculateDistanceService>();
        }

        /// <summary>
        /// Настройка Swagger.
        /// </summary>
        /// <param name="services">Коллекция сервисов в контейнере DI.</param>
        private static void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSingleton<IConfigureOptions<SwaggerGenOptions>, SwaggerGenApiOptions>();
            services.AddSwaggerGen();
        }

        /// <summary>
        /// Настройка базовых сервисов.
        /// </summary>
        /// <param name="services">Коллекция сервисов в контейнере DI.</param>
        private static void ConfigureBase(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        }

        /// <summary>
        /// Настройка базовых сервисов для работы API.
        /// </summary>
        /// <param name="services">Коллекция сервисов в контейнере DI.</param>
        private static void ConfigureApiBase(IServiceCollection services)
        {
            services.AddMvcCore(options =>
            {
                options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status200OK));
                options.Filters.Add(new ProducesResponseTypeAttribute(typeof(Models.ApiErrorResponse), StatusCodes.Status400BadRequest));
                options.Filters.Add(new Models.ApiErrorResponseFilter());
                options.OutputFormatters.RemoveType<StringOutputFormatter>();
            })
            .AddJsonFormatters()
            .AddJsonOptions(options =>
            {
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllHeaders",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });


            services.AddApiVersioning(config =>
            {
                config.ReportApiVersions = true;
                config.ApiVersionReader = new UrlSegmentApiVersionReader();
            });

            services.AddVersionedApiExplorer(opt => opt.SubstituteApiVersionInUrl = true);

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
        }
    }
}
