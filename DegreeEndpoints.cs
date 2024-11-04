using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using VIRTUAL_LAB_API.Data;
using VIRTUAL_LAB_API.Model;
namespace VIRTUAL_LAB_API;

public static class DegreeEndpoints
{
    public static void MapDegreeEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Degree").WithTags(nameof(Degree));

        group.MapGet("/", async (VIRTUAL_LAB_APIContext db) =>
        {
            return await db.Degree.ToListAsync();
        })
        .WithName("GetAllDegrees")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Degree>, NotFound>> (int id, VIRTUAL_LAB_APIContext db) =>
        {
            return await db.Degree.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Degree model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetDegreeById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Degree degree, VIRTUAL_LAB_APIContext db) =>
        {
            var affected = await db.Degree
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.Id, degree.Id)
                    .SetProperty(m => m.AdmissionDate, degree.AdmissionDate)
                    .SetProperty(m => m.GraduationDate, degree.GraduationDate)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateDegree")
        .WithOpenApi();

        group.MapPost("/", async (Degree degree, VIRTUAL_LAB_APIContext db) =>
        {
            db.Degree.Add(degree);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Degree/{degree.Id}",degree);
        })
        .WithName("CreateDegree")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, VIRTUAL_LAB_APIContext db) =>
        {
            var affected = await db.Degree
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteDegree")
        .WithOpenApi();
    }
}
