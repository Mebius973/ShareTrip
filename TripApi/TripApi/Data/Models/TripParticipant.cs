namespace TripApi.Data.Models
{
    public class TripParticipant
    {
        public Guid TripId { get; set; }
        public Guid UserId { get; set; }
        public virtual Trip Trip { get; set; } = null!;
    }
}
