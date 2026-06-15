using System.Net;
using System.Text.Json;

namespace Officing_API.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }

        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;// HTTP 500 by default
        var result = exception.Message;
        //mapping certain exceptions to http codes
        if (exception is KeyNotFoundException)
        {
            code = HttpStatusCode.NotFound;//404
        }
        else if (exception is ApplicationException)
        {
            code = HttpStatusCode.BadRequest;//400 (Breaking business rule)
        }
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        var responsePayload = JsonSerializer.Serialize(new{error = result});
        return context.Response.WriteAsync(responsePayload);
    }
}