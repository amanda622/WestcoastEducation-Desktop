using System;
using System.Collections.Generic;
using Client.Services;
using WestcoastEducation.Models;
using WestcoastEducation.Enums;

namespace Client.Services;

public class InMemoryCourseService : ICourseService
{
    public List<Course> GetCourses()
    {
        return
        [
        new Course
        {
             CourseNumber = "C101",
            Title = "C# Fundamentals",
            StartDate = new DateTime(2026, 11, 9),
            LengthInWeeks = 8,
            CourseType = CourseType.Klassrum
        },
        new Course
        {
            CourseNumber = "C102",
            Title = "Avalonia UI",
            StartDate = new DateTime(2027, 1, 11),
            LengthInWeeks = 6,
            CourseType = CourseType.Distans
        }
        ];
    }
}
