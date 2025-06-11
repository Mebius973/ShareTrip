using TripApi.Entities;
using TripApi.Repositories;

namespace TripApi.Services
{
    public class TripsService
    {
        private readonly ITripsRepository _tripRepository;
        public TripsService(ITripsRepository tripRepository)
        {
            _tripRepository = tripRepository;
        }
        public async Task<IList<TripEntity>> GetAllAsync(string ownerId)
        {
            return await _tripRepository.GetAllAsync(ownerId);
        }
        public async Task<TripEntity?> GetAsync(string ownerId, string id) { 
            return await _tripRepository.GetAsync(ownerId, id);
        }
        public async Task<bool> CreateAsync(string ownerId, TripEntity trip)
        {
            return await _tripRepository.CreateAsync(ownerId, trip);
        }

        public async Task<bool> UpdateAsync(string ownerId, string id, TripEntity trip) {
            return await _tripRepository.UpdateAsync(ownerId, id, trip);
        }

        public async Task<bool> DeleteAsync(string ownerId, string id)
        {
            return await _tripRepository.DeleteAsync(ownerId, id);
        }
    }
}
