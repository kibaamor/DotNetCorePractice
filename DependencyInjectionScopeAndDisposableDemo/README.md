# DependencyInjectionScopeAndDisposableDemo

## 相关接口

- IServiceScope

## 实现IDisposable接口类型的释放

- DI只负责释放由其创建的对象实例
- DI在容器或子容器释放时，释放由其创建的对象实例

## 建议

- 避免在根容器获取实现了IDisposable接口的瞬时服务
- 避免手动创建实现了IDisposable对象，应该使用容器来管理其生命周期

## 容器

- 每个请求都会创建一个容器
- 使用Controller中的HttpContext.RequestServices可以获得当前请求的容器，并可以使用 `HttpContext.RequestServices.CreateScope()` 创建一个当前请求的子容器。

## 注意

- 从根容器中获取服务，将导致被获取的服务实例直到程序退出时才能被释放
