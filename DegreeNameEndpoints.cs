using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using VIRTUAL_LAB_API.Data;
using VIRTUAL_LAB_API.Model;
namespace VIRTUAL_LAB_API;

public static class DegreeNameEndpoints
{
    public static void MapDegreeNameEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/DegreeName").WithTags(nameof(DegreeName));

        group.MapGet("/", async (VIRTUAL_LAB_APIContext db) =>
        {
            return await db.DegreeName.ToListAsync();
        })
        .WithName("GetAllDegreeNames")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<DegreeName>, NotFound>> (int id, VIRTUAL_LAB_APIContext db) =>
        {
            return await db.DegreeName.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is DegreeName model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetDegreeNameById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, DegreeName degreeName, VIRTUAL_LAB_APIContext db) =>
        {
            var affected = await db.DegreeName
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.Id, degreeName.Id)
                    .SetProperty(m => m.Name, degreeName.Name)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateDegreeName")
        .WithOpenApi();

        group.MapPost("/", async (DegreeName degreeName, VIRTUAL_LAB_APIContext db) =>
        {
            db.DegreeName.Add(degreeName);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/DegreeName/{degreeName.Id}",degreeName);
        })
        .WithName("CreateDegreeName")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, VIRTUAL_LAB_APIContext db) =>
        {
            var affected = await db.DegreeName
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteDegreeName")
        .WithOpenApi();
    }
}
