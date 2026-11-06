using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Client.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WestcoastEducation.Enums;
using WestcoastEducation.Models;

namespace Client.ViewModels;

public partial class CoursesViewModel : ObservableObject
{
    private readonly ICourseService _courseService;

    public string PageTitle { get; } = "Courses";

    public ObservableCollection<Course> Courses { get; } = [];

    // The choices shown in the course type drop-down: Klassrum, Distans
    public CourseType[] CourseTypes { get; } = Enum.GetValues<CourseType>();

    // The course the user clicked in the list (null = nothing selected)
    [ObservableProperty]
    public partial Course? SelectedCourse { get; set; }

    // ---- Form fields (two-way bound to the input boxes) ----
    [ObservableProperty]
    public partial string FormCourseNumber { get; set; } = "";

    [ObservableProperty]
    public partial string FormTitle { get; set; } = "";

    [ObservableProperty]
    public partial decimal? FormLengthInWeeks { get; set; } = 1;

    [ObservableProperty]
    public partial DateTime? FormStartDate { get; set; } = DateTime.Today;

    [ObservableProperty]
    public partial CourseType FormCourseType { get; set; } = CourseType.Klassrum;

    [ObservableProperty]
    public partial string StatusMessage { get; set; } = "";

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

            Courses.Clear();
            foreach (var course in courses)
            {
                Courses.Add(course);
            }
        }
        catch (Exception ex)
        {
            StatusMessage = $"Could not load courses: {ex.Message}";
        }
    }

    // Runs automatically when SelectedCourse changes: copy the course into the form.
    partial void OnSelectedCourseChanged(Course? value)
    {
        if (value is null)
        {
            return;
        }

        FormCourseNumber = value.CourseNumber;
        FormTitle = value.Title;
        FormLengthInWeeks = value.LengthInWeeks;
        FormStartDate = value.StartDate;
        FormCourseType = value.CourseType;
        StatusMessage = "";
    }

    [RelayCommand]
    private async Task AddCourse()
    {
        Course? course = CreateCourseFromForm();
        if (course is null)
        {
            return;
        }

        if (Courses.Any(c => c.CourseNumber == course.CourseNumber))
        {
            StatusMessage = $"Course {course.CourseNumber} already exists.";
            return;
        }

        try
        {
            await _courseService.AddCourseAsync(course);
            await LoadCoursesAsync();
            ClearForm();
            StatusMessage = $"Course {course.CourseNumber} added.";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Could not add course: {ex.Message}";
        }
    }

    [RelayCommand]
    private async Task UpdateCourse()
    {
        if (SelectedCourse is null)
        {
            StatusMessage = "Select a course in the list first.";
            return;
        }

        Course? course = CreateCourseFromForm();
        if (course is null)
        {
            return;
        }

        // Keep the RestDb id, so RestDb knows which record to change.
        course.DbId = SelectedCourse.DbId;

        try
        {
            await _courseService.UpdateCourseAsync(course);
            await LoadCoursesAsync();
            ClearForm();
            StatusMessage = $"Course {course.CourseNumber} updated.";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Could not update course: {ex.Message}";
        }
    }

    [RelayCommand]
    private async Task DeleteCourse()
    {
        if (SelectedCourse is null)
        {
            StatusMessage = "Select a course in the list first.";
            return;
        }

        string courseNumber = SelectedCourse.CourseNumber;

        try
        {
            await _courseService.DeleteCourseAsync(SelectedCourse);
            await LoadCoursesAsync();
            ClearForm();
            StatusMessage = $"Course {courseNumber} deleted.";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Could not delete course: {ex.Message}";
        }
    }

    [RelayCommand]
    private void ClearForm()
    {
        SelectedCourse = null;
        FormCourseNumber = "";
        FormTitle = "";
        FormLengthInWeeks = 1;
        FormStartDate = DateTime.Today;
        FormCourseType = CourseType.Klassrum;
        StatusMessage = "";
    }

    // Reads the form. Returns null (and shows a message) if something is missing.
    private Course? CreateCourseFromForm()
    {
        if (string.IsNullOrWhiteSpace(FormCourseNumber) || string.IsNullOrWhiteSpace(FormTitle))
        {
            StatusMessage = "Fill in course number and title.";
            return null;
        }

        if (FormLengthInWeeks is null || FormLengthInWeeks < 1)
        {
            StatusMessage = "Length must be at least 1 week.";
            return null;
        }

        if (FormStartDate is null)
        {
            StatusMessage = "Choose a start date.";
            return null;
        }

        return new Course
        {
            CourseNumber = FormCourseNumber.Trim(),
            Title = FormTitle.Trim(),
            LengthInWeeks = (int)FormLengthInWeeks.Value,
            StartDate = FormStartDate.Value.Date,
            CourseType = FormCourseType
        };
    }
}
