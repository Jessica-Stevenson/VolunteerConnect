using SQLite;
using System.Diagnostics.CodeAnalysis;

namespace VolunteerConnect.Models
{
    public class VolunteerOpportunity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [SQLite.NotNull]
        public string Title { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public DateTime Date { get; set; }

        public string Time { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Requirements { get; set; } = string.Empty;

        public int AvailablePlaces { get; set; }

        public string ImageName { get; set; } = string.Empty;

        public bool IsAvailable { get; set; }
    }
}