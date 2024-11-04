namespace VIRTUAL_LAB_API.Model
{
    public class StudentTaskStatistic
    {
        public int Id { get; set; }
        public double MarkRate { get; set; }
        public double TimeRate { get; set; }
        public double GeneralCourseRate { get; set; }

        public Student Student { get; set; }
        public Task Task { get; set; }

        //public StudentTaskStatistic(double MarkRate, double TimeRate, double GeneralCourseRate, Student Student, Student Course) { }

        //public StudentCourseStatistic Calculate() { }
    }
}
