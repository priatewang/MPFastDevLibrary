# MPFastDevLibrary
我的快速开发平台。my fast development platform。

## 我的工具帮助类

### SerializableHelper
序列化辅助类
二进制和xml的序列化和反序列化

### MapHelper
映射工具类
类映射，用于同属性名类快速拷贝

### CsvHelper
Csv读写帮助类
支持直接读取行，读写DataTable等

### DateTimeHelper
时间帮助类
```c#
 //分钟转时分格式（HH:mm）
 public static string MinuteToFormatHHmm(int minutes) 

 //秒数转时分秒格式
  public static string SecendToFormatHms(int seconds)
```

### EnumHelper
枚举帮助类
用于枚举的相关操作，获取枚举项集合，获取枚举描述和描述集合等

获取枚举项的字符串集合
 ```c#
 public static List<string> GetEnumContents<T>()
 ```

 获取枚举所有项的描述集合
  ```c#
 public static List<string> GetEnumDescriptions<T>()
  ```

  获取枚举值的描述（需要添加[Description]特性）
  ```c#
 public static string GetDescription(this Enum value)
  ```

 将描述转成枚举
```c#
public static T EnumConvertByDescription<T>(string desc)
```

 通过名称转换成枚举
 ```c#
 public static T ConvertToEnum<T>(string enumStr)
 ```

 将描述转成枚举
 ```c#
public static object EnumConvertByDescription(Type type, string desc)
 ```

 ## 扩展方法
 常用的一些扩展方法

 ### ObservableCollectionExtensions
 ObservableCollection的扩展方法


| 方法 |功能|
| ---- | ---- | 
|AddRange|批量添加| 
|RemoveRange|批量删除| 
|AddNotExist|条件添加，如果不存在则添加| 

### DateTimeEx
DateTime 扩展方法

| 方法 |功能|
| ---- | ---- | 
|ToWeekDay_CH|转换成星期几| 
|ToWeekNumber| 获取周数（第几周）| 
|IsToday|判断时间是否今天| 

### ListExtensions
List 扩展方法

| 方法 |功能|
| ---- | ---- | 
|AddNotExist|条件添加，如果不存在则添加| 
|DeleteRange| 批量删除|