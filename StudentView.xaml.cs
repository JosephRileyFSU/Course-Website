using MAUI.project.ViewModels;

namespace MAUI.project.Views;

public partial class StudentView : ContentPage
{
	public StudentView()
	{
		InitializeComponent();
		BindingContext = new StudentViewViewModel();
	}
    private void LogoutClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }
    private void ViewCoursesClicked(object sender, EventArgs e)
    {
       (BindingContext as StudentViewViewModel).RefreshView();
    }
    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as StudentViewViewModel).RefreshView();
    }
}