using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AspNetCoreBackgroundDemo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddLogging();
builder.Services.AddSingleton<EmailService>();
builder.Services.AddSingleton<SmsService>();

// Register background service
builder.Services.AddHostedService<NotificationBackgroundService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
