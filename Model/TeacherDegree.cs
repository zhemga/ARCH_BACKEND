namespace VIRTUAL_LAB_API.Model
{
public class TeacherDegree : Degree
{
    public virtual List<Teacher> Teachers { get; private set; }

}
}
