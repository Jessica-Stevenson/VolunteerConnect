using SQLite;
using VolunteerConnect.Models;

namespace VolunteerConnect.Services
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection? _database;

        private async Task Init()
        {
            if (_database != null)
                return;

            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "volunteer.db3");

            _database = new SQLiteAsyncConnection(dbPath);

            await _database.CreateTableAsync<VolunteerOpportunity>();
            await _database.CreateTableAsync<VolunteerRegistration>();

            await SeedData();
        }

        private async Task SeedData()
        {
            var count = await _database.Table<VolunteerOpportunity>().CountAsync();

            if (count > 0)
                return;

            var opportunities = new List<VolunteerOpportunity>
            {
                new VolunteerOpportunity
                {
                    Title = "Beach Cleanup",
                    Category = "Environment",
                    Date = DateTime.Today.AddDays(3),
                    Time = "9:00 AM",
                    Location = "Takapuna Beach",
                    Description = "Help clean up the beach and protect marine life.",
                    Requirements = "Bring gloves and sunscreen",
                    AvailablePlaces = 10,
                    ImageName = "beach.jpg",
                    IsAvailable = true
                },
                new VolunteerOpportunity
                {
                    Title = "Animal Shelter Helper",
                    Category = "Animals",
                    Date = DateTime.Today.AddDays(5),
                    Time = "1:00 PM",
                    Location = "Auckland Shelter",
                    Description = "Assist with feeding and caring for animals.",
                    Requirements = "Must be comfortable with animals",
                    AvailablePlaces = 5,
                    ImageName = "animals.jpg",
                    IsAvailable = true
                }
            };

            await _database.InsertAllAsync(opportunities);
        }
        public async Task<List<VolunteerOpportunity>> GetOpportunitiesAsync()
        {
            await Init();
            return await _database.Table<VolunteerOpportunity>().ToListAsync();
        }

        public async Task<VolunteerOpportunity> GetOpportunityAsync(int id)
        {
            await Init();
            return await _database.Table<VolunteerOpportunity>()
                                  .Where(o => o.Id == id)
                                  .FirstOrDefaultAsync();
        }

        public async Task<int> SaveOpportunityAsync(VolunteerOpportunity opportunity)
        {
            await Init();

            if (opportunity.Id != 0)
                return await _database.UpdateAsync(opportunity);
            else
                return await _database.InsertAsync(opportunity);
        }

        public async Task<int> DeleteOpportunityAsync(VolunteerOpportunity opportunity)
        {
            await Init();
            return await _database.DeleteAsync(opportunity);
        }

        public async Task<List<VolunteerRegistration>> GetRegistrationsAsync()
        {
            await Init();
            return await _database.Table<VolunteerRegistration>().ToListAsync();
        }

        public async Task<List<VolunteerRegistration>> GetRegistrationsByOpportunityAsync(int opportunityId)
        {
            await Init();
            return await _database.Table<VolunteerRegistration>()
                                  .Where(r => r.OpportunityId == opportunityId)
                                  .ToListAsync();
        }

        public async Task<int> SaveRegistrationAsync(VolunteerRegistration registration)
        {
            await Init();

            if (registration.Id != 0)
                return await _database.UpdateAsync(registration);
            else
                return await _database.InsertAsync(registration);
        }

        public async Task<int> DeleteRegistrationAsync(VolunteerRegistration registration)
        {
            await Init();
            return await _database.DeleteAsync(registration);
        }

        public async Task<VolunteerRegistration> GetRegistrationAsync(int id)
        {
            await Init();

            return await _database.Table<VolunteerRegistration>()
                                  .Where(r => r.Id == id)
                                  .FirstOrDefaultAsync();
        }
    }


}