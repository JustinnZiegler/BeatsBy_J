using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatsBy_J_Models.Song
{
    public class SongCreate
    {
        [MaxLength(100, ErrorMessage = "Title is too long. Please shorten that shit down.")]
        public string Title { get; set; }

        public int ArtistId { get; set; }

        public string ArtistName { get; set; }

        public int GenreId { get; set; }

        public string GenreName { get; set; }

        public int AlbumId { get; set; }

        public DateTime Date { get; set; }
    }
}
