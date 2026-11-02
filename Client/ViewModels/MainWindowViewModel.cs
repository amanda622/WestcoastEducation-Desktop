using System;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
namespace Client.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty]
    public partial ObservableObject CurrentView { get; set; } = new CoursesViewModel();
    
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
        CurrentView = new CoursesViewModel();
    }

}


