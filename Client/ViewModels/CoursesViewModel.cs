using System.Collections.ObjectModel;
using Client.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using WestcoastEducation.Models;
namespace Client.ViewModels;

public partial class CoursesViewModel : ObservableObject
{
    private readonly ICourseService _courseService;
    public string PageTitle { get; } = "Courses";
    public ObservableCollection<Course> Courses { get; }
    public CoursesViewModel(ICourseService courseService)
    {
        _courseService = courseService;
        Courses = new ObservableCollection<Course>(_courseService.GetCourses());
    }

}
