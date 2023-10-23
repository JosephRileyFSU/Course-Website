using Library.project.Services;
using Library.project.Tabs;
using MAUI.project.ViewModels;

namespace MAUI.project.Views;

[QueryProperty(nameof(PersonId), "personId")]

public partial class CreateNewPerson : ContentPage
{
	public CreateNewPerson()
	{
		InitializeComponent();
	}
    public int PersonId { get; set; }

    private void FinishedClicked(object sender, EventArgs e)
    {
        (BindingContext as CreateNewPersonModel).AddPerson();
    }
    private void OnLeaving(object sender, NavigatedFromEventArgs e)
    {
        BindingContext = null;
    }
    private void OnArriving (object sender, NavigatedToEventArgs e)
    {
        BindingContext = new CreateNewPersonModel(PersonId);
    }
}