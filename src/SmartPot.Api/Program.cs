using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SmartPot.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// ── Database ──────────────────────────────────────────────────
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// ── SignalR ───────────────────────────────────────────────────
builder.Services.AddSignalR();

// ── API ───────────────────────────────────────────────────────
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title       = "GrowPots — SmartPot API",
        Version     = "v1",
        Description = "REST API to receive sensor data from ESP32 devices and manage automated plant care.",
        Contact     = new OpenApiContact { Name = "Luis Francisco Valdes", Email = "vsluisfrancisco@gmail.com" }
    });
    // Include XML comments if they exist
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath)) c.IncludeXmlComments(xmlPath);
});

// ── CORS (allow any origin in dev, restrict in production) ────
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

// ── Auto-migrate in Development ───────────────────────────────
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    try
    {
        db.Database.Migrate();
        logger.LogInformation("✅ Database migrations applied successfully.");
    }
    catch (Exception ex)
    {
        logger.LogWarning("⚠️  Could not apply migrations: {Message}. Is the database running?", ex.Message);
    }
}

// ── Middleware Pipeline ───────────────────────────────────────
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SmartPot API v1");
        c.RoutePrefix = string.Empty; // Swagger at root: http://localhost:5000/
    });
}

app.UseCors();
app.UseAuthorization();
app.MapControllers();

app.Run();

