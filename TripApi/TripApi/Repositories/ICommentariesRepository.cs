using TripApi.Entities;

namespace TripApi.Repositories
{
    public interface ICommentariesRepository
    {
        Task<IList<CommentaryEntity>> GetAllAsync();
        Task<CommentaryEntity?> GetAsync(int id);
        Task<bool> CreateAsync(CommentaryEntity entity);
        Task<bool> UpdateAsync(int id, CommentaryEntity entity);
        Task<bool> DeleteAsync(int id);
    }
}
