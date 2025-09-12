using System.Reflection;
using Chat.API.Hubs;
using Chat.Application;
using Chat.Infrastructure;
using Chat.Infrastructure.Persistance.Context;
using Common.API;
using Common.API.Extenisions;
using Common.Infrastructure.Migrator;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();
builder.Services.AddLogging();
builder.Services.AddMemoryCache();
builder.Services.AddSwaggerDocumentation(Assembly.GetExecutingAssembly().GetName().Name!);
builder.Services.AddControllers();
builder.Services.AddInfrastructureChatService(builder.Configuration);
builder.Services.AddChatApplication();
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
app.UseDbMigrator<ChatDbContext>();
app.UseHubs();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();