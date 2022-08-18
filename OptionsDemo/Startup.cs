using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OptionsDemo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptionsDemo
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
            //services.AddSingleton<OrderServiceOptions>();
            //services.Configure<OrderServiceOptions>(Configuration.GetSection("OrderService"));
            // 直接注册验证函数
            //services.AddOptions<OrderServiceOptions>().Configure(options =>
            //{
            //    Configuration.GetSection("OrderService").Bind(options);
            //}).Validate(options => options.MaxOrderCount < 10000, "MaxOrderCount cannot greater than 10000");

            // 使用Microsoft.Extensions.Options.DataAnnotations
            //services.AddOptions<OrderServiceOptions>().Configure(options =>
            //{
            //    Configuration.GetSection("OrderService").Bind(options);
            //}).ValidateDataAnnotations();

            // 实现 `IValidateOptions<TOptions>`
            services.AddOptions<OrderServiceOptions>().Configure(options =>
            {
                Configuration.GetSection("OrderService").Bind(options);
            }).Services.AddSingleton<IValidateOptions<OrderServiceOptions>, OrderServiceValidateOptions>();


            services.AddSingleton<IOrderService, OrderService>();// use IOptionsMonitor
            //services.AddScoped<IOrderService, OrderService>(); // use IOptionsSnapshot

            //services.PostConfigure<OrderServiceOptions>(options =>
            //{
            //    options.MaxOrderCount += 10000;
            //});

            services.AddControllers();
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
