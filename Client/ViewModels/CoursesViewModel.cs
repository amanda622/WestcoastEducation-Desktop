using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Client.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using WestcoastEducation.Models;

namespace Client.ViewModels;

public partial class CoursesViewModel : ObservableObject
{
    private readonly ICourseService _courseService;

    public string PageTitle { get; } = "Courses";

    public ObservableCollection<Course> Courses { get; } = [];

    public CoursesViewModel(ICourseService courseService)
    {
        _courseService = courseService;
        _ = LoadCoursesAsync();
    }

    private async Task LoadCoursesAsync()
    {
        try
        {
            var courses = await _courseService.GetCoursesAsync();
            foreach (var course in courses)
            {
                Courses.Add(course);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Could not load courses: {ex.Message}");
        }
    }
}
