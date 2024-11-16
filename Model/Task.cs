using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VIRTUAL_LAB_API.Model
{
    public class Task
    {
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [MaxLength(20000)]
        public string DataJSON { get; set; }
        public int MaxAttempts { get; set; }
        public double MinRate { get; set; }
        public double MaxRate { get; set; }


        [ForeignKey(nameof(Course))]
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
        public virtual List<StudentTaskAttempt> StudentTaskAttempts { get; set; }

        //public Task(string name, string description, string dataJSON, int maxAttempts, Course course) { }
    }

}
