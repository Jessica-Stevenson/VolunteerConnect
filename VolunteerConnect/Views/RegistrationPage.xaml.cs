using System.Net.Mail;
using VolunteerConnect.Models;
using VolunteerConnect.Services;

namespace VolunteerConnect.Views;


[QueryProperty(nameof(OpportunityId), "id")]
public partial class RegistrationPage : ContentPage
{
    private readonly DatabaseService _database;

    public int OpportunityId { get; set; }


    public RegistrationPage(DatabaseService database)
    {
        InitializeComponent();

        _database = database;
    }


    protected override void OnAppearing()
    {
        base.OnAppearing();

        Console.WriteLine($"Opportunity ID: {OpportunityId}");
    }




    private async void OnSubmitClicked(object sender, EventArgs e)
    {

        string name = PreferredNameEntry.Text;
        string contact = ContactEntry.Text;



        // Required fields
        if (string.IsNullOrWhiteSpace(name) ||
           string.IsNullOrWhiteSpace(contact) ||
           string.IsNullOrWhiteSpace(AvailabilityEntry.Text))
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




        VolunteerRegistration registration = new()
        {
            OpportunityId = OpportunityId,

            PreferredName = name,

            ContactDetail = contact,

            Availability = AvailabilityEntry.Text,

            Notes = NotesEditor.Text ?? "",

            ConsentGiven = true,

            RegistrationDate = DateTime.Now
        };

        await _database.SaveRegistrationAsync(registration);

        await DisplayAlert(
            "Success",
            "Your registration has been saved.",
            "OK");


        await Shell.Current.GoToAsync("..");
    }

    private bool IsValidContact(string contact)
    {

        //Email check
        try
        {
            var email = new MailAddress(contact);

            return true;
        }
        catch
        {

        }

        //Phone check
        return contact.All(
            c => char.IsDigit(c) ||
            c == '+' ||
            c == '-' ||
            c == ' ');  

    }

}