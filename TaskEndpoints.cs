using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using VIRTUAL_LAB_API.Data;
using VIRTUAL_LAB_API.Model;
using Task = VIRTUAL_LAB_API.Model.Task;
using Microsoft.AspNetCore.Mvc;
using Bogus.DataSets;
namespace VIRTUAL_LAB_API;

public static class TaskEndpoints
{
    public static void MapTaskEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Task").WithTags(nameof(Task));

        group.MapGet("/",
            async ([FromQuery(Name = "courseId")] int? courseId, VIRTUAL_LAB_APIContext db) =>
        {
            if (courseId != null)
            {
                return await db.Task
                   .Where(model => model.CourseId == courseId)
                   .ToListAsync();
            }

            return await db.Task.ToListAsync();
        })
        .WithName("GetTasks")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Task>, NotFound>> (int id, VIRTUAL_LAB_APIContext db) =>
        {
            return await db.Task.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Task model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetTaskById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Task task, VIRTUAL_LAB_APIContext db) =>
        {
            var affected = await db.Task
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.Id, task.Id)
                    .SetProperty(m => m.Name, task.Name)
                    .SetProperty(m => m.Description, task.Description)
                    .SetProperty(m => m.DataJSON, task.DataJSON)
                    .SetProperty(m => m.MaxAttempts, task.MaxAttempts)
                    .SetProperty(m => m.CourseId, task.CourseId)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateTask")
        .WithOpenApi();

        group.MapPost("/", async (Task task, [FromQuery(Name = "courseId")] int? courseId, VIRTUAL_LAB_APIContext db) =>
        {
            if (courseId != null)
            {
                task.CourseId = (int) courseId;
            }

            db.Task.Add(task);

            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Task/{task.Id}", task);
        })
        .WithName("CreateTask")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, VIRTUAL_LAB_APIContext db) =>
        {
            var affected = await db.Task
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteTask")
        .WithOpenApi();
    }
}
