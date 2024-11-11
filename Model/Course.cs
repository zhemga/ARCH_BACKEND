using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VIRTUAL_LAB_API.Model
{
    public class Course
    {
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Teacher> Teachers { get; set; }
        public List<Student> Students { get; private set; }
        public List<EducationalMaterial> EducationalMaterials { get; private set; }
        public List<Task> Tasks { get; private set; }

    }
}
