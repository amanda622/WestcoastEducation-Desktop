using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using Client.Services;
namespace Client.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    private readonly ICourseService _courseService;
    private readonly IStudentService _studentService;
    private readonly ITeacherService _teacherService;

    [ObservableProperty]
    public partial ObservableObject CurrentView { get; set; }

    public MainWindowViewModel(ICourseService courseService, IStudentService studentService, ITeacherService teacherService)
    {
        _courseService = courseService;
        _studentService = studentService;
        _teacherService = teacherService;
        CurrentView  = new CoursesViewModel(_courseService);
    }

    [RelayCommand]
    private void GoToStudents()
    {
        CurrentView = new StudentsViewModel(_studentService);
    }

    [RelayCommand]
    private void GoToTeachers()
    {
        CurrentView = new TeachersViewModel(_teacherService);
    }

    [RelayCommand]
    private void GoToCourses()
    {
        CurrentView = new CoursesViewModel(_courseService);
    }

}


