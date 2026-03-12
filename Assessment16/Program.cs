using Microsoft.AspNetCore.HttpOverrides;
using Assessment16.Middleware;

var builder = WebApplication.CreateBuilder(args);

// ✅ Load configuration based on environment
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

// Services
builder.Services.AddControllers();

// ✅ Swagger services (no OpenApiInfo needed)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ✅ Important behind IIS / reverse proxy
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

// ✅ Global exception middleware
app.UseMiddleware<GlobalExceptionMiddleware>();

// ✅ HSTS only in Production
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

// ✅ Enforce HTTPS redirect
app.UseHttpsRedirection();

// ✅ Swagger only in Development
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

app.MapControllers();
app.Run();