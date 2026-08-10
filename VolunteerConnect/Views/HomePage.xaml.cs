using VolunteerConnect.Services;
using VolunteerConnect.Models;

namespace VolunteerConnect.Views;

public partial class HomePage : ContentPage
{
    private readonly DatabaseService _database;

    public HomePage(DatabaseService database)
    {
        InitializeComponent();
        _database = database;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var opportunities = await _database.GetOpportunitiesAsync();

        var featured = opportunities.FirstOrDefault(o => o.IsAvailable);

        if (featured != null)
        {
            BindingContext = featured;
        }

        var availableCount = opportunities.Count(o => o.IsAvailable);

        OpportunityCountLabel.Text =
            $"Available Volunteer Opportunities: {availableCount}";
    }

    private async void OnBrowseOpportunitiesClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//OpportunitiesPage");
    }
}