using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VIRTUAL_LAB_API.Data;
using VIRTUAL_LAB_API;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<VIRTUAL_LAB_APIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("VIRTUAL_LAB_APIContext") ?? throw new InvalidOperationException("Connection string 'VIRTUAL_LAB_APIContext' not found.")));

// Add services to the container.
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

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapAdministratorEndpoints();

app.MapCourseEndpoints();

app.MapDegreeEndpoints();

app.MapDegreeNameEndpoints();

app.MapEducationalMaterialEndpoints();

app.MapSpecialtyEndpoints();

app.MapStudentEndpoints();

app.MapStudentCourseStatisticEndpoints();

app.MapStudentTaskAttemptEndpoints();

app.MapStudentTaskStatisticEndpoints();

app.MapTaskEndpoints();

app.MapTeacherEndpoints();

app.MapUserEndpoints();

app.MapUserRoleEndpoints();

app.MapStudentDegreeEndpoints();

app.MapTeacherDegreeEndpoints();

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
