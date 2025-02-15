using Common.Infrastructure.Migrator;
using Identity.API.Cors;
using Identity.API.MIddlewares;
using Identity.Application;
using Identity.Infrastructure;
using Identity.Infrastructure.Persistance.Context;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSerilog();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddIdentityApplication();
builder.Services.AddIdentityInfrastructure(builder.Configuration);
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
app.UseDbMigrator<IdentityDbContext>();

app.Run();