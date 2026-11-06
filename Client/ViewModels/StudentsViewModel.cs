using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Client.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WestcoastEducation.Models;

namespace Client.ViewModels;

public partial class StudentsViewModel : ObservableObject
{
    private readonly IStudentService _studentService;

    public string PageTitle { get; } = "Students";

    public ObservableCollection<Student> Students { get; } = [];

    // ---- Form fields (two-way bound to the input boxes) ----
    [ObservableProperty]
    public partial string FormPersonNumber { get; set; } = "";

    [ObservableProperty]
    public partial string FormFirstName { get; set; } = "";

    [ObservableProperty]
    public partial string FormLastName { get; set; } = "";

    [ObservableProperty]
    public partial string FormEmail { get; set; } = "";

    [ObservableProperty]
    public partial string FormPhone { get; set; } = "";

    [ObservableProperty]
    public partial string StatusMessage { get; set; } = "";

    public StudentsViewModel(IStudentService studentService)
    {
        _studentService = studentService;
        _ = LoadStudentsAsync();
    }

    private async Task LoadStudentsAsync()
    {
        try
        {
            var students = await _studentService.GetStudentsAsync();

            Students.Clear();
            foreach (var student in students)
            {
                Students.Add(student);
            }
        }
        catch (Exception ex)
        {
            StatusMessage = $"Could not load students: {ex.Message}";
        }
    }

    [RelayCommand]
    private async Task AddStudent()
    {
        if (string.IsNullOrWhiteSpace(FormPersonNumber) ||
            string.IsNullOrWhiteSpace(FormFirstName) ||
            string.IsNullOrWhiteSpace(FormLastName) ||
            string.IsNullOrWhiteSpace(FormEmail))
        {
            StatusMessage = "Fill in person number, first name, last name and email.";
            return;
        }

        if (Students.Any(s => s.PersonNumber == FormPersonNumber.Trim()))
        {
            StatusMessage = $"Student {FormPersonNumber} already exists.";
            return;
        }

        var student = new Student
        {
            PersonNumber = FormPersonNumber.Trim(),
            FirstName = FormFirstName.Trim(),
            LastName = FormLastName.Trim(),
            Email = FormEmail.Trim(),
            Phone = string.IsNullOrWhiteSpace(FormPhone) ? null : FormPhone.Trim()
        };

        try
        {
            await _studentService.AddStudentAsync(student);
            await LoadStudentsAsync();
            ClearForm();
            StatusMessage = $"Student {student.FirstName} {student.LastName} added.";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Could not add student: {ex.Message}";
        }
    }

    [RelayCommand]
    private void ClearForm()
    {
        FormPersonNumber = "";
        FormFirstName = "";
        FormLastName = "";
        FormEmail = "";
        FormPhone = "";
        StatusMessage = "";
    }
}
