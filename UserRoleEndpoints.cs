using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using VIRTUAL_LAB_API.Data;
using VIRTUAL_LAB_API.Model;
namespace VIRTUAL_LAB_API;

public static class UserRoleEndpoints
{
    public static void MapUserRoleEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/UserRole").WithTags(nameof(UserRole));

        group.MapGet("/", async (VIRTUAL_LAB_APIContext db) =>
        {
            return await db.UserRole.ToListAsync();
        })
        .WithName("GetAllUserRoles")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<UserRole>, NotFound>> (int id, VIRTUAL_LAB_APIContext db) =>
        {
            return await db.UserRole.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is UserRole model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetUserRoleById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, UserRole userRole, VIRTUAL_LAB_APIContext db) =>
        {
            var affected = await db.UserRole
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.Id, userRole.Id)
                    .SetProperty(m => m.Name, userRole.Name)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateUserRole")
        .WithOpenApi();

        group.MapPost("/", async (UserRole userRole, VIRTUAL_LAB_APIContext db) =>
        {
            db.UserRole.Add(userRole);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/UserRole/{userRole.Id}",userRole);
        })
        .WithName("CreateUserRole")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, VIRTUAL_LAB_APIContext db) =>
        {
            var affected = await db.UserRole
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteUserRole")
        .WithOpenApi();
    }
}
