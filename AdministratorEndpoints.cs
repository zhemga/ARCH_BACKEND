﻿using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using VIRTUAL_LAB_API.Data;
using VIRTUAL_LAB_API.Model;
namespace VIRTUAL_LAB_API;

public static class AdministratorEndpoints
{
    public static void MapAdministratorEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Administrator").WithTags(nameof(Administrator));

        group.MapGet("/", async (VIRTUAL_LAB_APIContext db) =>
        {
            return await db.Administrator.ToListAsync();
        })
        .WithName("GetAllAdministrators")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Administrator>, NotFound>> (int id, VIRTUAL_LAB_APIContext db) =>
        {
            return await db.Administrator.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Administrator model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetAdministratorById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Administrator administrator, VIRTUAL_LAB_APIContext db) =>
        {
            var affected = await db.Administrator
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.Id, administrator.Id)
                    .SetProperty(m => m.Name, administrator.Name)
                    .SetProperty(m => m.MiddleName, administrator.MiddleName)
                    .SetProperty(m => m.Surname, administrator.Surname)
                    .SetProperty(m => m.Email, administrator.Email)
                    .SetProperty(m => m.Phone, administrator.Phone)
                    .SetProperty(m => m.HashPassword, administrator.HashPassword)
                    .SetProperty(m => m.BirthDate, administrator.BirthDate)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateAdministrator")
        .WithOpenApi();

        group.MapPost("/", async (Administrator administrator, VIRTUAL_LAB_APIContext db) =>
        {
            db.Administrator.Add(administrator);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Administrator/{administrator.Id}",administrator);
        })
        .WithName("CreateAdministrator")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, VIRTUAL_LAB_APIContext db) =>
        {
            var affected = await db.Administrator
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteAdministrator")
        .WithOpenApi();
    }
}
