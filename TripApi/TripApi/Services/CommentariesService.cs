using TripApi.Entities;
using TripApi.Repositories;

namespace TripApi.Services
{
    public class CommentariesService
    {
        private readonly ICommentariesRepository _commentariesRepository;
        public CommentariesService(ICommentariesRepository commentariesRepository)
        {
            _commentariesRepository = commentariesRepository;
        }
        public async Task<IList<CommentaryEntity>> GetAllAsync()
        {
            return await _commentariesRepository.GetAllAsync();
        }
        public async Task<CommentaryEntity?> GetAsync(int id)
        {
            return await _commentariesRepository.GetAsync(id);
        }
        public async Task<bool> CreateAsync(CommentaryEntity trip)
        {
            return await _commentariesRepository.CreateAsync(trip);
        }

        public async Task<bool> UpdateAsync(int id, CommentaryEntity trip)
        {
            return await _commentariesRepository.UpdateAsync(id, trip);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _commentariesRepository.DeleteAsync(id);
        }
    }
}
