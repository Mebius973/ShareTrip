using Microsoft.EntityFrameworkCore;
using TripApi.Data.Models;
using TripApi.Entities;
using TripApi.Repositories;

namespace TripApi.Data
{
    public class PicturesRepository: IPicturesRepository
    {
        private readonly AppDbContext _context;
        public PicturesRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IList<PictureEntity>> GetAllAsync()
        {
            var results = new List<PictureEntity>();
            await _context.Pictures.ForEachAsync(picture => results.Add(picture.ToEntity()));
            return results;
        }

        public async Task<PictureEntity?> GetAsync(int id)
        {
            var picture = await _context.Pictures.FindAsync(id);
            return picture?.ToEntity();
        }

        public async Task<bool> CreateAsync(PictureEntity entity)
        {
            await _context.Pictures.AddAsync(new Picture(entity));
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(int id, PictureEntity entity)
        {
            var picture = await _context.Pictures.FindAsync(id);
            if (picture == null) return false;
            picture.Url = entity.Url;
            picture.Description = entity.Description;
            picture.Likes = entity.Likes;
            picture.DateTaken = entity.DateTaken;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var picture = await _context.Pictures.FindAsync(id);
            if (picture == null) return false;
            _context.Pictures.Remove(picture);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
