using TripApi.Entities;
using TripApi.Repositories;

namespace TripApi.Services
{
    public class PicturesService
    {
        private readonly IPicturesRepository _picturesRepository;
        public PicturesService(IPicturesRepository picturesRepository)
        {
            _picturesRepository = picturesRepository;
        }
        public async Task<IList<PictureEntity>> GetAllAsync()
        {
            return await _picturesRepository.GetAllAsync();
        }
        public async Task<PictureEntity?> GetAsync(int id)
        {
            return await _picturesRepository.GetAsync(id);
        }
        public async Task<bool> CreateAsync(PictureEntity trip)
        {
            return await _picturesRepository.CreateAsync(trip);
        }

        public async Task<bool> UpdateAsync(int id, PictureEntity trip)
        {
            return await _picturesRepository.UpdateAsync(id, trip);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _picturesRepository.DeleteAsync(id);
        }
    }
}
