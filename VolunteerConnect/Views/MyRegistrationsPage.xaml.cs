using System.Collections.ObjectModel;
using VolunteerConnect.Models;
using VolunteerConnect.Services;

namespace VolunteerConnect.Views;

public partial class MyRegistrationsPage : ContentPage
{
    private readonly DatabaseService _database;

    public ObservableCollection<RegistrationDisplayItem> Registrations { get; set; } = new();


    public MyRegistrationsPage(DatabaseService database)
    {
        InitializeComponent();

        _database = database;

        RegistrationsCollectionView.ItemsSource = Registrations;
    }


    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await LoadRegistrations();
    }


    private async Task LoadRegistrations()
    {
        var registrations = await _database.GetRegistrationsAsync();

        Registrations.Clear();

        foreach (var registration in registrations)
        {
            var opportunity = await _database.GetOpportunityAsync(
                registration.OpportunityId);

            if (opportunity == null)
                continue;

            Registrations.Add(
                new RegistrationDisplayItem
                {
                    Registration = registration,
                    Opportunity = opportunity
                });
        }
    }


    private async void OnEditClicked(object sender, EventArgs e)
    {
        if (sender is Button button &&
            button.BindingContext is RegistrationDisplayItem item)
        {
            await Shell.Current.GoToAsync(
                $"{nameof(RegistrationPage)}?RegistrationId={item.Registration.Id}");
        }
    }


    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        if (sender is not Button button ||
            button.BindingContext is not RegistrationDisplayItem item)
        {
            return;
        }


        bool confirm = await DisplayAlert(
            "Delete Registration",
            $"Are you sure you want to delete your registration for {item.Opportunity.Title}?",
            "Delete",
            "Cancel");


        if (!confirm)
            return;


        await _database.DeleteRegistrationAsync(item.Registration);


        await DisplayAlert(
            "Deleted",
            "Your registration has been deleted.",
            "OK");


        await LoadRegistrations();
    }
}


public class RegistrationDisplayItem
{
    public VolunteerRegistration Registration { get; set; } = new();

    public VolunteerOpportunity Opportunity { get; set; } = new();
}