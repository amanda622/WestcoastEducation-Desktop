using System;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
namespace Client.ViewModels;

public partial class MainWindowViewModel: ObservableObject
{
    [ObservableProperty]
    public partial string Greeting {get; set;} = "Välkommen till WestcoastEducation";
    public string Subtitle {get; } = "Administrera kurser, studenter och lärare";
    [RelayCommand]
    private void ChangeGreeting()
    {
        Greeting = "Hej, Du klickade på knappen.";
        Console.WriteLine($"Greeting is now: {Greeting}");

    }

}
