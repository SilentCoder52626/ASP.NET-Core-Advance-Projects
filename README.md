# CQRS and MediatR in ASP.NET Core - Building Scalable Systems

**CQRS (Command Query Responsibility Segregation)** is a software architectural pattern that separates the read and write operations of a system into two distinct parts.

✅ Commands: Modify data (Create, Update, Delete)

✅ Queries: Retrieve data (Read operations)

This separation enhances scalability, performance, and maintainability.

**Benefits of CQRS**

✔️ Better scalability and performance

✔️ Cleaner and more maintainable code

✔️ Allows independent optimization of read and write operations

✔️ Works well with Event Sourcing and Microservices

**What is MediatR?**

MediatR is a lightweight in-process messaging library that helps decouple components in ASP.NET Core applications. It enables communication between components without direct dependencies using the Mediator pattern.

**Why Use MediatR?**

✔️ Reduces direct dependencies between layers

✔️ Simplifies unit testing by eliminating direct service dependencies

✔️ Improves maintainability and scalability


**Install Required NuGet Packages**
 ```
dotnet add package MediatR
```

# Structured Logging with Serilog in ASP.NET Core
Logging is an essential part of any application for debugging, monitoring, and tracking issues. In .NET Core, Serilog is a popular structured logging library that provides powerful features like log enrichment, various sinks (file, console, database), and JSON-formatted logs.
Why Use Serilog?

✔️ Supports structured logging (JSON format).

✔️ Multiple sinks (console, file, database, cloud, etc.).

✔️ Easy integration with .NET Core.

✔️ Enrichment features like MachineName, ThreadId, and RequestId.

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

# Global Exception Handling in .NET

Global exception handling ensures applications remain stable by managing unexpected errors efficiently.

**Importance**

✔️ Prevents Crashes: Avoids application failures due to unhandled exceptions.

✔️ Improves Debugging: Facilitates logging for troubleshooting.

✔️ Enhances User Experience: Provides friendly error messages.

✔️ Centralized Handling: Reduces redundant error-handling code.

**Old Method (Pre .NET 6)**

✔️ AppDomain.UnhandledException: For console apps.

✔️ Application_Error in Global.asax: For ASP.NET MVC.

✔️ Middleware-based Handling: Custom middleware in .NET Core.

**New Method (Since .NET 6)**

✔️ Built-in Exception Middleware: Centralized handling.

✔️ UseExceptionHandler: Simplified global error capture.

✔️ Minimal APIs Integration: Clean, efficient error handling.

.NET's newer global exception handling methods simplify error management, enhance maintainability, and improve debugging. Implementing it ensures a robust and user-friendly application.


# Validation with MediatR Pipeline and FluentValidation

**Fluent Validation** is a popular .NET library for building strongly-typed validation rules. It simplifies input validation with a fluent API and provides better maintainability.

```
public class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Email).EmailAddress().WithMessage("Invalid email format");
    }
} 
```

**Fluent Validation in MediatR Pipeline**

Fluent Validation can be integrated into the MediatR pipeline using a validation behavior that runs before request handlers. This ensures that requests are validated before processing.

```
public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse> where TRequest : class
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(next);

        if (validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);
            var validationResults = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            var failures = validationResults
           .Where(r => r.Errors.Count > 0)
           .SelectMany(r => r.Errors)
           .ToList();

            if (failures.Count > 0)
                throw new ValidationException(failures);
        }
        return await next().ConfigureAwait(false);

    }
}
```

Register Fluent Validation and MediatR in Program.cs:

```
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
    cfg.AddOpenBehavior(typeof(RequestResponseLoggingBehavior<,>));
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));


});
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

```

# Options Pattern in .NET  

The **Options Pattern** in .NET is a design pattern used to manage application configuration settings in a structured and strongly typed manner. It leverages dependency injection to provide configuration values to different parts of the application.  

**Importance:**  

- **Strongly Typed Configuration:** Ensures type safety and avoids magic strings.  
- **Separation of Concerns:** Keeps configuration logic separate from business logic.  
- **Dependency Injection Support:** Enables easy testing and better maintainability.  
- **Dynamic Updates:** Using `IOptionsMonitor`, applications can react to configuration changes at runtime.  
- **Improved Readability:** Organizes configuration settings into meaningful classes, making the code more readable and manageable.

# In-Memory Caching in .NET

In-Memory Caching in .NET is a technique that stores frequently accessed data in memory to improve application performance and reduce database or API calls.

**Importance**  
- **Enhances Performance** – Reduces response time by serving data from memory instead of external sources.  
- **Minimizes Load** – Decreases the number of database or API requests, improving scalability.  
- **Cost-Effective** – Saves resources by preventing redundant computations or fetch operations.  
- **Thread-Safe & Efficient** – .NET provides built-in `IMemoryCache` for safe and efficient in-memory storage.

🚀 Start using In-Memory Caching in .NET to boost your application's speed and efficiency!  

# Distributed Caching in .NET

Distributed Caching in .NET stores data across multiple servers or instances, allowing applications to access cached data globally instead of relying on local memory.

**Importance**  
- **Scalability** – Works across multiple servers, ideal for load-balanced environments.  
- **Persistence** – Survives application restarts, unlike in-memory caching.  
- **Consistency** – Ensures all instances get updated cache data.  
- **Supports Large Data** – Can handle more extensive data compared to local memory constraints.

# Distributed Caching in .NET

Distributed Caching in .NET stores data across multiple servers or instances, allowing applications to access cached data globally instead of relying on local memory.

**Importance**  

- **Scalability** – Works across multiple servers, ideal for load-balanced environments.  
- **Persistence** – Survives application restarts, unlike in-memory caching.  
- **Consistency** – Ensures all instances get updated cache data.  
- **Supports Large Data** – Can handle more extensive data compared to local memory constraints.

**Why Use Distributed Caching Instead of In-Memory?**

- **In-Memory Cache** is tied to a single instance and resets on restarts.  
- **Distributed Cache** provides global access, making it more reliable for multi-server apps.  

🚀 Use **Distributed Caching** for high availability and performance in cloud or multi-node environments!  

# Distributed Caching with Redis and MediatR in .NET


Distributed Caching with Redis stores frequently used data in an external in-memory database, making it accessible across multiple servers. MediatR is used to implement **CQRS (Command Query Responsibility Segregation)**, ensuring a clean and scalable request-handling approach.


🚀 **Why Use Redis for Distributed Caching?**  

- **Global Accessibility** – Cached data is available across multiple servers, ensuring consistency.  
- **High Performance** – Redis is an in-memory datastore, making data retrieval ultra-fast.  
- **Persistence & Reliability** – Cache remains even if the application restarts.  
- **Scalability** – Works well in cloud and microservices architectures.  

⚡ **Why Use MediatR?**  

- **Decouples Business Logic** – Separates request handling, improving maintainability.  
- **Enhances Code Readability** – Reduces dependencies and simplifies architecture.  
- **Supports Pipeline Behaviors** – Allows caching, logging, and validation without modifying request handlers.  

🔥 **Redis + MediatR = Optimized Performance**  

By combining **Redis for caching** and **MediatR for structured request handling**, applications can:  

✅ Reduce database load.  
✅ Improve API response times.  
✅ Maintain a scalable and modular architecture.  

🚀 Start using **Redis with MediatR** in .NET for a high-performance, scalable, and maintainable application!  

