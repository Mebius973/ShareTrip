using TripApi.Entities;

namespace TripApi.Repositories
{
    public interface ITripsRepository
    {
        Task<IList<TripEntity>> GetAllAsync();
        Task<TripEntity?> GetAsync(int id);
        Task<bool> CreateAsync(TripEntity entity);
        Task<bool> UpdateAsync(int id, TripEntity entity);
        Task<bool> DeleteAsync(int id);
    }
}
