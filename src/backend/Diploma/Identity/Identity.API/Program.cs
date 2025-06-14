using System.Reflection;
using Common.API;
using Common.API.Extenisions;
using Common.Infrastructure;
using Common.Infrastructure.Migrator;
using Identity.Application;
using Identity.Infrastructure;
using Identity.Infrastructure.Persistance.Context;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerDocumentation(Assembly.GetExecutingAssembly().GetName().Name!);
builder.Services.AddControllers();
builder.Services.AddIdentityApplication();
builder.Services.AddIdentityInfrastructure(builder.Configuration);
builder.Services.AddCorsPolicy(builder.Configuration);
builder.Services.AddApiAuthentication(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseDbMigrator<IdentityDbContext>();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.MapControllers();
app.UseCorsPolicy();
app.UseAuthentication();
app.UseAuthorization();

app.Run();