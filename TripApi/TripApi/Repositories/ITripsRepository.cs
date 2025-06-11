using TripApi.Entities;

namespace TripApi.Repositories
{
    public interface ITripsRepository
    {
        Task<IList<TripEntity>> GetAllAsync(string ownerId);
        Task<TripEntity?> GetAsync(string ownerId, string id);
        Task<bool> CreateAsync(string ownerId, TripEntity entity);
        Task<bool> UpdateAsync(string ownerId, string id, TripEntity entity);
        Task<bool> DeleteAsync(string ownerId, string id);
    }
}
