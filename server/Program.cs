var builder = WebApplication.CreateBuilder(args);

// 1. Define a CORS policy name
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// 2. Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:5173") // Vite UI default port
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});

builder.Services.AddControllers();
var app = builder.Build();

// 3. Enable CORS middleware before Authorization
app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();
app.MapControllers();

// Temporary endpoint to test connectivity
app.MapGet("/api/health", () => Results.Ok(new
{
    status = "ok",
    data = "Pick Up Your Socks API"
}));

app.Run();