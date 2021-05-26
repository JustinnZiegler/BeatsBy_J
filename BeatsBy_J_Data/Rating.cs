using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatsBy_J_Data
{
    public class Rating
    {
        [Key]
        public int RatingId { get; set; }

        [Required, Range(0, 5)]
        public double EnjoymentScore { get; set; }

        [Required, Range(0, 5)]
        public double SongLengthScore { get; set; }

        [Required, Range(0, 5)]
        public double ArtistStyleScore { get; set; }

        [Range(0, 5)]
        public double ScoreAverage
        {
            get
            {
                var totalScore = EnjoymentScore + SongLengthScore + ArtistStyleScore;
                return Math.Round(totalScore / 3, 2);
            }
        }

        [MaxLength(300, ErrorMessage = "Be less verbose, please!")]
        public string Description { get; set; }

        public Guid UserId { get; set; }

        [ForeignKey(nameof(Song))]
        public int SongId { get; set; }

        public Song Song { get; set; }

        public virtual List<Rating> RatingsForSong { get; set; } = new List<Rating>();
    }
}