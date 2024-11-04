using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using VIRTUAL_LAB_API.Data;
using VIRTUAL_LAB_API.Model;
namespace VIRTUAL_LAB_API;

public static class StudentCourseStatisticEndpoints
{
    public static void MapStudentCourseStatisticEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/StudentCourseStatistic").WithTags(nameof(StudentCourseStatistic));

        group.MapGet("/", async (VIRTUAL_LAB_APIContext db) =>
        {
            return await db.StudentCourseStatistic.ToListAsync();
        })
        .WithName("GetAllStudentCourseStatistics")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<StudentCourseStatistic>, NotFound>> (int id, VIRTUAL_LAB_APIContext db) =>
        {
            return await db.StudentCourseStatistic.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is StudentCourseStatistic model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetStudentCourseStatisticById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, StudentCourseStatistic studentCourseStatistic, VIRTUAL_LAB_APIContext db) =>
        {
            var affected = await db.StudentCourseStatistic
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.Id, studentCourseStatistic.Id)
                    .SetProperty(m => m.MarkRate, studentCourseStatistic.MarkRate)
                    .SetProperty(m => m.TimeRate, studentCourseStatistic.TimeRate)
                    .SetProperty(m => m.GeneralCourseRate, studentCourseStatistic.GeneralCourseRate)
                    .SetProperty(m => m.CompletionRate, studentCourseStatistic.CompletionRate)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateStudentCourseStatistic")
        .WithOpenApi();

        group.MapPost("/", async (StudentCourseStatistic studentCourseStatistic, VIRTUAL_LAB_APIContext db) =>
        {
            db.StudentCourseStatistic.Add(studentCourseStatistic);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/StudentCourseStatistic/{studentCourseStatistic.Id}",studentCourseStatistic);
        })
        .WithName("CreateStudentCourseStatistic")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, VIRTUAL_LAB_APIContext db) =>
        {
            var affected = await db.StudentCourseStatistic
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteStudentCourseStatistic")
        .WithOpenApi();
    }
}
