using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatsBy_J_Data
{
    public class Song
    {
        [Key]
        public int SongId { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Title is too long. Please shorten that shit down.")]
        public string Title { get; set; }

        [ForeignKey(nameof(Artist))]
        public int? ArtistId { get; set; }

        public virtual Artist Artist { get; set; }

        public string ArtistName { get; set; }

        [ForeignKey(nameof(Genre))]
        public int GenreId { get; set; }

        public virtual Genre Genre { get; set; }

        public string GenreName { get; set; }

        //[ForeignKey(nameof(Album))]
        public int AlbumId { get; set; }

        //public virtual Album Album { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public List<Rating> RatingsForSong { get; set; } = new List<Rating>();

        public double AverageRating
        {
            get
            {
                double totalRating = 0;
                foreach (Rating rating in RatingsForSong)
                {
                    totalRating += rating.ScoreAverage;
                }

                return RatingsForSong.Count > 0 ? Math.Round(totalRating / RatingsForSong.Count, 2) : 0;
            }
        }

        public Guid OwnerId { get; set; }

        public bool IsRecommended
        {
            get
            {
                return AverageRating >= 4;
            }
        }
    }
}