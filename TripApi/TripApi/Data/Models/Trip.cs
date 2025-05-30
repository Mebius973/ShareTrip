using TripApi.Entities;

namespace TripApi.Data.Models
{
    public class Trip
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Destination { get; set; } = string.Empty;

        public virtual ICollection<Picture> Pictures { get; set; } = new List<Picture>();

        public Trip() { }

        public Trip(TripEntity entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
            StartDate = entity.StartDate;
            EndDate = entity.EndDate;
            Destination = entity.Destination;
        }

        public TripEntity ToEntity()
        {
            return new TripEntity
            {
                Id = Id,
                Name = Name,
                Description = Description,
                StartDate = StartDate,
                EndDate = EndDate,
                Destination = Destination
            };
        }
    }
}
