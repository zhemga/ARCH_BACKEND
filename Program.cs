using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VIRTUAL_LAB_API.Data;
using VIRTUAL_LAB_API;
using VIRTUAL_LAB_API.Model;
using System.Text.Json.Serialization;
using System.Text.Json;
using AutoBogus;
using System;
using Microsoft.AspNetCore.Http.Json;
using Bogus.DataSets;
using Bogus;
using System.Diagnostics.Metrics;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<VIRTUAL_LAB_APIContext>(
    options =>
    {
        options.UseLazyLoadingProxies();
        options.UseSqlServer(builder.Configuration.GetConnectionString("VIRTUAL_LAB_APIContext") ?? throw new InvalidOperationException("Connection string 'VIRTUAL_LAB_APIContext' not found."));
    });

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});


var app = builder.Build();

// SEEDING
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<VIRTUAL_LAB_APIContext>();

    dbContext.Database.EnsureDeletedAsync().Wait();
    dbContext.Database.EnsureCreatedAsync().Wait();

    {

        var items = new List<UserRole>()
                {
                    new UserRole { Name = "Administrator" },
                    new UserRole { Name = "Teacher" },
                    new UserRole { Name = "Student" },
                };

        dbContext.AddRange(items);
        dbContext.SaveChangesAsync().Wait();
    }

    {
        var items = new AutoFaker<User>()
            .RuleFor(i => i.Id, 0)
            .Generate(3);
        var role = dbContext.UserRole.Where(x => x.Name == "Administrator").First();
        items.ForEach(i => i.UserRoleId = role.Id);
        items.ForEach(i => i.UserRole = role);

        dbContext.AddRange(items);
        dbContext.SaveChangesAsync().Wait();
    }

}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapAdministratorEndpoints();

app.MapCourseEndpoints();

app.MapDegreeEndpoints();

app.MapDegreeNameEndpoints();

app.MapSpecialtyEndpoints();

app.MapStudentEndpoints();

app.MapStudentCourseStatisticEndpoints();

app.MapStudentTaskStatisticEndpoints();

app.MapTeacherEndpoints();

app.MapStudentDegreeEndpoints();

app.MapTeacherDegreeEndpoints();

app.MapEducationalMaterialEndpoints();

app.MapStudentTaskAttemptEndpoints();

app.MapTaskEndpoints();

app.MapUserRoleEndpoints();

app.MapUserEndpoints();

app.Run();