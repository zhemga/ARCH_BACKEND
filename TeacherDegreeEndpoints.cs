using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using VIRTUAL_LAB_API.Data;
using VIRTUAL_LAB_API.Model;
namespace VIRTUAL_LAB_API;

public static class TeacherDegreeEndpoints
{
    public static void MapTeacherDegreeEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/TeacherDegree").WithTags(nameof(TeacherDegree));

        group.MapGet("/", async (VIRTUAL_LAB_APIContext db) =>
        {
            return await db.TeacherDegree.ToListAsync();
        })
        .WithName("GetAllTeacherDegrees")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<TeacherDegree>, NotFound>> (int id, VIRTUAL_LAB_APIContext db) =>
        {
            return await db.TeacherDegree.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is TeacherDegree model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetTeacherDegreeById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, TeacherDegree teacherDegree, VIRTUAL_LAB_APIContext db) =>
        {
            var affected = await db.TeacherDegree
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.Id, teacherDegree.Id)
                    .SetProperty(m => m.AdmissionDate, teacherDegree.AdmissionDate)
                    .SetProperty(m => m.GraduationDate, teacherDegree.GraduationDate)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateTeacherDegree")
        .WithOpenApi();

        group.MapPost("/", async (TeacherDegree teacherDegree, VIRTUAL_LAB_APIContext db) =>
        {
            db.TeacherDegree.Add(teacherDegree);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/TeacherDegree/{teacherDegree.Id}",teacherDegree);
        })
        .WithName("CreateTeacherDegree")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, VIRTUAL_LAB_APIContext db) =>
        {
            var affected = await db.TeacherDegree
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteTeacherDegree")
        .WithOpenApi();
    }
}
