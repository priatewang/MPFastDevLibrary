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

#### Csv与DataTable

```c#
/// <summary>
/// 读取Csv文件，加载到DataTable
/// </summary>
/// <param name="path">csv文件路径</param>
/// <param name="hasTitle">是否有标题行</param>
/// <param name="SafeLevel">安全等级：0:错误格式行正常添加；1：错误行忽略（不添加），2：出现错误弹出异常</param>
/// <returns></returns>
public static DataTable ReadCsvToDataTable(string path, bool hasTitle = false, int SafeLevel = 0)


/// <summary>
/// 以文件流形式读取Csv文件，加载到DataTable
/// </summary>
/// <param name="path">csv文件路径</param>
/// <param name="hasTitle">是否有标题行</param>
/// <param name="SafeLevel">安全等级：0:错误格式行正常添加；1：错误行忽略（不添加），2：出现错误弹出异常</param>
/// <returns></returns>
/// <exception cref="Exception"></exception>
public static DataTable ReadCsvByStream(string path, bool hasTitle = false, int SafeLevel = 0)

/// <summary>
/// 以文件流形式将DataTable写入csv文件
/// </summary>
/// <param name="dt">DataTable对象</param>
/// <param name="path">文件路径</param>
/// <param name="hasTitle">是否有标题行</param>
/// <returns></returns>
public static bool WriteToCsvByDataTable(DataTable dt, string path, bool hasTitle = false)

```

#### Csv文件与对象集合List<T>

```c#
/// <summary>
/// 读取Csv，返回对象集合
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="path">文件路径</param>
/// <returns></returns>
public static List<T> ReadCsv<T>(string path)  where T : class, new()

/// <summary>
/// 对象集合反写入csv文件
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="list">对象集合</param>
/// <param name="path">写入文件路径</param>
/// <param name="headers">需要的属性名称集合</param>
public static void WriteCsv<T>(IEnumerable<T> list, string path, string[] headers = null)

```

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