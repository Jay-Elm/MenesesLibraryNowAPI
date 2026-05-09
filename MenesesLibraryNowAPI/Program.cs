var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Urls.Add($"http://+:{port}");

// Swagger
app.UseSwagger();
app.UseSwaggerUI();

// Only redirect HTTPS locally
if (!app.Environment.IsProduction())
{
    app.UseHttpsRedirection();
}

app.MapControllers();

// Optional homepage
app.MapGet("/", () => "MenesesLibraryNowAPI is running!");

app.Run();
