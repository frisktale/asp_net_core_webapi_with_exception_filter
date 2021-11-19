# 添加Filter形式的全局异常处理
定义`ExceptionFilter` 后，有两种方法应用  
1. 全局添加
    ```csharp
    builder.Services.AddControllers(
        opt=>opt.Filters.Add(typeof(ExceptionFilter))
    );
    ```
    这种方法添加会将异常处理应用到所有的Controller
    见Program 地8行
1. 将Attribute添加到Controller上
    ```csharp
    [TypeFilter(typeof(ExceptionFilter))]
    ```
    见WeatherForecastController第八行
