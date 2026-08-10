using System.Collections.ObjectModel;
using VolunteerConnect.Models;
using VolunteerConnect.Services;

namespace VolunteerConnect.Views;

public partial class OpportunitiesPage : ContentPage
{
    private readonly DatabaseService _database;

    private List<VolunteerOpportunity> allOpportunities = new();

    public ObservableCollection<VolunteerOpportunity> Opportunities { get; set; } = new();


    public OpportunitiesPage(DatabaseService database)
    {
        InitializeComponent();

        _database = database;

        BindingContext = this;
    }


    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await LoadOpportunities();
    }


    private async Task LoadOpportunities()
    {
        allOpportunities = await _database.GetOpportunitiesAsync();

        Opportunities.Clear();

        foreach (var opportunity in allOpportunities)
        {
            Opportunities.Add(opportunity);
        }
    }


    private void OnSearchChanged(object sender, TextChangedEventArgs e)
    {
        string searchText = e.NewTextValue ?? "";

        Opportunities.Clear();


        foreach (var item in allOpportunities.Where(x =>
            x.Title.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            x.Category.Contains(searchText, StringComparison.OrdinalIgnoreCase)))
        {
            Opportunities.Add(item);
        }
    }

    private void OnCategoryChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;


        string category = picker.SelectedItem?.ToString();


        Opportunities.Clear();


        if (category == "All" || string.IsNullOrEmpty(category))
        {
            foreach (var item in allOpportunities)
            {
                Opportunities.Add(item);
            }

            return;
        }


        foreach (var item in allOpportunities.Where(x =>
            x.Category == category))
        {
            Opportunities.Add(item);
        }
    }

}