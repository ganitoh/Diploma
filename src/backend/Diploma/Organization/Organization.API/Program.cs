using Common.API;
using Common.Infrastructure.Migrator;
using Organization.API.Cors;
using Organization.Application;
using Organization.Infrastructure;
using Organization.Infrastructure.Persistance.Context;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSerilog();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddOrganizationApplication();
builder.Services.AddOrganizationInfrastructure(builder.Configuration);
builder.Services.AddCorsPolicy(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.MapControllers();
app.UseCorsPolicy();
app.UseDbMigrator<OrganizationDbContext>();

app.Run();