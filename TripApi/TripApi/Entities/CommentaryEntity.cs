namespace TripApi.Entities
{
    public class CommentaryEntity
    {
        public Guid Id { get; set; }
        public string Author { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public int Likes { get; set; } = 0;
        public DateTime DateCreated { get; set; } = DateTime.UtcNow; // Default to current time in UTC
    }
}
