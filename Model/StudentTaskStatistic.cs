using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VIRTUAL_LAB_API.Model
{
    public class StudentTaskStatistic
    {
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public double MarkRate { get; set; }
        public double TimeRate { get; set; }
        public double GeneralCourseRate { get; set; }

        public virtual Student Student { get; set; }
        public virtual Task Task { get; set; }

        //public StudentTaskStatistic(double MarkRate, double TimeRate, double GeneralCourseRate, Student Student, Student Course) { }

        //public StudentCourseStatistic Calculate() { }
    }
}
