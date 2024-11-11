namespace VIRTUAL_LAB_API.Model
{
    public class StudentDegree : Degree
    {
        public virtual List<Student> Students { get; private set; }

        //public StudentDegree(Degree degree, Student student) { }
    }

}
