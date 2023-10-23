using Library.project.Tabs;
using Library.project.Services;
using MAUI.project.ViewModels;

namespace MAUI.project.Views;
[QueryProperty(nameof(CourseId), "courseId")]
public partial class CourseDetailView : ContentPage
{
	public CourseDetailView()
	{
		InitializeComponent();
	}
    public int CourseId { get; set; }
    private void FinishedClicked(object sender, EventArgs e)
    {
        (BindingContext as CourseDetailViewModel).AddCourse();
    }
    private void OnLeaving(object sender, NavigatedFromEventArgs e)
    {
        BindingContext = null;
    }
    private void OnArriving(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new CourseDetailViewModel(CourseId);
    }
    private void AddToRosterClicked(object sender, EventArgs e)
    {
        (BindingContext as CourseDetailViewModel).AddToRosterClicked(CourseId);
    }
}