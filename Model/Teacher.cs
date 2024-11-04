namespace VIRTUAL_LAB_API.Model
{
    public class Teacher : User
    {
        public List<TeacherDegree> TeacherDegree { get; private set; }
        public List<Course> Courses { get; private set; }

        //public Course CreateCourse(Course course) { }
        //public Student AssignStudentToCourse(Student student, Course course) { }
        //public Student AssignStudentsToCourse(List<Student> students, Course course) { }

        //public Task CreateTask(Task task, Course course) { }
        //public Task CreateEducationMaterial(EducationMaterial educationMaterial, Course course) { }

        //public Task SetStudentTaskAttempts(Task task, Student student, int taskAttempts) { }
        //public Task ResetStudentTaskAttempts(Task task, Student student) { }

        //public StudentCourseStatistic GetStudentCourseStatistics(Student student, Course course) { }
        //public StudentTaskStatistic GetStudentTaskStatistics(Student student, Task task) { }
    }
}
