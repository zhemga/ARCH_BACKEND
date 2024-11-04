using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using VIRTUAL_LAB_API.Data;
using VIRTUAL_LAB_API.Model;
namespace VIRTUAL_LAB_API;

public static class StudentTaskStatisticEndpoints
{
    public static void MapStudentTaskStatisticEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/StudentTaskStatistic").WithTags(nameof(StudentTaskStatistic));

        group.MapGet("/", async (VIRTUAL_LAB_APIContext db) =>
        {
            return await db.StudentTaskStatistic.ToListAsync();
        })
        .WithName("GetAllStudentTaskStatistics")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<StudentTaskStatistic>, NotFound>> (int id, VIRTUAL_LAB_APIContext db) =>
        {
            return await db.StudentTaskStatistic.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is StudentTaskStatistic model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetStudentTaskStatisticById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, StudentTaskStatistic studentTaskStatistic, VIRTUAL_LAB_APIContext db) =>
        {
            var affected = await db.StudentTaskStatistic
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.Id, studentTaskStatistic.Id)
                    .SetProperty(m => m.MarkRate, studentTaskStatistic.MarkRate)
                    .SetProperty(m => m.TimeRate, studentTaskStatistic.TimeRate)
                    .SetProperty(m => m.GeneralCourseRate, studentTaskStatistic.GeneralCourseRate)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateStudentTaskStatistic")
        .WithOpenApi();

        group.MapPost("/", async (StudentTaskStatistic studentTaskStatistic, VIRTUAL_LAB_APIContext db) =>
        {
            db.StudentTaskStatistic.Add(studentTaskStatistic);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/StudentTaskStatistic/{studentTaskStatistic.Id}",studentTaskStatistic);
        })
        .WithName("CreateStudentTaskStatistic")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, VIRTUAL_LAB_APIContext db) =>
        {
            var affected = await db.StudentTaskStatistic
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteStudentTaskStatistic")
        .WithOpenApi();
    }
}
