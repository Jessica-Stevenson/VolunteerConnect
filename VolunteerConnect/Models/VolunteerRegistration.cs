using SQLite;

namespace VolunteerConnect.Models
{
    public class VolunteerRegistration
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        //Foreign Key (links to Opportunity)
        public int OpportunityId { get; set; }

        [NotNull]
        public string PreferredName { get; set; } = string.Empty;

        [NotNull]
        public string ContactDetail { get; set; } = string.Empty;

        public string Availability { get; set; } = string.Empty;

        public string Notes { get; set; } = string.Empty;

        public bool ConsentGiven { get; set; }

        public DateTime RegistrationDate { get; set; }
    }
}