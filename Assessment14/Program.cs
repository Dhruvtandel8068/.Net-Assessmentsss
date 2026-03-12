using Assessment14.BackgroundJobs;
using Assessment14.Options;
using Assessment14.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ Bind EmailSettings from appsettings.json
builder.Services.Configure<EmailSettings>(
    builder.Configuration.GetSection("EmailSettings"));

// ✅ Register Services
builder.Services.AddSingleton<NotificationQueue>();
builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<SmsService>();

// ✅ Register Background Worker (Hosted Service)
builder.Services.AddHostedService<NotificationWorker>();

var app = builder.Build();

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
app.MapControllers();

app.Run();