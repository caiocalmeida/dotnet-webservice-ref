using DotnetWsRef.Api.GraphQL;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services
    .AddGraphQLServer()
    .AddDocumentFromFile("./graphql/schema.graphql")
    .BindRuntimeType<Query>()
    .BindRuntimeType<Mutation>();

// Used to access request headers on graphql resolvers
builder.Services.AddHttpContextAccessor();

DotnetWsRef.Application.Config.RegisterDependencies(builder.Services);
DotnetWsRef.Infra.Config.RegisterDependencies(builder.Services, builder.Configuration["POSTGRES_CONNECTION_STRING"]);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("API Key", new OpenApiSecurityScheme
    {
        Name = "X-API-KEY",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "apiKey"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "API Key"
                },
                Scheme = "apiKey",
                Name = "X-API-KEY",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapGraphQL();

app.Run();