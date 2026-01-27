using System.Reflection;
using Common.API;
using Common.API.Extenisions;
using Common.Infrastructure.Kafka;
using Common.Infrastructure.Migrator;
using Microsoft.Extensions.Logging.Abstractions;
using Notifications.API.Consumers;
using Notifications.API.Hubs;
using Notifications.Application;
using Notifications.Application.SignalR;
using Notifications.Infrastructure;
using Notifications.Infrastructure.Persistance.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();
builder.Services.AddSwaggerDocumentation(Assembly.GetExecutingAssembly().GetName().Name!);
builder.Services.AddControllers();
builder.Services.AddNotificationInfrastructure(builder.Configuration);
builder.Services.AddNotificationApplicationService();
builder.Services.AddCorsPolicy(builder.Configuration);
builder.Services.AddApiAuthentication(builder.Configuration);
builder.Services.AddKafkaConsumers(builder.Configuration.GetSection(nameof(KafkaConfig)));
builder.Services.AddHubs();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "Organization_";
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseRouting();
app.UseCorsPolicy();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseDbMigrator<NotificationDbContext>();
app.UseHubs();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();