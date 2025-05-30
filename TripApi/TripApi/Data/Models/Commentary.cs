using System;
using TripApi.Entities;

namespace TripApi.Data.Models
{
    public class Commentary
    {
        public Guid Id { get; set; }
        public string Author { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public int Likes { get; set; } = 0; // Default value for likes
        public DateTime DateCreated { get; set; } = DateTime.UtcNow; // Default to current time in UTC
        public Guid PictureId { get; set; } // Foreign key to the Picture model
        public virtual Picture Picture { get; set; } = null!; // Navigation property to the Picture model

        public Commentary() { }

        public Commentary(CommentaryEntity entity)
        {
            Id = entity.Id;
            Author = entity.Author;
            Text = entity.Text;
            Likes = entity.Likes;
            DateCreated = entity.DateCreated;
        }

        public CommentaryEntity ToEntity()
        {
            return new CommentaryEntity
            {
                Id = Id,
                Author = Author,
                Text = Text,
                Likes = Likes,
                DateCreated = DateCreated
            };
        }
    }
}
