namespace WestcoastEducation.Models;

public class Teacher:Person
{
    public string KnowledgeArea {get; set;} = "";
    // Aggregation
    public List<Course> Courses { get; set; } = [];
    public override string ToString()
    {
        return $"{base.ToString()} KnowledgeArea: {KnowledgeArea} Courses: {Courses.Count}" ;
    }
    public void AddCourse(Course course)
    {
        foreach(var item in Courses)
        {
            if (item.CourseNumber == course.CourseNumber)
            {
                Console.WriteLine("Läraren ansvarar redan för den här kursen.");
                return;
            }
        }
        Courses.Add(course);
    }
}
