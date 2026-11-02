using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using WestcoastEducation.Enums;
using WestcoastEducation.Models;
namespace Client.ViewModels;

public partial class CoursesViewModel : ObservableObject
{
    public string PageTitle { get; } = "Courses";
    public ObservableCollection<Course> Courses { get; } = [
        new Course{
            CourseNumber = "C101",
            Title = "C# Fundamentals",
            StartDate = new DateTime(2026, 11, 9),
            LengthInWeeks = 8,
            CourseType = CourseType.Klassrum
        },
        new Course{
            CourseNumber = "C102",
            Title = "Avalonia UI",
            StartDate = new DateTime(2027, 1, 11),
            LengthInWeeks = 6,
            CourseType = CourseType.Distans
        }
    ];

}
