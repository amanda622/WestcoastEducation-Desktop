using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using Client.Services;
namespace Client.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    private readonly ICourseService _courseService;
    [ObservableProperty]
    public partial ObservableObject CurrentView { get; set; }
    public MainWindowViewModel(ICourseService courseService)
    {
        _courseService = courseService;
        CurrentView  = new CoursesViewModel(_courseService);
    }
    [RelayCommand]
    private void GoToStudents()
    {
        CurrentView = new StudentsViewModel();
    }

    [RelayCommand]
    private void GoToTeachers()
    {
        CurrentView = new TeachersViewModel();
    }

    [RelayCommand]
    private void GoToCourses()
    {
        CurrentView = new CoursesViewModel(_courseService);
    }

}


