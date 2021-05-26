using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatsBy_J_Models.Rating
{
    public class RatingUpdate
    {
        public int RatingId { get; set; }

        public double EnjoymentScore { get; set; }

        public double SongLengthScore { get; set; }

        public double ArtistStyleScore { get; set; }

        [MaxLength(300, ErrorMessage = "Be less verbose, please!")]
        public string Description { get; set; }
    }
}
