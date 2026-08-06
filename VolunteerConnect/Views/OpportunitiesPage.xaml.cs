using System.Collections.ObjectModel;
using VolunteerConnect.Models;

namespace VolunteerConnect.Views;


public partial class OpportunitiesPage : ContentPage
{

    ObservableCollection<VolunteerOpportunity> Opportunities { get; set; }


    List<VolunteerOpportunity> allOpportunities;


    public OpportunitiesPage()
    {
        InitializeComponent();


        allOpportunities = new()
        {

            new VolunteerOpportunity
            {
                Id=1,
                Title="Beach Cleanup",
                Category="Environment",
                Date = new DateTime(2026, 8, 15),
                Time="10:00 AM",
                Location="Auckland Beach",
                Description="Help clean local beaches.",
                Requirements="Bring gloves and water.",
                AvailablePlaces=12,
                IsAvailable=true,
                ImageName="beach.png"
            },


            new VolunteerOpportunity
            {
                Id=2,
                Title="Animal Shelter Helper",
                Category="Animals",
                Date = new DateTime(2026, 8, 20),
                Time="9:00 AM",
                Location="Auckland Shelter",
                Description="Assist with animal care.",
                Requirements="Must enjoy working with animals.",
                AvailablePlaces=5,
                IsAvailable=true,
                ImageName="animal.png"
            }

        };


        Opportunities = new ObservableCollection<VolunteerOpportunity>(
            allOpportunities);


        BindingContext = this;

    }



    private void OnSearchChanged(object sender, TextChangedEventArgs e)
    {

        Opportunities.Clear();


        foreach (var item in allOpportunities
            .Where(x => x.Title
            .Contains(e.NewTextValue,
            StringComparison.OrdinalIgnoreCase)))
        {
            Opportunities.Add(item);
        }

    }



    private void OnCategoryChanged(object sender, EventArgs e)
    {

        var picker = (Picker)sender;


        if (picker.SelectedItem?.ToString() == "All")
        {
            Opportunities =
            new ObservableCollection<VolunteerOpportunity>(allOpportunities);
        }

    }

}