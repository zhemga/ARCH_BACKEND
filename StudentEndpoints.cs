using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using VIRTUAL_LAB_API.Data;
using VIRTUAL_LAB_API.Model;
namespace VIRTUAL_LAB_API;

public static class StudentEndpoints
{
    public static void MapStudentEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Student").WithTags(nameof(Student));

        group.MapGet("/", async (VIRTUAL_LAB_APIContext db) =>
        {
            return await db.Student.ToListAsync();
        })
        .WithName("GetAllStudents")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Student>, NotFound>> (int id, VIRTUAL_LAB_APIContext db) =>
        {
            return await db.Student.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Student model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetStudentById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Student student, VIRTUAL_LAB_APIContext db) =>
        {
            var affected = await db.Student
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.Id, student.Id)
                    .SetProperty(m => m.Name, student.Name)
                    .SetProperty(m => m.MiddleName, student.MiddleName)
                    .SetProperty(m => m.Surname, student.Surname)
                    .SetProperty(m => m.Email, student.Email)
                    .SetProperty(m => m.Phone, student.Phone)
                    .SetProperty(m => m.HashPassword, student.HashPassword)
                    .SetProperty(m => m.BirthDate, student.BirthDate)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateStudent")
        .WithOpenApi();

        group.MapPost("/", async (Student student, VIRTUAL_LAB_APIContext db) =>
        {
            db.Student.Add(student);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Student/{student.Id}",student);
        })
        .WithName("CreateStudent")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, VIRTUAL_LAB_APIContext db) =>
        {
            var affected = await db.Student
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteStudent")
        .WithOpenApi();
    }
}
