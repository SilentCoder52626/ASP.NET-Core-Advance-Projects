## CQRS and MediatR in ASP.NET Core - Building Scalable Systems

**CQRS (Command Query Responsibility Segregation)** is a software architectural pattern that separates the read and write operations of a system into two distinct parts.

**Key Concepts of CQRS**:
Write Operations (Commands) and Read Operations (Queries) are handled separately.
Different models are optimized for each type of operation.
This separation can lead to simpler and more scalable architectures, especially in complex systems where the read and write patterns differ significantly.

## Structured Logging with Serilog in ASP.NET Core
Logging is an essential part of any application for debugging, monitoring, and tracking issues. In .NET Core, Serilog is a popular structured logging library that provides powerful features like log enrichment, various sinks (file, console, database), and JSON-formatted logs.
Why Use Serilog?

✅ Supports structured logging (JSON format).

✅ Multiple sinks (console, file, database, cloud, etc.).

✅ Easy integration with .NET Core.

✅ Enrichment features like MachineName, ThreadId, and RequestId.

 **Install Required NuGet Packages**
 ```
dotnet add package Serilog.AspNetCore
dotnet add package Serilog.Sinks.Console
dotnet add package Serilog.Sinks.File
dotnet add package Serilog.Formatting.Compact
```
**Program.cs**
```
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();


app.UseSerilogRequestLogging();

```
**Appsetting.json**
``` json

  "Serilog": {
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
        "Microsoft": "Warning",
        "Microsoft.AspNetCore.Hosting.Diagnostics": "Error",
        "Microsoft.Hosting.Lifetime": "Information"
      }
    },

    "WriteTo": [
      {
        "Name": "File",
        "Args": {
          "path": "logs/log.json",
          "rollingInterval": "Day",
          "rollOnFileSizeLimit": true,
          "formatter": "Serilog.Formatting.Compact.CompactJsonFormatter, Serilog.Formatting.Compact"
        }
      }
    ],
    "Enrich": [
      "WithMachineName",
      "WithProcessId",
      "WithThreadId"
    ]
  },
```

Serilog makes logging in .NET Core applications easy and powerful. With structured logging and multiple sinks, you can effectively monitor and debug your application. Try integrating Serilog with Seq, Elasticsearch, or Application Insights for even better log management!

🔹 Happy Logging with Serilog! 🚀
