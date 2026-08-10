using System.Net.Mail;
using VolunteerConnect.Models;
using VolunteerConnect.Services;

namespace VolunteerConnect.Views;

[QueryProperty(nameof(OpportunityId), "id")]
[QueryProperty(nameof(RegistrationId), "RegistrationId")]
public partial class RegistrationPage : ContentPage
{
    private readonly DatabaseService _database;

    private VolunteerRegistration? _registration;

    public int OpportunityId { get; set; }

    public int RegistrationId { get; set; }


    public RegistrationPage(DatabaseService database)
    {
        InitializeComponent();

        _database = database;
    }


    protected override async void OnAppearing()
    {
        base.OnAppearing();


        // Editing an existing registration
        if (RegistrationId != 0)
        {
            await LoadRegistration();

            return;
        }


        // Creating a new registration
        Console.WriteLine($"Opportunity ID: {OpportunityId}");

        if (OpportunityId != 0)
        {
            var opportunity = await _database.GetOpportunityAsync(OpportunityId);

            if (opportunity != null)
            {
                OpportunityLabel.Text =
                    $"Opportunity: {opportunity.Title}";
            }
        }
    }


    private async Task LoadRegistration()
    {
        _registration =
            await _database.GetRegistrationAsync(RegistrationId);


        if (_registration == null)
        {
            await DisplayAlert(
                "Error",
                "Registration could not be found.",
                "OK");

            await Shell.Current.GoToAsync("..");

            return;
        }


        OpportunityId = _registration.OpportunityId;


        var opportunity =
            await _database.GetOpportunityAsync(
                _registration.OpportunityId);


        if (opportunity != null)
        {
            OpportunityLabel.Text =
                $"Opportunity: {opportunity.Title}";
        }


        PreferredNameEntry.Text =
            _registration.PreferredName;

        ContactEntry.Text =
            _registration.ContactDetail;

        AvailabilityEntry.Text =
            _registration.Availability;

        NotesEditor.Text =
            _registration.Notes;

        ConsentCheckBox.IsChecked =
            _registration.ConsentGiven;


        SubmitButton.Text = "Update Registration";
    }


    private async void OnSubmitClicked(object sender, EventArgs e)
    {
        string name = PreferredNameEntry.Text ?? "";

        string contact = ContactEntry.Text ?? "";

        string availability =
            AvailabilityEntry.Text ?? "";


        // Required fields
        if (string.IsNullOrWhiteSpace(name) ||
            string.IsNullOrWhiteSpace(contact) ||
            string.IsNullOrWhiteSpace(availability))
        {
            await DisplayAlert(
                "Missing Information",
                "Please complete all required fields.",
                "OK");

            return;
        }


        // Contact validation
        if (!IsValidContact(contact))
        {
            await DisplayAlert(
                "Invalid Contact",
                "Enter a valid email address or phone number.",
                "OK");

            return;
        }


        // Privacy consent
        if (!ConsentCheckBox.IsChecked)
        {
            await DisplayAlert(
                "Consent Required",
                "You must provide privacy consent.",
                "OK");

            return;
        }


        // UPDATE existing registration
        if (_registration != null)
        {
            _registration.PreferredName = name;

            _registration.ContactDetail = contact;

            _registration.Availability = availability;

            _registration.Notes =
                NotesEditor.Text ?? "";

            _registration.ConsentGiven = true;


            await _database.SaveRegistrationAsync(
                _registration);


            await DisplayAlert(
                "Updated",
                "Your registration has been updated.",
                "OK");


            await Shell.Current.GoToAsync("..");

            return;
        }


        // CREATE new registration
        VolunteerRegistration registration = new()
        {
            OpportunityId = OpportunityId,

            PreferredName = name,

            ContactDetail = contact,

            Availability = availability,

            Notes = NotesEditor.Text ?? "",

            ConsentGiven = true,

            RegistrationDate = DateTime.Now
        };


        await _database.SaveRegistrationAsync(
            registration);


        await DisplayAlert(
            "Success",
            "Your registration has been saved.",
            "OK");


        await Shell.Current.GoToAsync("..");
    }


    private bool IsValidContact(string contact)
    {
        // Email check
        try
        {
            var email = new MailAddress(contact);

            return true;
        }
        catch
        {
        }


        // Phone check
        return contact.All(
            c => char.IsDigit(c) ||
                 c == '+' ||
                 c == '-' ||
                 c == ' ');
    }
}