using VolunteerConnect.Models;
using VolunteerConnect.Views;

namespace VolunteerConnect.Components;

public partial class OpportunityCardView : ContentView
{

    public OpportunityCardView()
    {
        InitializeComponent();
    }


    private async void OnDetailsClicked(object sender, EventArgs e)
    {
        if (BindingContext is VolunteerOpportunity opportunity)
        {
            await Shell.Current.GoToAsync(
                nameof(OpportunityDetailsPage),
                new Dictionary<string, object>
                {
                    {"Opportunity", opportunity}
                });
        }
    }

}