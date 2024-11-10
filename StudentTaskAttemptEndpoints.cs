using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using VIRTUAL_LAB_API.Data;
using VIRTUAL_LAB_API.Model;
namespace VIRTUAL_LAB_API;

public static class StudentTaskAttemptEndpoints
{
    public static void MapStudentTaskAttemptEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/StudentTaskAttempt").WithTags(nameof(StudentTaskAttempt));

        group.MapGet("/", async (VIRTUAL_LAB_APIContext db) =>
        {
            return await db.StudentTaskAttempt.ToListAsync();
        })
        .WithName("GetAllStudentTaskAttempts")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<StudentTaskAttempt>, NotFound>> (int id, VIRTUAL_LAB_APIContext db) =>
        {
            return await db.StudentTaskAttempt.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is StudentTaskAttempt model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetStudentTaskAttemptById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, StudentTaskAttempt studentTaskAttempt, VIRTUAL_LAB_APIContext db) =>
        {
            var affected = await db.StudentTaskAttempt
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.Id, studentTaskAttempt.Id)
                    .SetProperty(m => m.Number, studentTaskAttempt.Number)
                    .SetProperty(m => m.StudentDataJSON, studentTaskAttempt.StudentDataJSON)
                    .SetProperty(m => m.Rate, studentTaskAttempt.Rate)
                    .SetProperty(m => m.IsSuccessful, studentTaskAttempt.IsSuccessful)
                    .SetProperty(m => m.AttemptDate, studentTaskAttempt.AttemptDate)
                    .SetProperty(m => m.TaskId, studentTaskAttempt.TaskId)
                    .SetProperty(m => m.StudentId, studentTaskAttempt.StudentId)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateStudentTaskAttempt")
        .WithOpenApi();

        group.MapPost("/", async (StudentTaskAttempt studentTaskAttempt, VIRTUAL_LAB_APIContext db) =>
        {
            db.StudentTaskAttempt.Add(studentTaskAttempt);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/StudentTaskAttempt/{studentTaskAttempt.Id}",studentTaskAttempt);
        })
        .WithName("CreateStudentTaskAttempt")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, VIRTUAL_LAB_APIContext db) =>
        {
            var affected = await db.StudentTaskAttempt
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteStudentTaskAttempt")
        .WithOpenApi();
    }
}
