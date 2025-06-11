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

        public async Task<IList<TripEntity>> GetAllAsync(string ownerId)
        {
            if (Guid.TryParse(ownerId, out var ownerGuid))
            {
                var results = new List<TripEntity>();
                await _context.Trips
                      .Include(t => t.Participants)
                      .Where(t => t.Participants.Any(p => p.UserId == ownerGuid))
                      .ForEachAsync(trip => results.Add(trip.ToEntity()));
                return results;
            }
            else
            { 
                throw new ArgumentException("Invalid owner ID (" + ownerId + ") format.");
            }
        }

        public async Task<TripEntity?> GetAsync(string ownerId, string id)
        {
            if (Guid.TryParse(id, out var guid) &&
                Guid.TryParse(ownerId, out var ownerGuid))
            {
                var trip = await _context.Trips
                    .Include(t => t.Participants)
                    .FirstOrDefaultAsync(t =>
                        t.Id == guid &&
                        t.Participants.Any(p => p.UserId == ownerGuid)
                    );
                return trip?.ToEntity();
            }
            else
            {
                throw new ArgumentException("Invalid ID (" + id + ") or owner ID (" + ownerId + ") format.");
            }
        }

        public async Task<bool> CreateAsync(string ownerId, TripEntity entity)
        {
            if (Guid.TryParse(ownerId, out var ownerGuid))
            {
                var trip = await _context.Trips.AddAsync(new Trip(ownerGuid, entity));
                await _context.TripParticipants.AddAsync(new TripParticipant
                {
                    TripId = trip.Entity.Id,
                    UserId = ownerGuid
                });
                return await _context.SaveChangesAsync() > 0;
            }
            else
            {
                throw new ArgumentException("Invalid owner ID (" + ownerId + ") format.");
            }
        }

        public async Task<bool> UpdateAsync(string ownerId, string id, TripEntity entity)
        {
            if (Guid.TryParse(ownerId, out var ownerGuid))
            {
                var trip = await _context.Trips.FindAsync(id);
                if (trip == null || trip.OwnerId != ownerGuid) return false;
                trip.Name = entity.Name;
                trip.Destination = entity.Destination;
                trip.Description = entity.Description;
                trip.StartDate = entity.StartDate;
                trip.EndDate = entity.EndDate;

                return await _context.SaveChangesAsync() > 0;
            }
            else
            {
                throw new ArgumentException("Invalid owner ID (" + ownerId + ") format.");
            }
        }

        public async Task<bool> DeleteAsync(string ownerId, string id)
        {
            if (Guid.TryParse(ownerId, out var ownerGuid))
            {
                var trip = await _context.Trips.FindAsync(id);
                if (trip == null || trip.OwnerId != ownerGuid) return false;
                _context.Trips.Remove(trip);
                return await _context.SaveChangesAsync() > 0;
            }
            else
            {
                throw new ArgumentException("Invalid owner ID (" + ownerId + ") format.");
            }
        }
    }
}
