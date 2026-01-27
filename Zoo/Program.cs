using Microsoft.EntityFrameworkCore;
using Zoo.Data;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// DbContext
builder.Services.AddDbContext<ZooContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("ZooDb"),
        sqlOptions => sqlOptions.UseNetTopologySuite()));

// CORS – dozvola za frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Custom services
builder.Services.AddScoped<CostsService>();
builder.Services.AddScoped<WorkerValidationService>();

var app = builder.Build();

// Swagger samo u Developmentu
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// CORS mora biti prije MapControllers
app.UseCors("Frontend");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Seeder – izvršava se pri startu aplikacije
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ZooContext>();
    await DbSeeder.SeedAsync(context);
}

app.Run();
