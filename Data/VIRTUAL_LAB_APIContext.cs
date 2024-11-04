using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VIRTUAL_LAB_API.Model;

namespace VIRTUAL_LAB_API.Data
{
    public class VIRTUAL_LAB_APIContext : DbContext
    {
        public VIRTUAL_LAB_APIContext (DbContextOptions<VIRTUAL_LAB_APIContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        public DbSet<VIRTUAL_LAB_API.Model.Administrator> Administrator { get; set; } = default!;
        public DbSet<VIRTUAL_LAB_API.Model.Course> Course { get; set; } = default!;
        public DbSet<VIRTUAL_LAB_API.Model.Degree> Degree { get; set; } = default!;
        public DbSet<VIRTUAL_LAB_API.Model.DegreeName> DegreeName { get; set; } = default!;
        public DbSet<VIRTUAL_LAB_API.Model.EducationalMaterial> EducationalMaterial { get; set; } = default!;
        public DbSet<VIRTUAL_LAB_API.Model.Specialty> Specialty { get; set; } = default!;
        public DbSet<VIRTUAL_LAB_API.Model.Student> Student { get; set; } = default!;
        public DbSet<VIRTUAL_LAB_API.Model.StudentCourseStatistic> StudentCourseStatistic { get; set; } = default!;
        public DbSet<VIRTUAL_LAB_API.Model.StudentTaskAttempt> StudentTaskAttempt { get; set; } = default!;
        public DbSet<VIRTUAL_LAB_API.Model.StudentTaskStatistic> StudentTaskStatistic { get; set; } = default!;
        public DbSet<VIRTUAL_LAB_API.Model.Task> Task { get; set; } = default!;
        public DbSet<VIRTUAL_LAB_API.Model.Teacher> Teacher { get; set; } = default!;
        public DbSet<VIRTUAL_LAB_API.Model.User> User { get; set; } = default!;
        public DbSet<VIRTUAL_LAB_API.Model.UserRole> UserRole { get; set; } = default!;
        public DbSet<VIRTUAL_LAB_API.Model.StudentDegree> StudentDegree { get; set; } = default!;
        public DbSet<VIRTUAL_LAB_API.Model.TeacherDegree> TeacherDegree { get; set; } = default!;

    }
}
