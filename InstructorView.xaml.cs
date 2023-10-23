using MAUI.project.ViewModels;

namespace MAUI.project.Views;

public partial class InstructorView : ContentPage
{
	public InstructorView()
	{
		InitializeComponent();
		BindingContext = new InstructorViewViewModel();
	}
	

	private void LogoutClicked (object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//MainPage");
	}
    private void CreateNewPersonClicked(object sender, EventArgs e)
    {
		(BindingContext as InstructorViewViewModel).AddPersonClick(Shell.Current);
    }
	private void ContentPage_NavigatedTo ( object sender, NavigatedToEventArgs e)
	{
		(BindingContext as InstructorViewViewModel).RefreshView();
	}
    private void RemovePersonClicked(object sender, EventArgs e)
    {
        (BindingContext as InstructorViewViewModel).RemovePersonClick(Shell.Current);
    }
    private void UpdatePersonClicked(object sender, EventArgs e)
    {
        (BindingContext as InstructorViewViewModel).AddPersonClick(Shell.Current);
    }
	private void Toolbar_PeopleClicked(object sender, EventArgs e)
	{
		(BindingContext as InstructorViewViewModel).ShowPeople();
	}
    private void Toolbar_CoursesClicked(object sender, EventArgs e)
    {
        (BindingContext as InstructorViewViewModel).ShowCourses();
    }
    private void CreateNewCourseClicked(object sender, EventArgs e)
    {
        (BindingContext as InstructorViewViewModel).AddCourseClick(Shell.Current);
    }
    private void UpdateCourseClicked(object sender, EventArgs e)
    {
        (BindingContext as InstructorViewViewModel).AddCourseClick(Shell.Current);
    }
    private void RemoveCourseClicked(object sender, EventArgs e)
    {
        (BindingContext as InstructorViewViewModel).RemoveCourseClick(Shell.Current);
    }
}