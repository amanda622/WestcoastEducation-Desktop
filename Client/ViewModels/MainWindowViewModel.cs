using System;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
namespace Client.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty]
    public partial ObservableObject CurrentView { get; set; } = new CoursesViewModel();

}


