using TripApi.Entities;

namespace TripApi.Repositories
{
    public interface IPicturesRepository
    {
        Task<IList<PictureEntity>> GetAllAsync();
        Task<PictureEntity?> GetAsync(int id);
        Task<bool> CreateAsync(PictureEntity entity);
        Task<bool> UpdateAsync(int id, PictureEntity entity);
        Task<bool> DeleteAsync(int id);
    }
}
