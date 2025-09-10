using ConsoleSearch;

var builder = WebApplication.CreateBuilder(args);

// Tilføj SearchLogic med dependency injection
builder.Services.AddScoped<SearchLogic>();
builder.Services.AddScoped<IDatabase, DatabaseSqlite>();

// Tilføj controllers
builder.Services.AddControllers();

// Tilføj Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger middleware

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Search API v1");
        c.RoutePrefix = string.Empty; // Swagger vises på http://localhost:5000/
    });


app.MapControllers();

app.Run();
