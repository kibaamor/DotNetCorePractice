using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using DependencyInjectionAutofacDemo.Services;
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

namespace DependencyInjectionAutofacDemo
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
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<MyService>().As<IMyService>();

            //builder.RegisterType<MyServiceV2>().Named<IMyService>("service2");// 命名注册

            //builder.RegisterType<MyNameService>();
            //builder.RegisterType<MyServiceV2>().As<IMyService>().PropertiesAutowired(); // 属性注入

            // AOP
            //builder.RegisterType<MyInterceptor>();
            //builder.RegisterType<MyNameService>();
            //builder.RegisterType<MyServiceV2>().As<IMyService>().PropertiesAutowired().InterceptedBy(typeof(MyInterceptor)).EnableInterfaceInterceptors();

            // 子容器
            builder.RegisterType<MyNameService>().InstancePerMatchingLifetimeScope("myscope");
        }

        public ILifetimeScope AutofacContainer { get; private set; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            this.AutofacContainer = app.ApplicationServices.GetAutofacRoot();

            //var service = this.AutofacContainer.Resolve<IMyService>();
            //service.ShowCode();

            // 使用命名注册
            //var service2 = this.AutofacContainer.ResolveNamed<IMyService>("service2");
            //service2.ShowCode();

            //var service2 = this.AutofacContainer.Resolve<IMyService>();
            //service2.ShowCode();

            using (var myscope = this.AutofacContainer.BeginLifetimeScope("myscope"))
            {
                var service0 = myscope.Resolve<MyNameService>();
                using (var scope = myscope.BeginLifetimeScope())
                {
                    var service1 = scope.Resolve<MyNameService>();
                    var service2 = scope.Resolve<MyNameService>();

                    Console.WriteLine($"service0==service1: {service0 == service1}");
                    Console.WriteLine($"service2==service1: {service2 == service1}");
                }
            }

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
