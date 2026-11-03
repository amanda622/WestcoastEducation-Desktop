using System;
using System.Collections.ObjectModel;
using Client.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using Tmds.DBus.Protocol;
using WestcoastEducation.Enums;
using WestcoastEducation.Models;
namespace Client.ViewModels;

public partial class CoursesViewModel : ObservableObject
{
    private readonly ICourseService _courseService= new InMemoryCourseService();
    public string PageTitle { get; } = "Courses";
    public ObservableCollection<Course> Courses { get; }
    public CoursesViewModel()
    {
        Courses = new ObservableCollection<Course>(_courseService.GetCourses());
    }

}
