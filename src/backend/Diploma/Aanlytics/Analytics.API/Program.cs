using System.Reflection;
using Analytics.Application;
using Analytics.Infrastructure;
using Analytics.Infrastructure.Persistance.Context;
using Common.API;
using Common.API.Extenisions;
using Common.Infrastructure;
using Common.Infrastructure.Kafka;
using Common.Infrastructure.Migrator;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerDocumentation(Assembly.GetExecutingAssembly().GetName().Name!);
builder.Services.AddControllers();
builder.Services.AddAnalyticsInfrastructure(builder.Configuration);
builder.Services.AddAnalyticsApplication();
builder.Services.AddCorsPolicy(builder.Configuration);
builder.Services.AddApiAuthentication(builder.Configuration);
builder.Services.AddProducer(builder.Configuration.GetSection(nameof(KafkaConfig)));
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "Analytics_";
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseDbMigrator<AnalyticsDbContext>();
app.UseHttpsRedirection();
app.MapControllers();
app.UseCorsPolicy();
app.UseAuthentication();
app.UseAuthorization();

app.Run();