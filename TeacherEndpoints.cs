using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using VIRTUAL_LAB_API.Data;
using VIRTUAL_LAB_API.Model;
namespace VIRTUAL_LAB_API;

public static class TeacherEndpoints
{
    public static void MapTeacherEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Teacher").WithTags(nameof(Teacher));

        group.MapGet("/", async (VIRTUAL_LAB_APIContext db) =>
        {
            return await db.Teacher.ToListAsync();
        })
        .WithName("GetAllTeachers")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Teacher>, NotFound>> (int id, VIRTUAL_LAB_APIContext db) =>
        {
            return await db.Teacher.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Teacher model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetTeacherById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Teacher teacher, VIRTUAL_LAB_APIContext db) =>
        {
            var affected = await db.Teacher
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.Id, teacher.Id)
                    .SetProperty(m => m.Name, teacher.Name)
                    .SetProperty(m => m.MiddleName, teacher.MiddleName)
                    .SetProperty(m => m.Surname, teacher.Surname)
                    .SetProperty(m => m.Email, teacher.Email)
                    .SetProperty(m => m.Phone, teacher.Phone)
                    .SetProperty(m => m.HashPassword, teacher.HashPassword)
                    .SetProperty(m => m.BirthDate, teacher.BirthDate)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateTeacher")
        .WithOpenApi();

        group.MapPost("/", async (Teacher teacher, VIRTUAL_LAB_APIContext db) =>
        {
            db.Teacher.Add(teacher);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Teacher/{teacher.Id}",teacher);
        })
        .WithName("CreateTeacher")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, VIRTUAL_LAB_APIContext db) =>
        {
            var affected = await db.Teacher
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteTeacher")
        .WithOpenApi();
    }
}
