using Microsoft.EntityFrameworkCore;
using TripApi.Data.Models;

namespace TripApi.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Trip> Trips { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Commentary> Commentaries { get; set; }
    }
}
