using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using VIRTUAL_LAB_API.Data;
using VIRTUAL_LAB_API.Model;
namespace VIRTUAL_LAB_API;

public static class StudentDegreeEndpoints
{
    public static void MapStudentDegreeEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/StudentDegree").WithTags(nameof(StudentDegree));

        group.MapGet("/", async (VIRTUAL_LAB_APIContext db) =>
        {
            return await db.StudentDegree.ToListAsync();
        })
        .WithName("GetAllStudentDegrees")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<StudentDegree>, NotFound>> (int id, VIRTUAL_LAB_APIContext db) =>
        {
            return await db.StudentDegree.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is StudentDegree model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetStudentDegreeById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, StudentDegree studentDegree, VIRTUAL_LAB_APIContext db) =>
        {
            var affected = await db.StudentDegree
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.Id, studentDegree.Id)
                    .SetProperty(m => m.AdmissionDate, studentDegree.AdmissionDate)
                    .SetProperty(m => m.GraduationDate, studentDegree.GraduationDate)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateStudentDegree")
        .WithOpenApi();

        group.MapPost("/", async (StudentDegree studentDegree, VIRTUAL_LAB_APIContext db) =>
        {
            db.StudentDegree.Add(studentDegree);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/StudentDegree/{studentDegree.Id}",studentDegree);
        })
        .WithName("CreateStudentDegree")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, VIRTUAL_LAB_APIContext db) =>
        {
            var affected = await db.StudentDegree
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteStudentDegree")
        .WithOpenApi();
    }
}
