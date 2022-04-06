using DotnetWsRef.Api.GraphQL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services
    .AddGraphQLServer()
    .AddDocumentFromFile("./graphql/schema.graphql")
    .BindRuntimeType<Query>()
    .BindRuntimeType<Mutation>();
DotnetWsRef.Application.Config.RegisterDependencies(builder.Services);
DotnetWsRef.Infra.Config.RegisterDependencies(builder.Services, builder.Configuration["POSTGRES_CONNECTION_STRING"]);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();

app.MapControllers();
app.MapGraphQL();

app.Run();