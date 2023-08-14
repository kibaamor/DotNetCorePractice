using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace StaticFilesDemo
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
            //services.AddDirectoryBrowser();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            //app.UseDefaultFiles();
            //app.UseDirectoryBrowser();
            app.UseStaticFiles();

            app.UseStaticFiles(new StaticFileOptions
            {
                RequestPath = "/other",
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "file"))
            });

            // 不是以/api请求开头的路径全部重定向到默认首页
            app.MapWhen(context => !context.Request.Path.Value.StartsWith("/api"), builder =>
            {
                //var option = new RewriteOptions();
                //option.AddRedirect(".*", "/index.html");
                //builder.UseRewriter(option);
                //builder.UseStaticFiles();

                builder.Run(async c =>
                {
                    var file = env.WebRootFileProvider.GetFileInfo("index.html");
                    c.Response.ContentType = "text/html";
                    using (var fileStream = new FileStream(file.PhysicalPath, FileMode.Open, FileAccess.Read))
                    {
                        await StreamCopyOperation.CopyToAsync(fileStream, c.Response.Body, null, c.RequestAborted);
                    }
                });
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
