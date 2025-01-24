/// <summary>
/// Middleware for handling API key authentication
/// </summary>
public class AuthMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Initializes a new instance of the authentication middleware
    /// </summary>
    /// <param name="next">The next middleware in the pipeline</param>
    /// <param name="configuration">The application configuration</param>
    public AuthMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
    }

    /// <summary>
    /// Processes the request through the middleware
    /// </summary>
    /// <param name="context">The HTTP context for the request</param>
    /// <returns>A task representing the asynchronous operation</returns>
    public async Task InvokeAsync(HttpContext context)
    {
        const string ApiKeyHeaderName = "api_key";
        if (!context.Request.Headers.TryGetValue(ApiKeyHeaderName, out var extractedApiKey))
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync("Missing authorization");
            return;
        }
        var validApiKey = _configuration.GetValue<string>("Authorization:ApiKey");
        if (validApiKey is null || !validApiKey.Equals(extractedApiKey))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Authorization failed");
            return;
        }
       await _next(context);
    }
}
