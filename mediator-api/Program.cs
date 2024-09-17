using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using webapi_docker;
using webapi_docker.Interfaces;
using webapi_docker.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
{
// Configurar el esquema de seguridad de Swagger
    c.AddSecurityDefinition("ApiKey", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Name = "api_key",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "API Key Authentication"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "ApiKey"
                }
            },
            new List<string>()
        }
    });
   c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProductAPI", Version = "v1" });
   // Custom : Configurar los comentarios XML en swagger
   //Ruta al archivo XML de documentaci�n generado
   var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
   var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
   c.IncludeXmlComments(xmlPath);
});

// Custom : Dependecy injection
builder.Services.AddInjection(builder.Configuration);
builder.Services.AddScoped<IApiDbContext, ApiDbContext>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(conf =>
    {
        conf.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

using var scope = app.Services.CreateScope();

var dbContext = scope.ServiceProvider.GetRequiredService<ApiDbContext>();

dbContext.Database.EnsureCreated();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseMiddleware<AuthMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.UseCors();

app.Run();
