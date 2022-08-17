using DependencyInjectionDemo.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjectionDemo
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
            //services.AddSingleton<IMySingletonService, MySingletonService>();
            //services.AddSingleton<IMySingletonService>(new MySingletonService());
            services.AddSingleton<IMySingletonService>(serviceProvider =>
            {
                // serviceProvider.GetService<>
                return new MySingletonService();
            });
            services.AddScoped<IMyScopedService, MyScopedService>();
            services.AddTransient<IMyTransientService, MyTransientService>();

            services.AddSingleton<IOrderService, OrderService>();
            services.TryAddSingleton<IOrderService, OrderServiceEx>();
            services.TryAddEnumerable(ServiceDescriptor.Singleton<IOrderService, OrderServiceEx>());

            //services.RemoveAll<IOrderService>();
            //services.Replace(ServiceDescriptor.Singleton<IOrderService, OrderServiceEx>());

            services.AddSingleton(typeof(IGenericService<>), typeof(GenericService<>));

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
