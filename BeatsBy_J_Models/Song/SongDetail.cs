using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatsBy_J_Models.Song
{
    public class SongDetail
    {
        public int SongId { get; set; }

        public int? ArtistId { get; set; }

        public string ArtistName { get; set; }

        public int GenreId { get; set; }

        public string GenreName { get; set; }

        public int AlbumId { get; set; }

        public string Title { get; set; }

        //public Artist Artist { get; set; }

        //public Genre Genre { get; set; }

        //public Album Album { get; set; }

        public DateTime Date { get; set; }

        //public List<Rating> RatingsForSong { get; set; } = new List<Rating>();

        public List<RatingForListInSongDetail> RatingsForSong { get; set; } = new List<RatingForListInSongDetail>();

        public double AverageRating { get; set; }

        public bool IsRecommended { get; set; }
    }
}
