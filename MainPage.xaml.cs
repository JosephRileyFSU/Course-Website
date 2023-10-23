using MAUI.project.ViewModels;

namespace MAUI.project
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }
        private void StudentClicked (object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//StudentView");
        }
        private void InstructorClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//InstructorView");
        }
    }
}