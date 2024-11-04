using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using VIRTUAL_LAB_API.Data;
using VIRTUAL_LAB_API.Model;
namespace VIRTUAL_LAB_API;

public static class SpecialtyEndpoints
{
    public static void MapSpecialtyEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Specialty").WithTags(nameof(Specialty));

        group.MapGet("/", async (VIRTUAL_LAB_APIContext db) =>
        {
            return await db.Specialty.ToListAsync();
        })
        .WithName("GetAllSpecialties")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Specialty>, NotFound>> (int id, VIRTUAL_LAB_APIContext db) =>
        {
            return await db.Specialty.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Specialty model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetSpecialtyById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Specialty specialty, VIRTUAL_LAB_APIContext db) =>
        {
            var affected = await db.Specialty
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.Id, specialty.Id)
                    .SetProperty(m => m.Name, specialty.Name)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateSpecialty")
        .WithOpenApi();

        group.MapPost("/", async (Specialty specialty, VIRTUAL_LAB_APIContext db) =>
        {
            db.Specialty.Add(specialty);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Specialty/{specialty.Id}",specialty);
        })
        .WithName("CreateSpecialty")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, VIRTUAL_LAB_APIContext db) =>
        {
            var affected = await db.Specialty
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteSpecialty")
        .WithOpenApi();
    }
}
