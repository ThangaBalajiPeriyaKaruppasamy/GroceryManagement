using GroceryManagement.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddSingleton<IGroceryService, InMemoryGroceryService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable Swagger UI
if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "GroceryManagement API v1");
        c.RoutePrefix = string.Empty; // serve at root
    });
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
