using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VIRTUAL_LAB_API.Model
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [MaxLength(20000)]
        public string DataJSON { get; set; }
        public int MaxAttempts { get; set; }

        public Course Course { get; set; }
        public List<StudentTaskAttempt> StudentTaskAttempts { get; set; }

        //public Task(string name, string description, string dataJSON, int maxAttempts, Course course) { }
    }

}
