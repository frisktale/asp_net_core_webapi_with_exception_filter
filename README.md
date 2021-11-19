# 添加中间件形式的全局异常处理
中间件形式的异常处理默认全局启用  
启用方法有两种  
1. 定义异常处理类和`Invoke`方法（必须命名为"Invoke")，并用`app.UseMiddleware`方法注册中间件（见Program 15行）。用此方法应该在中间件中记录错误日志
1. 定义异常处理类和静态处理方法(随意命名)，并用`app.UseExceptionHandler`注册中间件（见Program 17至20行）。用词方法不需要再中间件中记录错误日志，因为`Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware`会记录错误日志