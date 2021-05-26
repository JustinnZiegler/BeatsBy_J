using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatsBy_J_Models
{
    public class RatingForListInSongDetail
    {
        public Guid UserId { get; set; }

        public double ScoreAverage { get; set; }

        public string Description { get; set; }
    }
}
