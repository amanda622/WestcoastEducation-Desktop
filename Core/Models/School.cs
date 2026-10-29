using WestcoastEducation.Interfaces;

namespace WestcoastEducation.Models;

public class School
{
    //aggregation
    public List<Course> Courses { get; set; } = [];
    public List<Student> Students { get; set; } = [];
    public List<Teacher> Teachers { get; set; } = [];

    private void AddItem<T>(List<T> list, T item, string duplicateMessage) where T : IHasId
    {
        foreach (var existing in list)
        {
            if (existing.Id == item.Id)
            {
                Console.WriteLine(duplicateMessage);
                return;
            }
        }
        list.Add(item);
    }
    private void ListItems<T>(List<T> list, string emptyMessage)
    {
        if (list.Count == 0)
        {
            Console.WriteLine(emptyMessage);
            return;
        }
        foreach (var item in list)
        {
            Console.WriteLine(item);
        }
    }

    public void AddCourse(Course course) => AddItem(Courses, course, "Kursen finns redan.");
    public void ListCourses() => ListItems(Courses, "Inga kurser.");

    public void AddStudent(Student student) => AddItem(Students, student, "Student finns redan.");
    public void ListStudents() => ListItems(Students, "Inga studenter.");

    public void AddTeacher(Teacher teacher) => AddItem(Teachers, teacher, "Läraren finns redan.");
    public void ListTeachers() => ListItems(Teachers, "Inga lärare.");

}
