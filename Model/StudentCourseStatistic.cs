using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VIRTUAL_LAB_API.Model
{
    public class StudentCourseStatistic
    {
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public double MarkRate { get; set; }
        public double TimeRate { get; set; }
        public double GeneralCourseRate { get; set; }
        public double CompletionRate { get; set; }

        public Student Student { get; set; }
        public Course Course { get; set; }

        //public StudentTaskStatistics(double markRate, double timeRate, double generalCourseRate, double completionRate, Student Student, Student Course) { }

        //public StudentCourseStatistic Calculate() { }
    }

}
