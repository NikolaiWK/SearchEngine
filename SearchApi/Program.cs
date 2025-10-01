using ConsoleSearch;

var builder = WebApplication.CreateBuilder(args);

// Tilføj SearchLogic med dependency injection
builder.Services.AddScoped<SearchLogic>();
builder.Services.AddScoped<IDatabase, DatabaseSqlite>();



// Tilføj controllers
builder.Services.AddControllers();

// 👇 Tilføj CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazor",
        policy => policy
            .WithOrigins("https://localhost:5169") // Porten hvor din Blazor-app kører
            .AllowAnyHeader()
            .AllowAnyMethod());
});

// Tilføj Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();
app.MapFallbackToFile("index.html");

// Swagger middleware
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Search API v1");
    c.RoutePrefix = string.Empty; // Swagger vises på http://localhost:5000/
});

// 👇 Brug CORS
app.UseCors("AllowBlazor");

app.MapControllers();

app.Run();
