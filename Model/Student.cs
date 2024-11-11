namespace VIRTUAL_LAB_API.Model
{
    public class Student : User
    {
        public virtual List<StudentDegree> StudentDegrees { get; private set; }
        public virtual List<Course> Courses { get; private set; }
        public virtual List<StudentTaskAttempt> StudentTaskAttempts { get; set; }

        public virtual List<StudentCourseStatistic> StudentCourseStatistics { get; set; }
        public virtual List<StudentTaskStatistic> StudentTaskStatistics { get; set; }

        //public Task TakeTask(Task task) { }
        //public StudentTaskAttempt FinishTask(Task task, string DataJSON) { }

        //public EducationalMaterial DonwnloadEducationMaterial(EducationMaterial educationMaterial) { }

        //public StudentCourseStatistic CalculateCourseStatistics(Course course) { }
        //public StudentTaskStatistic CalculateTaskStatistics(Task task) { }

        //public StudentCourseStatistic GetCourseStatistics(Course course) { }
        //public StudentTaskStatistic GetTaskStatistics(Task task) { }
    }
}
