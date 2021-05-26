using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatsBy_J_Models.Rating
{
    public class RatingDetail
    {
        public int RatingId { get; set; }

        public double EnjoymentScore { get; set; }

        public double SongLengthScore { get; set; }

        public double ArtistStyleScore { get; set; }

        public double AverageRating
        {
            get
            {
                var totalScore = EnjoymentScore + SongLengthScore + ArtistStyleScore;
                return totalScore / 3;
            }
        }

        public string Description { get; set; }

        public Guid UserId { get; set; }

        [ForeignKey(nameof(Song))]
        public int SongId { get; set; }
    }
}
