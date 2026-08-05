namespace VolunteerConnect.Views;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();
    }


    private async void OnBrowseOpportunitiesClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(OpportunitiesPage));
    }
}