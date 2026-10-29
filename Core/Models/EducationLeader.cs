namespace WestcoastEducation.Models;

public class EducationLeader:Teacher
{
    public required DateTime EmploymentDate {get; set;}
    public override string ToString()
    {
        return $"{base.ToString()} EmploymentDate: {EmploymentDate:yyyy-MM-dd}";
    }

}
