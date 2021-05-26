using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatsBy_J_Data
{
    public class Album
    {
        [Key]
        public int AlbumId { get; set; }

        public string Titles { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Album Name is too long. Gonna need a record for that name.")]
        public string AlbumName { get; set; }

        [Required]
        public DateTime AlbumReleaseDate { get; set; }

        [ForeignKey(nameof(Artist))]
        public int ArtistId { get; set; }
        public virtual Artist Artist { get; set; }

        //[ForeignKey(nameof(Song))]
        //public int? SongId { get; set; }

        public virtual Song Song { get; set; }

        public virtual List<Song> SongsInAlbum { get; set; } = new List<Song>();
    }
}
