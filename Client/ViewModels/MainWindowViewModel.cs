using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using Client.Services;
namespace Client.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty]
    public partial ObservableObject CurrentView { get; set; } = new CoursesViewModel(new InMemoryCourseService());
    
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
        CurrentView = new CoursesViewModel(new InMemoryCourseService());
    }

}


