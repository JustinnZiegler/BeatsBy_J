using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatsBy_J_Models.Rating
{
    public class RatingCreate
    {
        [Required, Range(0, 5)]
        public double EnjoymentScore { get; set; }

        [Required, Range(0, 5)]
        public double SongLengthScore { get; set; }

        [Required, Range(0, 5)]
        public double ArtistStyleScore { get; set; }

        [MaxLength(300, ErrorMessage = "Be less verbose, please!")]
        public string Description { get; set; }

        public int SongId { get; set; }
    }
}
