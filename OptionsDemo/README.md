# OptionsDemo

## 特性

- 支持单例模式读取配置
- 支持快照
- 支持配置变更通知
- 支持运行时动态修改选项值

## 设计原则

- 接口分离原则(ISP)，我们的类不应该依赖它不使用的配置
- 关注点分离(SoC)，不同组件、服务、类之间的配置不应相互依赖或耦合

## 建议

- 为服务设计XXXOptions
- 使用 `IOptions<XXXOptions>` 、`IOptionsSnapshot<XXXOptions>` 、 `IOptionsMonitor<XXXOptions>` 作为服务构造函数的参数。

## 关键类型

- `IOptionsMonitor<out TOptions>`
- `IOptionsSnapshot<out TOptions>`

## 使用场景

- 范围作用域类型使用IOptionsSnapshot
- 单例服务使用IOptionsMonitor

## 通过代码更新选项

- `IPostConfigureOptions<TOptions>`

## 三种选项验证方法

- 直接注册验证函数
- 实现 `IValidateOptions<TOptions>`
- 使用Microsoft.Extensions.Options.DataAnnotations

