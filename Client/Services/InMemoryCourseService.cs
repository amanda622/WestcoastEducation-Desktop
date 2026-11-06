using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WestcoastEducation.Enums;
using WestcoastEducation.Models;

namespace Client.Services;

// A fake service that keeps courses in memory. Useful for testing the UI without RestDb.
public class InMemoryCourseService : ICourseService
{
    private readonly List<Course> _courses =
    [
        new Course
        {
            DbId = "1",
            CourseNumber = "C101",
            Title = "C# Fundamentals",
            StartDate = new DateTime(2026, 11, 9),
            LengthInWeeks = 8,
            CourseType = CourseType.Klassrum
        },
        new Course
        {
            DbId = "2",
            CourseNumber = "C102",
            Title = "Avalonia UI",
            StartDate = new DateTime(2027, 1, 11),
            LengthInWeeks = 6,
            CourseType = CourseType.Distans
        }
    ];

    public Task<List<Course>> GetCoursesAsync()
    {
        return Task.FromResult(new List<Course>(_courses));
    }

    public Task AddCourseAsync(Course course)
    {
        course.DbId = Guid.NewGuid().ToString();
        _courses.Add(course);
        return Task.CompletedTask;
    }

    public Task UpdateCourseAsync(Course course)
    {
        int index = _courses.FindIndex(c => c.DbId == course.DbId);
        if (index >= 0)
        {
            _courses[index] = course;
        }
        return Task.CompletedTask;
    }

    public Task DeleteCourseAsync(Course course)
    {
        _courses.RemoveAll(c => c.DbId == course.DbId);
        return Task.CompletedTask;
    }
}
