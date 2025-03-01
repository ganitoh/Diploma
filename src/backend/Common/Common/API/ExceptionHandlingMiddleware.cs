using System.Net;
using System.Text.Json;
using Common.API;
using Common.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Common.API;

public class ExceptionHandlingMiddleware
{

    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var response = context.Response;

        response.ContentType = "application/json";
        var responseModel = ApiResponse<string>.Fail(exception.Message);
        
        response.StatusCode = exception switch
        {
            ApplicationException e => (int) HttpStatusCode.InternalServerError,
            ValidationException e => (int) HttpStatusCode.BadRequest,
            _ => (int) HttpStatusCode.InternalServerError
        };
        
        if (exception is ValidationException validationException)
        {
            responseModel.Response = JsonSerializer.Serialize(validationException.ErrorsDictionary);
        }
        
        var result = JsonSerializer.Serialize(responseModel, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
        
        await response.WriteAsync(result);
    }
}