using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VIRTUAL_LAB_API.Model
{
    public class StudentTaskAttempt
    {
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Number { get; set; }
        [MaxLength(20000)]
        public string StudentDataJSON { get; set; }
        public double Rate { get; set; }
        public bool IsSuccessful { get; set; }
        public DateTime AttemptDate { get; set; }

        [ForeignKey(nameof(Task))]
        public int TaskId { get; set; }
        public virtual Task Task { get; set; }

        [ForeignKey(nameof(Student))]
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }

        //public UserTaskAttempt(int number, int studentDataJSON, double rate, bool isSuccessful, DateTime AttemptDate, Student student, Task task) { }
    }
}
