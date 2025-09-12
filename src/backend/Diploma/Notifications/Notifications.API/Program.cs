using System.Reflection;
using Common.API;
using Common.API.Extenisions;
using Common.Infrastructure.Migrator;
using Notifications.API.Hubs;
using Notifications.Application;
using Notifications.Infrastructure;
using Notifications.Infrastructure.Persistance.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();
builder.Services.AddLogging();
builder.Services.AddMemoryCache();
builder.Services.AddSwaggerDocumentation(Assembly.GetExecutingAssembly().GetName().Name!);
builder.Services.AddControllers();
builder.Services.AddNotificationInfrastructureServices(builder.Configuration);
builder.Services.AddNotificationApplicationService();
builder.Services.AddCorsPolicy(builder.Configuration);
builder.Services.AddApiAuthentication(builder.Configuration);

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