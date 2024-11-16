using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using VIRTUAL_LAB_API.Data;
using VIRTUAL_LAB_API.Model;
using Task = VIRTUAL_LAB_API.Model.Task;
using Microsoft.AspNetCore.Mvc;
namespace VIRTUAL_LAB_API;

public static class CourseEndpoints
{
    public static void MapCourseEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Course").WithTags(nameof(Course));

        group.MapGet("/", async ([FromQuery(Name = "teacherId")] int? teacherId,
            [FromQuery(Name = "studentId")] int? studentId,
            VIRTUAL_LAB_APIContext db) =>
        {
            if (teacherId != null)
            {
                return await db.Course
                    .Where(model => model.Teachers.Select(t => t.Id).ToList().Contains((int)teacherId))
                    .ToListAsync();
            }
            else if (studentId != null)
            {
                return await db.Course
                    .Where(model => model.Students.Select(t => t.Id).ToList().Contains((int)studentId))
                    .ToListAsync();
            }

            return await db.Course.ToListAsync();
        })
        .WithName("GetCourses")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Course>, NotFound>> (int id, VIRTUAL_LAB_APIContext db) =>
        {
            return await db.Course.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Course model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetCourseById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Course course,
            [FromQuery(Name = "studentId")] int? studentId,
            VIRTUAL_LAB_APIContext db) =>
        {
            int affected = 0;
            var currentCourse = db.Course.Where(m => m.Id == id).First();
            currentCourse.Students.RemoveAll(s => s.Id == studentId);

            if (studentId != null)
            {
                affected = await db.Course
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.Id, course.Id)
                    .SetProperty(m => m.Name, course.Name)
                    .SetProperty(m => m.Description, course.Description)
                    .SetProperty(m => m.Students, currentCourse.Students)
                );
            }
            else
            {
                affected = await db.Course
                  .Where(model => model.Id == id)
                  .ExecuteUpdateAsync(setters => setters
                      .SetProperty(m => m.Id, course.Id)
                      .SetProperty(m => m.Name, course.Name)
                      .SetProperty(m => m.Description, course.Description)
                      );
            }



            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateCourse")
        .WithOpenApi();

        group.MapPost("/", async (Course course, [FromQuery(Name = "teacherId")] int? teacherId, [FromQuery(Name = "studentId")] int? studentId, VIRTUAL_LAB_APIContext db) =>
        {
            if (teacherId != null)
            {
                var teacherToLink = db.Teacher.Where(t => t.Id == teacherId).FirstOrDefault();

                if (teacherToLink != null)
                    course.Teachers.Add(teacherToLink);
            }

            if (studentId != null)
            {
                var studentToLink = db.Student.Where(t => t.Id == studentId).FirstOrDefault();

                if (studentToLink != null)
                    course.Students.Add(studentToLink);
            }

            db.Course.Add(course);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Course/{course.Id}", course);
        })
        .WithName("CreateCourse")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, VIRTUAL_LAB_APIContext db) =>
        {
            var affected = await db.Course
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteCourse")
        .WithOpenApi();
    }
}
