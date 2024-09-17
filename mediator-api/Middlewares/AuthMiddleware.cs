using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

public class AuthMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;

    public AuthMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
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
        validApiKey = configuration.GetValue<string>("Authentication:ApiKey");
        if (validApiKey is null || !ValidApiKey.Equals(extractedApiKey))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Authentication failed");
            return;
       ​ }
       await _next(context)
    }
}
