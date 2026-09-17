using System.Net;
using System.Text.Json;
using OnlineStoreAPI.Exceptions;
namespace OnlineStoreAPI.Middleware;
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    public ExceptionMiddleware(RequestDelegate next)
    {
        _next=next;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch(Exception ex)
        {
            await HandleExceptionAsync(context,ex);
        }
    }
    private static async Task HandleExceptionAsync(HttpContext context,Exception ex)
    {
        int statusCode=ex switch
        {
            NotFoundException=>StatusCodes.Status404NotFound,
            ConflictException=>StatusCodes.Status409Conflict,
            BadRequestException=>StatusCodes.Status400BadRequest,
            _=>StatusCodes.Status500InternalServerError
        };
        context.Response.StatusCode=statusCode;
        context.Response.ContentType="application/json";
        var response = new
        {
            statusCode=statusCode,
            message=ex.Message
        };
        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }

}