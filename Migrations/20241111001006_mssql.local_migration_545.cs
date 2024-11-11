using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VIRTUAL_LAB_API.Migrations
{
    /// <inheritdoc />
    public partial class mssqllocal_migration_545 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "SQ_CourseNumbers");

            migrationBuilder.CreateSequence<int>(
                name: "SQ_DegreeNumbers");

            migrationBuilder.CreateSequence<int>(
                name: "SQ_EducationalMaterialNumbers");

            migrationBuilder.CreateSequence<int>(
                name: "SQ_SpecialtyNumbers");

            migrationBuilder.CreateSequence<int>(
                name: "SQ_StudentCourseStatisticNumbers");

            migrationBuilder.CreateSequence<int>(
                name: "SQ_StudentTaskAttemptNumbers");

            migrationBuilder.CreateSequence<int>(
                name: "SQ_StudentTaskStatisticNumbers");

            migrationBuilder.CreateSequence<int>(
                name: "SQ_TaskNumbers");

            migrationBuilder.CreateSequence<int>(
                name: "SQ_UserNumbers");

            migrationBuilder.CreateSequence<int>(
                name: "SQ_UserRoleNumbers");

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR SQ_CourseNumbers"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DegreeName",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DegreeName", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Specialty",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR SQ_SpecialtyNumbers"),
                    Name = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialty", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR SQ_UserRoleNumbers"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EducationalMaterial",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR SQ_EducationalMaterialNumbers"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CloudDriveAttachedFileURLs = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationalMaterial", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EducationalMaterial_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Task",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR SQ_TaskNumbers"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataJSON = table.Column<string>(type: "nvarchar(max)", maxLength: 20000, nullable: false),
                    MaxAttempts = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Task_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Degree",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR SQ_DegreeNumbers"),
                    AdmissionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GraduationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DegreeNameId = table.Column<int>(type: "int", nullable: false),
                    SpecialtyId = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Degree", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Degree_DegreeName_DegreeNameId",
                        column: x => x.DegreeNameId,
                        principalTable: "DegreeName",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Degree_Specialty_SpecialtyId",
                        column: x => x.SpecialtyId,
                        principalTable: "Specialty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR SQ_UserNumbers"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HashPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserRoleId = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_UserRole_UserRoleId",
                        column: x => x.UserRoleId,
                        principalTable: "UserRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseStudent",
                columns: table => new
                {
                    CoursesId = table.Column<int>(type: "int", nullable: false),
                    StudentsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseStudent", x => new { x.CoursesId, x.StudentsId });
                    table.ForeignKey(
                        name: "FK_CourseStudent_Course_CoursesId",
                        column: x => x.CoursesId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseStudent_User_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseTeacher",
                columns: table => new
                {
                    CoursesId = table.Column<int>(type: "int", nullable: false),
                    TeachersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseTeacher", x => new { x.CoursesId, x.TeachersId });
                    table.ForeignKey(
                        name: "FK_CourseTeacher_Course_CoursesId",
                        column: x => x.CoursesId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseTeacher_User_TeachersId",
                        column: x => x.TeachersId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentCourseStatistic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR SQ_StudentCourseStatisticNumbers"),
                    MarkRate = table.Column<double>(type: "float", nullable: false),
                    TimeRate = table.Column<double>(type: "float", nullable: false),
                    GeneralCourseRate = table.Column<double>(type: "float", nullable: false),
                    CompletionRate = table.Column<double>(type: "float", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentCourseStatistic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentCourseStatistic_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentCourseStatistic_User_StudentId",
                        column: x => x.StudentId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentStudentDegree",
                columns: table => new
                {
                    StudentDegreesId = table.Column<int>(type: "int", nullable: false),
                    StudentsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentStudentDegree", x => new { x.StudentDegreesId, x.StudentsId });
                    table.ForeignKey(
                        name: "FK_StudentStudentDegree_Degree_StudentDegreesId",
                        column: x => x.StudentDegreesId,
                        principalTable: "Degree",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentStudentDegree_User_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentTaskAttempt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR SQ_StudentTaskAttemptNumbers"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    StudentDataJSON = table.Column<string>(type: "nvarchar(max)", maxLength: 20000, nullable: false),
                    Rate = table.Column<double>(type: "float", nullable: false),
                    IsSuccessful = table.Column<bool>(type: "bit", nullable: false),
                    AttemptDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentTaskAttempt", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentTaskAttempt_Task_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Task",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentTaskAttempt_User_StudentId",
                        column: x => x.StudentId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentTaskStatistic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR SQ_StudentTaskStatisticNumbers"),
                    MarkRate = table.Column<double>(type: "float", nullable: false),
                    TimeRate = table.Column<double>(type: "float", nullable: false),
                    GeneralCourseRate = table.Column<double>(type: "float", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    TaskId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentTaskStatistic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentTaskStatistic_Task_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Task",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentTaskStatistic_User_StudentId",
                        column: x => x.StudentId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherTeacherDegree",
                columns: table => new
                {
                    TeacherDegreeId = table.Column<int>(type: "int", nullable: false),
                    TeachersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherTeacherDegree", x => new { x.TeacherDegreeId, x.TeachersId });
                    table.ForeignKey(
                        name: "FK_TeacherTeacherDegree_Degree_TeacherDegreeId",
                        column: x => x.TeacherDegreeId,
                        principalTable: "Degree",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherTeacherDegree_User_TeachersId",
                        column: x => x.TeachersId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseStudent_StudentsId",
                table: "CourseStudent",
                column: "StudentsId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseTeacher_TeachersId",
                table: "CourseTeacher",
                column: "TeachersId");

            migrationBuilder.CreateIndex(
                name: "IX_Degree_DegreeNameId",
                table: "Degree",
                column: "DegreeNameId");

            migrationBuilder.CreateIndex(
                name: "IX_Degree_SpecialtyId",
                table: "Degree",
                column: "SpecialtyId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationalMaterial_CourseId",
                table: "EducationalMaterial",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourseStatistic_CourseId",
                table: "StudentCourseStatistic",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourseStatistic_StudentId",
                table: "StudentCourseStatistic",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentStudentDegree_StudentsId",
                table: "StudentStudentDegree",
                column: "StudentsId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentTaskAttempt_StudentId",
                table: "StudentTaskAttempt",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentTaskAttempt_TaskId",
                table: "StudentTaskAttempt",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentTaskStatistic_StudentId",
                table: "StudentTaskStatistic",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentTaskStatistic_TaskId",
                table: "StudentTaskStatistic",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Task_CourseId",
                table: "Task",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherTeacherDegree_TeachersId",
                table: "TeacherTeacherDegree",
                column: "TeachersId");

            migrationBuilder.CreateIndex(
                name: "IX_User_UserRoleId",
                table: "User",
                column: "UserRoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseStudent");

            migrationBuilder.DropTable(
                name: "CourseTeacher");

            migrationBuilder.DropTable(
                name: "EducationalMaterial");

            migrationBuilder.DropTable(
                name: "StudentCourseStatistic");

            migrationBuilder.DropTable(
                name: "StudentStudentDegree");

            migrationBuilder.DropTable(
                name: "StudentTaskAttempt");

            migrationBuilder.DropTable(
                name: "StudentTaskStatistic");

            migrationBuilder.DropTable(
                name: "TeacherTeacherDegree");

            migrationBuilder.DropTable(
                name: "Task");

            migrationBuilder.DropTable(
                name: "Degree");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "DegreeName");

            migrationBuilder.DropTable(
                name: "Specialty");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropSequence(
                name: "SQ_CourseNumbers");

            migrationBuilder.DropSequence(
                name: "SQ_DegreeNumbers");

            migrationBuilder.DropSequence(
                name: "SQ_EducationalMaterialNumbers");

            migrationBuilder.DropSequence(
                name: "SQ_SpecialtyNumbers");

            migrationBuilder.DropSequence(
                name: "SQ_StudentCourseStatisticNumbers");

            migrationBuilder.DropSequence(
                name: "SQ_StudentTaskAttemptNumbers");

            migrationBuilder.DropSequence(
                name: "SQ_StudentTaskStatisticNumbers");

            migrationBuilder.DropSequence(
                name: "SQ_TaskNumbers");

            migrationBuilder.DropSequence(
                name: "SQ_UserNumbers");

            migrationBuilder.DropSequence(
                name: "SQ_UserRoleNumbers");
        }
    }
}
