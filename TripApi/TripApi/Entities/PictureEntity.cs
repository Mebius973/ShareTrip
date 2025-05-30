namespace TripApi.Entities
{
    public class PictureEntity
    {
        public Guid Id { get; set; }
        public string Url { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Likes { get; set; } = 0;
        public DateTime DateTaken { get; set; }
    }
}
