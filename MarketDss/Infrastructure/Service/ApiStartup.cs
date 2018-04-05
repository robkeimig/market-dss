using System;
using log4net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace MarketDss.Infrastructure.Service
{
    internal class ApiStartup
    {
        private static readonly ILog Log = LogManager.GetLogger(nameof(ApiStartup));

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.Use(async(context, next) =>
            {
                try
                {
                    await next().ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = 500;
                    Log.Error($"Unhandled exception [500]: {ex}");
                    await context.Response.WriteAsync(ex.ToString()).ConfigureAwait(false);
                }
            });

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMvc();

            app.UseSwagger(c =>
            {
                c.RouteTemplate = "api/doc/{documentName}/swagger";
            });

            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "api/doc";
                c.SwaggerEndpoint("v1/swagger", "MarketDSS V1");
            });
        }
    }
}