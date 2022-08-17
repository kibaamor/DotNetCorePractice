using DependencyInjectionScopeAndDisposableDemo.Services;
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
using System.Threading.Tasks;

namespace DependencyInjectionScopeAndDisposableDemo
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
            //services.AddSingleton<IOrderService>(new OrderService());
            //services.AddSingleton<IOrderService>((serviceProvider) => new OrderService());

            //services.AddScoped<IOrderService, OrderService>();
            services.AddTransient<IOrderService, OrderService>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // �Ӹ������л�ȡ���񣬽����±���ȡ�ķ���ʵ��ֱ�������˳�ʱ���ܱ��ͷ�
            var orderService = app.ApplicationServices.GetService<IOrderService>();
            Console.WriteLine($"Configure => {nameof(orderService)}: {orderService.GetHashCode()}");


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
