using OnionArch.Application.Models;
using System.Text.Json;

namespace OnionArch.WebAPI.Middlewares;

public class ExceptionHandlingMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            ResponseModel<bool> responseModel = ResponseModel<bool>.Fail(ex.Message);
            context.Response.ContentType = "application/json";
            var serialized = JsonSerializer.Serialize(responseModel);
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsync(serialized);

        }
    }
}
