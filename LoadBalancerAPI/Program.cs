var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHttpClient(); // 👈 vigtigt

var app = builder.Build();

app.MapControllers();
app.Run();
