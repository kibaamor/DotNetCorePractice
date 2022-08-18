# DependencyInjectionAutofacDemo

## 什么情况下需要引入第三方容器组件

- 基于名称的注入

    根据名称来区分不同服务的实现。

- 属性注入

    直接把类注册到某个类的属性中，而不需要定义构造函数。

- 子容器

    与之前使用到的Scope类似。

- 基于动态代理的AOP

    在服务中注入额外的行为。

## 核心扩展点

.NET Core的依赖注入框架的核心扩展点是 `public interface IServiceProviderFactory<TContainerBuilder>` ，第三方的依赖注入容器都是使用了这个类来作为扩展点把自己注入到整个框架中。
