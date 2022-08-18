# ConfigurationCommandLineDemo

## 支持的命令格式

- 无前缀的 key=value 模式
- 双中横杠模式 --key=value 或 --key value
- 正斜杆模式 /key=value 或 /key value

> 备注：等号分隔符和空格分隔符不能混用

## 命令替换模式

即为命令参数提供别名。

- 必须以单划线(-)或双划线(--)开头
- 映射字典不能包含重复key

