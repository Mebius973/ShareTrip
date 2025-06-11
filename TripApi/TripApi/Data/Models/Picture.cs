using System.Xml.Linq;
using TripApi.Entities;

namespace TripApi.Data.Models
{
    public class Picture
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public string Url { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DateTaken { get; set; }
        public int Likes { get; set; } = 0; // Default value for likes
        public Guid TripId { get; set; } // Foreign key to the Trip model
        public virtual Trip Trip { get; set; } = null!; // Navigation property to the Trip entity
        public virtual ICollection<Commentary> Commentary { get; set; } = new List<Commentary>();

        public Picture() { }

        public Picture(PictureEntity entity) {
            Id = entity.Id;
            Url = entity.Url;
            Description = entity.Description;
            DateTaken = entity.DateTaken;
            Likes = entity.Likes;
        }

        public PictureEntity ToEntity()
        {
            return new PictureEntity
            {
                Id = Id,
                Url = Url,
                Likes = Likes,
                Description = Description,
                DateTaken = DateTaken
            };
        }
    }
}
