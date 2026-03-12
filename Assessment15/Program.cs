using System.Threading.RateLimiting;
using Assessment15.Data;
using Assessment15.Middlewares;
using Assessment15.Services;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Controllers + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ Response Caching (middleware)
builder.Services.AddResponseCaching();

// ✅ EF Core
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ✅ DI: Service
builder.Services.AddScoped<ProductService>();

// ✅ CORS: allow specific frontend origin + methods
var origin = builder.Configuration["Cors:FrontendOrigin"] ?? "http://localhost:5173";
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendOnly", policy =>
    {
        policy.WithOrigins(origin)
              .WithMethods("GET", "POST", "PUT", "DELETE")
              .AllowAnyHeader();
    });
});

// ✅ Rate Limiting: 5 requests/minute per IP
builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

    options.AddPolicy("per-ip-5-per-minute", httpContext =>
    {
        var ip = httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";
        return RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: ip,
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 5,
                Window = TimeSpan.FromMinutes(1),
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                QueueLimit = 0
            });
    });
});

var app = builder.Build();

// ✅ Global error middleware (before controllers)
app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Assessment15 API v1");

    // 👇 This makes Swagger open at root URL
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();
app.UseCors("FrontendOnly");
app.UseRateLimiter();
app.UseResponseCaching();

app.MapControllers().RequireRateLimiting("per-ip-5-per-minute");

app.Run();