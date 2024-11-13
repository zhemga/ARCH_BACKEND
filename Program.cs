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
using Newtonsoft.Json;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<VIRTUAL_LAB_APIContext>(
options =>
{
    //options.UseLazyLoadingProxies();
    options.UseSqlServer(builder.Configuration.GetConnectionString("VIRTUAL_LAB_APIContext") ?? throw new InvalidOperationException("Connection string 'VIRTUAL_LAB_APIContext' not found."));
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

// SEEDING
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<VIRTUAL_LAB_APIContext>();

    dbContext.Database.EnsureDeleted();
    dbContext.Database.EnsureCreated();

    {

        var items = new List<UserRole>()
                {
                    new UserRole { Name = "Administrator" },
                    new UserRole { Name = "Teacher" },
                    new UserRole { Name = "Student" },
                };

        dbContext.AddRange(items);
        dbContext.SaveChanges();
    }

    {
        var items = new AutoFaker<Administrator>()
            .RuleFor(i => i.Id, 0)
            .Generate(3);
        var role = dbContext.UserRole.Where(x => x.Name == "Administrator").First();
        items.ForEach(i => i.UserRoleId = role.Id);
        items.ForEach(i => i.UserRole = role);

        dbContext.AddRange(items);
        dbContext.SaveChanges();
    }

    {
        var itemsPrepare = new AutoFaker<User>()
            .RuleFor(i => i.Id, 0)
            .Generate(5);

        var items = new List<Teacher>();

        itemsPrepare.ForEach(i =>
        {
            items.Add(new Teacher
            {
                Id = i.Id,
                Name = i.Name,
                Surname = i.Surname,
                MiddleName = i.MiddleName,
                Email = i.Email,
                Phone = i.Phone,
                BirthDate = i.BirthDate,
                HashPassword = i.HashPassword,
            });
        });

        var role = dbContext.UserRole.Where(x => x.Name == "Teacher").First();
        items.ForEach(i => i.UserRoleId = role.Id);
        items.ForEach(i => i.UserRole = role);

        dbContext.AddRange(items);
        dbContext.SaveChanges();
    }


    {
        var itemsPrepare = new AutoFaker<User>()
            .RuleFor(i => i.Id, 0)
            .Generate(15);

        var items = new List<Student>();

        itemsPrepare.ForEach(i =>
        {
            items.Add(new Student
            {
                Id = i.Id,
                Name = i.Name,
                Surname = i.Surname,
                MiddleName = i.MiddleName,
                Email = i.Email,
                Phone = i.Phone,
                BirthDate = i.BirthDate,
                HashPassword = i.HashPassword,
            });
        });

        var role = dbContext.UserRole.Where(x => x.Name == "Student").First();
        items.ForEach(i => i.UserRoleId = role.Id);
        items.ForEach(i => i.UserRole = role);

        dbContext.AddRange(items);
        dbContext.SaveChanges();
    }

    {
        var students = dbContext.Student.ToList();
        var teachers = dbContext.Teacher.ToList();

        var items = new Faker<Course>()
            .RuleFor(i => i.Id, 0)
            .RuleFor(i => i.Name, f => f.Lorem.Word())
            .RuleFor(i => i.Description, f => f.Lorem.Text())
            .RuleFor(i => i.Teachers, teachers)
            .RuleFor(i => i.Students, students)
            .Generate(4);

        dbContext.AddRange(items);
        dbContext.SaveChanges();
    }

    //{
    //    var courses = dbContext.Course.ToList();
    //    var students = dbContext.Student.ToList();
    //    var teachers = dbContext.Teacher.ToList();
    //    var items = new Faker<VIRTUAL_LAB_API.Model.EducationalMaterial>()
    //        .RuleFor(i => i.Id, 0)
    //        .RuleFor(i => i.CourseId, f => f.PickRandom(courses).Id)
    //        .Generate(3);

    //    dbContext.AddRange(items);
    //    dbContext.SaveChanges();
    //}

    //{
    //    var courses = dbContext.Course.ToList();
    //    var students = dbContext.Student.ToList();
    //    var teachers = dbContext.Teacher.ToList();
    //    var items = new AutoFaker<VIRTUAL_LAB_API.Model.Task>()
    //        .RuleFor(i => i.Id, 0)
    //        .RuleFor(i => i.CourseId, f => f.PickRandom(courses).Id)
    //        .Generate(3);

    //    dbContext.AddRange(items);
    //    dbContext.SaveChanges();
    //}



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