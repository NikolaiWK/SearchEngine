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
            .WithOrigins("http://localhost:8080") // Porten hvor din Blazor-app kører
            .AllowAnyHeader()
            .AllowAnyMethod());
});

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

// 👇 Brug CORS
app.UseCors("AllowBlazor");

app.UseDefaultFiles();
app.UseStaticFiles();
app.MapFallbackToFile("index.html");



app.MapControllers();

app.Run();
