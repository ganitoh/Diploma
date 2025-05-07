using System.Reflection;
using Common.API;
using Common.API.Extenisions;
using Common.Infrastructure.Migrator;
using Organizaiton.Application;
using Organization.Infrastructure;
using Organization.Infrastructure.Persistance.Context;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSerilog();
builder.Services.AddSwaggerDocumentation(Assembly.GetExecutingAssembly().GetName().Name!);
builder.Services.AddControllers();
builder.Services.AddOrganizaitonApplication();
builder.Services.AddOrganizaitonInfrastructure(builder.Configuration);
builder.Services.AddCorsPolicy(builder.Configuration);
builder.Services.AddApiAuthentication(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseDbMigrator<OrganizationDbContext>();
app.UseHttpsRedirection();
app.MapControllers();
app.UseCorsPolicy();
app.UseAuthentication();
app.UseAuthorization();

app.Run();