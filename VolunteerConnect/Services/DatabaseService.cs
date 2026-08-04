using SQLite;
using VolunteerConnect.Models;

namespace VolunteerConnect.Services
{
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);

            _database.CreateTableAsync<VolunteerOpportunity>().Wait();
            _database.CreateTableAsync<VolunteerRegistration>().Wait();
        }

        //Oppturnities
        public Task<List<VolunteerOpportunity>> GetOpportunitiesAsync()
        {
            return _database.Table<VolunteerOpportunity>().ToListAsync();
        }

        public Task<VolunteerOpportunity> GetOpportunityAsync(int id)
        {
            return _database.Table<VolunteerOpportunity>()
                            .Where(o => o.Id == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveOpportunityAsync(VolunteerOpportunity opportunity)
        {
            if (opportunity.Id != 0)
                return _database.UpdateAsync(opportunity);
            else
                return _database.InsertAsync(opportunity);
        }

        public Task<int> DeleteOpportunityAsync(VolunteerOpportunity opportunity)
        {
            return _database.DeleteAsync(opportunity);
        }

        //Registertations
        public Task<List<VolunteerRegistration>> GetRegistrationsAsync()
        {
            return _database.Table<VolunteerRegistration>().ToListAsync();
        }

        public Task<List<VolunteerRegistration>> GetRegistrationsByOpportunityAsync(int opportunityId)
        {
            return _database.Table<VolunteerRegistration>()
                            .Where(r => r.OpportunityId == opportunityId)
                            .ToListAsync();
        }

        public Task<int> SaveRegistrationAsync(VolunteerRegistration registration)
        {
            if (registration.Id != 0)
                return _database.UpdateAsync(registration);
            else
                return _database.InsertAsync(registration);
        }

        public Task<int> DeleteRegistrationAsync(VolunteerRegistration registration)
        {
            return _database.DeleteAsync(registration);
        }
    }
}