using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatsBy_J_Models.Album
{
    public class AlbumDetail
    {
        public int AlbumId { get; set; }

        public string AlbumName { get; set; }

        public DateTime AlbumReleaseDate { get; set; }

        public List<string> SongTitlesByAlbum { get; set; } = new List<string>();
    }
}
