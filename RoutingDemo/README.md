# RoutingDemo

## 路由注册方式

- 路由模板的方式

    之前传统的方式。可以用来作为MVC页面Web配置。
    
- RouteAttribute方式

    适合Web API的方式。

## 路由约束

即路由如何进行匹配。

- 类型约束
- 范围约束
- 正则表达式
- 是否必选
- 自定义IRouteConstraint

## URL生成

根据路由的信息生成URL地址。

- LinkGenerator
- IUrlHelper

## Web API定义

- Restful不是必须的
- 约定好API的表达契约
- 将API约束在特定目录下，如/api/
- 使用ObsoleteAttribute标记即将废弃的API
