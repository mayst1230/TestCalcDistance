using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace CalcDistBeetwenTwoCordinates.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Конфигурация Pipeline.
        /// </summary>
        /// <param name="app">Настраиваемое веб-прилоежние.</param>
        /// <param name="env">Конфигурация среды.</param>
        public static void Configure(this IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseHttpsRedirection();
            if (!env.IsProduction())
            {
                var provider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();

                app.UseSwagger(c =>
                {
                    c.RouteTemplate = "/swagger/{documentName}/CalculateDistanceApi.json";
                });

                app.UseSwaggerUI(c =>
                {
                    foreach (var description in provider.ApiVersionDescriptions.OrderByDescending(i => i.ApiVersion))
                    {
                        c.SwaggerEndpoint($"{description.GroupName}/CalculateDistanceApi.json", description.GroupName.ToUpperInvariant());
                    }

                    c.ConfigObject.DisplayOperationId = true;
                    c.ConfigObject.DefaultModelsExpandDepth = 0;
                    c.ConfigObject.Filter = "";
                    c.ConfigObject.ShowCommonExtensions = true;
                });
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors("AllowAllHeaders");
            app.UseMvc();
        }
    }
}
