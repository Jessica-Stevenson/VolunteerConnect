using VolunteerConnect.Models;
using VolunteerConnect.Services;

namespace VolunteerConnect.Views;

[QueryProperty(nameof(OpportunityId), "id")]
public partial class OpportunityDetailsPage : ContentPage
{
    private readonly DatabaseService _database;

    private VolunteerOpportunity? _opportunity;


    public int OpportunityId { get; set; }


    public OpportunityDetailsPage(DatabaseService database)
    {
        InitializeComponent();

        _database = database;
    }


    protected override async void OnAppearing()
    {
        base.OnAppearing();

        Console.WriteLine($"Opportunity ID: {OpportunityId}");

        if (OpportunityId == 0)
        {
            await DisplayAlert(
                "Error",
                "No opportunity selected.",
                "OK");

            return;
        }


        _opportunity = await _database.GetOpportunityAsync(OpportunityId);


        if (_opportunity == null)
        {
            await DisplayAlert(
                "Error",
                "Opportunity could not be found.",
                "OK");

            return;
        }


        BindingContext = _opportunity;
    }



    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        if (_opportunity == null)
        {
            await DisplayAlert(
                "Error",
                "No opportunity loaded.",
                "OK");

            return;
        }


        await Shell.Current.GoToAsync(
            $"{nameof(RegistrationPage)}?id={_opportunity.Id}");
    }
}