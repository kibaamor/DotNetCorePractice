using ExceptionDemo.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExceptionDemo
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
            // services.AddMvc();

            services.AddMvc(mvcOptions =>
            {
                //mvcOptions.Filters.Add<MyExceptionFilter>();
                //mvcOptions.Filters.Add<MyExceptionFilterAttribute>();
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            //app.UseExceptionHandler("/error");

            //app.UseExceptionHandler(errApp =>
            //{
            //    errApp.Run(async context =>
            //    {
            //        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();

            //        var ex = exceptionHandlerPathFeature?.Error;

            //        var knownException = ex as IKnownException;
            //        if (knownException == null)
            //        {
            //            var logger = context.RequestServices.GetService<ILogger<Startup>>();
            //            logger.LogError(ex, ex.Message);
            //            knownException = KnownException.Unknown;
            //        }
            //        else
            //        {
            //            knownException = KnownException.FromKnownException(knownException);
            //        }

            //        await context.Response.WriteAsync($"{knownException.ErrorCode}: {knownException.Message}");
            //    });
            //});

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
