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
        public async Task<IList<TripEntity>> GetAllAsync()
        {
            return await _tripRepository.GetAllAsync();
        }
        public async Task<TripEntity?> GetAsync(int id) { 
            return await _tripRepository.GetAsync(id);
        }
        public async Task<bool> CreateAsync(TripEntity trip)
        {
            return await _tripRepository.CreateAsync(trip);
        }

        public async Task<bool> UpdateAsync(int id, TripEntity trip) {
            return await _tripRepository.UpdateAsync(id, trip);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _tripRepository.DeleteAsync(id);
        }
    }
}
