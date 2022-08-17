# DependencyInjectionDemo

## 为什么要使用依赖注入框架

- 借助依赖注入框架，我们可以轻松管理类之间的依赖，帮助我们在构建应用时遵循设计原则，确保代码的可维护性和可扩展性。
- ASP.NET Core的整个架构中，依赖注入框架提供了对象创建和生命周期管理的核心能力，各个组件相互协作，也是由依赖注入框架的能力来实现的。

## 相关的组件包

- Microsoft.Extensions.DependencyInjection.Abstractions

    抽象包，只包含接口的定义。

- - Microsoft.Extensions.DependencyInjection

    实现包，包含具体的实现。

使用了设计模式中的接口实现分离模式。这样组件只需要依赖抽象包中的抽象接口即可，而在实际的使用时只需注入具体的实现即可。在使用时也能根据自己的需要选择使用任意接口的实现。

## 核心类型

- IServiceCollection

    负责服务的注册。

- ServiceDescriptor

    服务注册时的信息。

- IServiceProvider

    具体的容器，由IServiceCollection Build出来的。

- IServiceScope

    表示一个容器的子容器的生命周期。

## 生命周期ServiceLifeTime

- 单例Singleton

    指在整个根容器的生命周期内对象都是单例，不管是在根容器、子容器、子容器的子容器中等获取得到的对象都是同一个。

- 作用域Scoped

    在Scope的生成周期内（指容器的生存周期内或者子容器的生存周期内），获取对象得到的都是同一个对象，即Scope内的单例模式。如果对应的容器释放了，那么对象也会随着被释放。

- 瞬时（暂时）Transient

    每一次去容器里面获取对象时，都会得到一个全新的对象。

## 服务注册

### 几种不同的服务注册方式

```csharp
services.AddSingleton<IMySingletonService, MySingletonService>();

services.AddSingleton<IMySingletonService>(new MySingletonService()); // 直接注入实例

services.AddSingleton<IMySingletonService>(serviceProvider =>
{
    // serviceProvider.GetService<>
    return new MySingletonService();
});
```

> 注意：框架不会管理（释放）使用直接注入实例这种方式注册的对象实例的生命周期。

### 尝试注册服务

1. `services.TryAddSingleton<IOrderService, OrderServiceEx>()` 尝试注册IOrderService接口的实现，如果已经有IOrderService接口的实现注册过了，就不会注册了。

2. `services.TryAddEnumerable(ServiceDescriptor.Singleton<IOrderService, OrderServiceEx>())` 尝试注册IOrderService接口的OrderServiceEx实现，如果已经有相同的接口和实现就跳过，否则就注册。

### 移除注册和替换注册

1. `services.RemoveAll<IOrderService>()` 移除IOrderService接口的所有注册。

2. `services.Replace(ServiceDescriptor.Singleton<IOrderService, OrderServiceEx>())` 把IOrderService接口的第一个注册替换成OrderServiceEx。 **如果原来没有IOrderService接口的注册，则会添加一个新的。**

### 模板服务的注册

```csharp
services.AddSingleton(typeof(IGenericService<>), typeof(GenericService<>))
```

## 服务获取

1. 通过Controller构造函数

2. 通过FromServices属性