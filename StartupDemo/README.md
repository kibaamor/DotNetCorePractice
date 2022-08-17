# StartupDemo

## 代码执行顺序

1. ConfigureWebHostDefaults

    注册应用程序必要的几个组件。比如：配置的组件、容器的组件等。

2. ConfigureHostConfiguration

    用来配置应用程序启动时必要的配置。比如应用程序启动时所需要监听的端口，需要监听的URL地址等。
    
    这个地方可以用来嵌入一些配置到框架中。

3. ConfigureAppConfiguration

    这个地方可以嵌入一些配置供应用程序读取。这些配置可以在后续应用程序执行过程中每个组件读取。

4. ConfigureServices或者(Startup+Startup.ConfigureServices)或者ConfigureLogging

    这三个函数的执行顺序与代码中调用的顺序相同。
    
    > Startup构造函数和Startup.ConfigureServices函数的调用由代码 `webBuilder.UseStartup<Startup>()` 触发。

    一般在ConfigureServices中注册服务。

5. Startup.Configure

    这个地方用于注册中间件，这些中间件作用于HttpContext请求处理过程。

## Startup类

Startup类不是必要的。下面两段代码作用相同：

```csharp
Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder =>
    {
        Console.WriteLine("ConfigureWebHostDefaults");
        webBuilder.UseStartup<Startup>();
    });

class Startup
{
    void ConfigureServices(IServiceCollection services);
    void Configure(IApplicationBuilder app, IWebHostEnvironment env);
}
```

与

```csharp
Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.ConfigureServices(services => {});
        webBuilder.Configure(builder => {});
    });
```

但是使用Startup类有助于分离代码。
