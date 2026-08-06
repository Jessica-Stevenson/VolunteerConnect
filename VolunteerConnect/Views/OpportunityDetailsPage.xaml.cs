using VolunteerConnect.Models;

namespace VolunteerConnect.Views;


[QueryProperty(nameof(Opportunity), "Opportunity")]

public partial class OpportunityDetailsPage : ContentPage
{

    public VolunteerOpportunity Opportunity
    {
        set
        {
            BindingContext = value;
        }
    }


    public OpportunityDetailsPage()
    {
        InitializeComponent();
    }



    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        await DisplayAlert(
            "Registration",
            "Your interest has been recorded!",
            "OK");
    }

}