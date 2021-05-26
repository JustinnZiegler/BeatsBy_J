using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatsBy_J_Models.Album
{
    public class AlbumUpdate
    {
        public int AlbumId { get; set; }

        [MaxLength(100, ErrorMessage = "Album Name is too long. Gonna need a record for that name.")]
        public string AlbumName { get; set; }

        public DateTime AlbumReleaseDate { get; set; }
    }
}
