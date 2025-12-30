namespace Pustok.Mvc;

public class LogMiddleware
{
    public LogMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public RequestDelegate next { get; set; }
    public async Task InvokeAsync(HttpContext context)
    {
        // Log the request
        Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path}");
        // Call the next middleware in the pipeline
        await next(context);
        // Log the response
        Console.WriteLine($"Response: {context.Response.StatusCode}");
    }
}
