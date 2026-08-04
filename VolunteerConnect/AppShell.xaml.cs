using VolunteerConnect.Views;

namespace VolunteerConnect
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            //Register routes for navigation
            Routing.RegisterRoute(nameof(OpportunityDetailsPage), typeof(OpportunityDetailsPage));
            Routing.RegisterRoute(nameof(RegistrationPage), typeof(RegistrationPage));
        }
    }
}