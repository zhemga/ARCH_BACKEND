namespace VIRTUAL_LAB_API.Model
{
public class Degree
{
    public int Id { get; set; }

    public DateTime AdmissionDate { get; set; }
    public DateTime GraduationDate { get; set; }

    public DegreeName DegreeName { get; private set; }
    public Specialty Specialty { get; private set; }

}
}
