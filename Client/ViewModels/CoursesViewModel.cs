using CommunityToolkit.Mvvm.ComponentModel;
namespace Client.ViewModels;

public partial class CoursesViewModel:ObservableObject
{
    public string PageTitle {get; } = "Courses";

}
