using Microsoft.EntityFrameworkCore;
using TripApi.Data.Models;
using TripApi.Entities;
using TripApi.Repositories;

namespace TripApi.Data
{
    public class TripsRepository : ITripsRepository
    {
        private readonly AppDbContext _context;
        public TripsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IList<TripEntity>> GetAllAsync()
        {
            var results = new List<TripEntity>();
            await _context.Trips.ForEachAsync(trip => results.Add(trip.ToEntity()));
            return results;
        }

        public async Task<TripEntity?> GetAsync(int id)
        {
            var trip = await _context.Trips.FindAsync(id);
            return trip?.ToEntity();
        }

        public async Task<bool> CreateAsync(Guid ownerId, TripEntity entity)
        {
            var trip = await _context.Trips.AddAsync(new Trip(ownerId, entity));
            await _context.TripParticipants.AddAsync(new TripParticipant
            {
                TripId = trip.Entity.Id,
                UserId = ownerId
            });
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(int id, TripEntity entity)
        {
            var trip = await _context.Trips.FindAsync(id);
            if (trip == null) return false;
            trip.Name = entity.Name;
            trip.Destination = entity.Destination;
            trip.Description = entity.Description;
            trip.StartDate = entity.StartDate;
            trip.EndDate = entity.EndDate;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var trip = await _context.Trips.FindAsync(id);
            if (trip == null) return false;
            _context.Trips.Remove(trip);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
