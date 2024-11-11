using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VIRTUAL_LAB_API.Model
{
    public class Degree
    {
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime AdmissionDate { get; set; }
        public DateTime GraduationDate { get; set; }

        public DegreeName DegreeName { get; private set; }
        public Specialty Specialty { get; private set; }

    }
}
