using Microsoft.EntityFrameworkCore;
using TripApi.Data.Models;
using TripApi.Entities;
using TripApi.Repositories;

namespace TripApi.Data
{
    public class CommentariesRepository : ICommentariesRepository
    {
        private readonly AppDbContext _context;
        public CommentariesRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IList<CommentaryEntity>> GetAllAsync()
        {
            var results = new List<CommentaryEntity>();
            await _context.Commentaries.ForEachAsync(commentary => results.Add(commentary.ToEntity()));
            return results;
        }

        public async Task<CommentaryEntity?> GetAsync(int id)
        {
            var commentary = await _context.Commentaries.FindAsync(id);
            return commentary?.ToEntity();
        }

        public async Task<bool> CreateAsync(CommentaryEntity entity)
        {
            await _context.Commentaries.AddAsync(new Commentary(entity));
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(int id, CommentaryEntity entity)
        {
            var commentary = await _context.Commentaries.FindAsync(id);
            if (commentary == null) return false;
            commentary.Id = entity.Id;
            commentary.Author = entity.Author;
            commentary.Text = entity.Text;
            commentary.Likes = entity.Likes;
            commentary.DateCreated = entity.DateCreated;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var commentary = await _context.Commentaries.FindAsync(id);
            if (commentary == null) return false;
            _context.Commentaries.Remove(commentary);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
