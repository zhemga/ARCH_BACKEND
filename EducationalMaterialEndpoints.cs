using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using VIRTUAL_LAB_API.Data;
using VIRTUAL_LAB_API.Model;
namespace VIRTUAL_LAB_API;

public static class EducationalMaterialEndpoints
{
    public static void MapEducationalMaterialEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/EducationalMaterial").WithTags(nameof(EducationalMaterial));

        group.MapGet("/", async (VIRTUAL_LAB_APIContext db) =>
        {
            return await db.EducationalMaterial.ToListAsync();
        })
        .WithName("GetAllEducationalMaterials")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<EducationalMaterial>, NotFound>> (int id, VIRTUAL_LAB_APIContext db) =>
        {
            return await db.EducationalMaterial.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is EducationalMaterial model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetEducationalMaterialById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, EducationalMaterial educationalMaterial, VIRTUAL_LAB_APIContext db) =>
        {
            var affected = await db.EducationalMaterial
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.Id, educationalMaterial.Id)
                    .SetProperty(m => m.Name, educationalMaterial.Name)
                    .SetProperty(m => m.Description, educationalMaterial.Description)
                    .SetProperty(m => m.CloudDriveAttachedFileURLs, educationalMaterial.CloudDriveAttachedFileURLs)
                    .SetProperty(m => m.CourseId, educationalMaterial.CourseId)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateEducationalMaterial")
        .WithOpenApi();

        group.MapPost("/", async (EducationalMaterial educationalMaterial, VIRTUAL_LAB_APIContext db) =>
        {
            db.EducationalMaterial.Add(educationalMaterial);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/EducationalMaterial/{educationalMaterial.Id}",educationalMaterial);
        })
        .WithName("CreateEducationalMaterial")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, VIRTUAL_LAB_APIContext db) =>
        {
            var affected = await db.EducationalMaterial
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteEducationalMaterial")
        .WithOpenApi();
    }
}
