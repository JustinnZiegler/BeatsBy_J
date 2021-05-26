using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatsBy_J_Models.Artist
{
    public class ArtistDetail
    {
        public int ArtistId { get; set; }

        public string ArtistName { get; set; }

        public DateTime Birthdate { get; set; }

        public List<string> NamesOfSongsByArtist { get; set; } = new List<string>();
    }
}
