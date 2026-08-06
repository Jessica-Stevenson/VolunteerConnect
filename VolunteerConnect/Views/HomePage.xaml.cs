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

        foreach (var opp in opportunities)
        {
            System.Diagnostics.Debug.WriteLine($"Loaded: {opp.Title}");
        }
    }

    private async void OnBrowseOpportunitiesClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(OpportunitiesPage));
    }
}