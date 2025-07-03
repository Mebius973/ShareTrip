using Microsoft.EntityFrameworkCore;
using TripApi.Data.Models;

namespace TripApi.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Trip> Trips { get; set; }
        public DbSet<TripParticipant> TripParticipants { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TripParticipant>()
                .HasKey(tp => new { tp.TripId, tp.UserId });
        }
    }
}
