using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatsBy_J_Models.Rating
{
    public class RatingList
    {
        public int RatingId { get; set; }

        public double EnjoymentScore { get; set; }

        public double SongLengthScore { get; set; }

        public double ArtistStyleScore { get; set; }

        public double ScoreAverage
        {
            get
            {
                var totalScore = EnjoymentScore + SongLengthScore + ArtistStyleScore;
                return Math.Round(totalScore / 3, 2);
            }
        }
    }
}
