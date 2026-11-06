using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Client.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WestcoastEducation.Models;

namespace Client.ViewModels;

public partial class TeachersViewModel : ObservableObject
{
    private readonly ITeacherService _teacherService;

    public string PageTitle { get; } = "Teachers";

    public ObservableCollection<Teacher> Teachers { get; } = [];

    // The teacher the user clicked in the list (null = nothing selected)
    [ObservableProperty]
    public partial Teacher? SelectedTeacher { get; set; }

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

    // Kompetensområde, e.g. "C#, Databaser"
    [ObservableProperty]
    public partial string FormKnowledgeArea { get; set; } = "";

    [ObservableProperty]
    public partial string StatusMessage { get; set; } = "";

    public TeachersViewModel(ITeacherService teacherService)
    {
        _teacherService = teacherService;
        _ = LoadTeachersAsync();
    }

    private async Task LoadTeachersAsync()
    {
        try
        {
            var teachers = await _teacherService.GetTeachersAsync();

            Teachers.Clear();
            foreach (var teacher in teachers)
            {
                Teachers.Add(teacher);
            }
        }
        catch (Exception ex)
        {
            StatusMessage = $"Could not load teachers: {ex.Message}";
        }
    }

    // Runs automatically when SelectedTeacher changes: copy the teacher into the form.
    partial void OnSelectedTeacherChanged(Teacher? value)
    {
        if (value is null)
        {
            return;
        }

        FormPersonNumber = value.PersonNumber;
        FormFirstName = value.FirstName;
        FormLastName = value.LastName;
        FormEmail = value.Email;
        FormPhone = value.Phone ?? "";
        FormKnowledgeArea = value.KnowledgeArea;
        StatusMessage = "";
    }

    [RelayCommand]
    private async Task AddTeacher()
    {
        Teacher? teacher = CreateTeacherFromForm();
        if (teacher is null)
        {
            return;
        }

        if (Teachers.Any(t => t.PersonNumber == teacher.PersonNumber))
        {
            StatusMessage = $"Teacher {teacher.PersonNumber} already exists.";
            return;
        }

        try
        {
            await _teacherService.AddTeacherAsync(teacher);
            await LoadTeachersAsync();
            ClearForm();
            StatusMessage = $"Teacher {teacher.FirstName} {teacher.LastName} added.";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Could not add teacher: {ex.Message}";
        }
    }

    // Saves all fields of the selected teacher, including the knowledge area.
    [RelayCommand]
    private async Task UpdateTeacher()
    {
        if (SelectedTeacher is null)
        {
            StatusMessage = "Select a teacher in the list first.";
            return;
        }

        Teacher? teacher = CreateTeacherFromForm();
        if (teacher is null)
        {
            return;
        }

        // Keep the RestDb id, so RestDb knows which record to change.
        teacher.DbId = SelectedTeacher.DbId;

        try
        {
            await _teacherService.UpdateTeacherAsync(teacher);
            await LoadTeachersAsync();
            ClearForm();
            StatusMessage = $"Teacher {teacher.FirstName} {teacher.LastName} updated.";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Could not update teacher: {ex.Message}";
        }
    }

    [RelayCommand]
    private void ClearForm()
    {
        SelectedTeacher = null;
        FormPersonNumber = "";
        FormFirstName = "";
        FormLastName = "";
        FormEmail = "";
        FormPhone = "";
        FormKnowledgeArea = "";
        StatusMessage = "";
    }

    // Reads the form. Returns null (and shows a message) if something is missing.
    private Teacher? CreateTeacherFromForm()
    {
        if (string.IsNullOrWhiteSpace(FormPersonNumber) ||
            string.IsNullOrWhiteSpace(FormFirstName) ||
            string.IsNullOrWhiteSpace(FormLastName) ||
            string.IsNullOrWhiteSpace(FormEmail))
        {
            StatusMessage = "Fill in person number, first name, last name and email.";
            return null;
        }

        return new Teacher
        {
            PersonNumber = FormPersonNumber.Trim(),
            FirstName = FormFirstName.Trim(),
            LastName = FormLastName.Trim(),
            Email = FormEmail.Trim(),
            Phone = string.IsNullOrWhiteSpace(FormPhone) ? null : FormPhone.Trim(),
            KnowledgeArea = FormKnowledgeArea.Trim()
        };
    }
}
