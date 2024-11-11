using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using AutoBogus;
using Azure;
using Bogus;
using Microsoft.EntityFrameworkCore;
using VIRTUAL_LAB_API.Model;

namespace VIRTUAL_LAB_API.Data
{
    public class VIRTUAL_LAB_APIContext : DbContext
    {
        public VIRTUAL_LAB_APIContext(DbContextOptions<VIRTUAL_LAB_APIContext> options)
            : base(options)
        {
  
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Administrator>().HasBaseType<User>();
            modelBuilder.Entity<Teacher>().HasBaseType<User>();
            modelBuilder.Entity<Student>().HasBaseType<User>();

            modelBuilder.Entity<StudentDegree>().HasBaseType<Degree>();
            modelBuilder.Entity<TeacherDegree>().HasBaseType<Degree>();

            // Sequences
            modelBuilder.HasSequence<int>($"SQ_{nameof(UserRole)}Numbers").StartsAt(1).IncrementsBy(1);
            modelBuilder.Entity<UserRole>()
                .Property(p => p.Id)
                .HasDefaultValueSql($"NEXT VALUE FOR SQ_{nameof(UserRole)}Numbers");

            modelBuilder.HasSequence<int>($"SQ_{nameof(User)}Numbers").StartsAt(1).IncrementsBy(1);
            modelBuilder.Entity<User>()
                .Property(p => p.Id)
                .HasDefaultValueSql($"NEXT VALUE FOR SQ_{nameof(User)}Numbers");

            modelBuilder.HasSequence<int>($"SQ_{nameof(User)}Numbers").StartsAt(1).IncrementsBy(1);
            modelBuilder.Entity<Administrator>()
                .Property(p => p.Id)
                .HasDefaultValueSql($"NEXT VALUE FOR SQ_{nameof(User)}Numbers");

            modelBuilder.HasSequence<int>($"SQ_{nameof(User)}Numbers").StartsAt(1).IncrementsBy(1);
            modelBuilder.Entity<Teacher>()
                .Property(p => p.Id)
                .HasDefaultValueSql($"NEXT VALUE FOR SQ_{nameof(User)}Numbers");

            modelBuilder.HasSequence<int>($"SQ_{nameof(User)}Numbers").StartsAt(1).IncrementsBy(1);
            modelBuilder.Entity<Student>()
                .Property(p => p.Id)
                .HasDefaultValueSql($"NEXT VALUE FOR SQ_{nameof(User)}Numbers");

            modelBuilder.HasSequence<int>($"SQ_{nameof(Course)}Numbers").StartsAt(1).IncrementsBy(1);
            modelBuilder.Entity<Course>()
                .Property(p => p.Id)
                .HasDefaultValueSql($"NEXT VALUE FOR SQ_{nameof(Course)}Numbers");

            modelBuilder.HasSequence<int>($"SQ_{nameof(Degree)}Numbers").StartsAt(1).IncrementsBy(1);
            modelBuilder.Entity<Degree>()
                .Property(p => p.Id)
                .HasDefaultValueSql($"NEXT VALUE FOR SQ_{nameof(Degree)}Numbers");


            modelBuilder.HasSequence<int>($"SQ_{nameof(Degree)}Numbers").StartsAt(1).IncrementsBy(1);
            modelBuilder.Entity<TeacherDegree>()
                .Property(p => p.Id)
                .HasDefaultValueSql($"NEXT VALUE FOR SQ_{nameof(Degree)}Numbers");

            modelBuilder.HasSequence<int>($"SQ_{nameof(Degree)}Numbers").StartsAt(1).IncrementsBy(1);
            modelBuilder.Entity<StudentDegree>()
                .Property(p => p.Id)
                .HasDefaultValueSql($"NEXT VALUE FOR SQ_{nameof(Degree)}Numbers");

            modelBuilder.HasSequence<int>($"SQ_{nameof(EducationalMaterial)}Numbers").StartsAt(1).IncrementsBy(1);
            modelBuilder.Entity<EducationalMaterial>()
                .Property(p => p.Id)
                .HasDefaultValueSql($"NEXT VALUE FOR SQ_{nameof(EducationalMaterial)}Numbers");

            modelBuilder.HasSequence<int>($"SQ_{nameof(Specialty)}Numbers").StartsAt(1).IncrementsBy(1);
            modelBuilder.Entity<Specialty>()
                .Property(p => p.Id)
                .HasDefaultValueSql($"NEXT VALUE FOR SQ_{nameof(Specialty)}Numbers");


            modelBuilder.HasSequence<int>($"SQ_{nameof(StudentCourseStatistic)}Numbers").StartsAt(1).IncrementsBy(1);
            modelBuilder.Entity<StudentCourseStatistic>()
                .Property(p => p.Id)
                .HasDefaultValueSql($"NEXT VALUE FOR SQ_{nameof(StudentCourseStatistic)}Numbers");

            modelBuilder.HasSequence<int>($"SQ_{nameof(StudentTaskAttempt)}Numbers").StartsAt(1).IncrementsBy(1);
            modelBuilder.Entity<StudentTaskAttempt>()
                .Property(p => p.Id)
                .HasDefaultValueSql($"NEXT VALUE FOR SQ_{nameof(StudentTaskAttempt)}Numbers");

            modelBuilder.HasSequence<int>($"SQ_{nameof(StudentTaskStatistic)}Numbers").StartsAt(1).IncrementsBy(1);
            modelBuilder.Entity<StudentTaskStatistic>()
                .Property(p => p.Id)
                .HasDefaultValueSql($"NEXT VALUE FOR SQ_{nameof(StudentTaskStatistic)}Numbers");

            modelBuilder.HasSequence<int>($"SQ_{nameof(Task)}Numbers").StartsAt(1).IncrementsBy(1);
            modelBuilder.Entity<Model.Task>()
                .Property(p => p.Id)
                .HasDefaultValueSql($"NEXT VALUE FOR SQ_{nameof(Task)}Numbers");

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
