using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

public class AuthMiddleware
{
    private readonly RequestDelegate _next;
    private const string _validApiKey;

    public AuthMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _validApiKey = configuration.GetValue<string>("Authentication:ApiKey");
    }

    public async Task InvokeAsync(HttpContext context)
    {
        const string ApiKeyHeaderName = "api_key";
        if (!context.Request.Headers.TryGetValue(ApiKeyHeaderName, out var extractedApiKey))
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync("Missing authentication");
            return;
        }
        if (_validApiKey is null || !ValidApiKey.Equals(extractedApiKey))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Authentication failed");
            return;
       ​ }
       _validApiKey = configuration.GetValue<string>("Authentication:ApiKey");
       await _next(context)
    }
}
