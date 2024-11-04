using System.ComponentModel.DataAnnotations;

namespace VIRTUAL_LAB_API.Model
{
    public class StudentTaskAttempt
    {
        public int Id { get; set; }
        public int Number { get; set; }
        [MaxLength(20000)]
        public string StudentDataJSON { get; set; }
        public double Rate { get; set; }
        public bool IsSuccessful { get; set; }
        public DateTime AttemptDate { get; set; }

        public Task Task { get; set; }
        public Student Student { get; set; }

        //public UserTaskAttempt(int number, int studentDataJSON, double rate, bool isSuccessful, DateTime AttemptDate, Student student, Task task) { }
    }
}
