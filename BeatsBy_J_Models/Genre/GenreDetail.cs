using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatsBy_J_Models.Genre
{
    public class GenreDetail
    {
        public int GenreId { get; set; }

        public int? SelectedGenreId { get; set; }

        public string GenreName { get; set; }

        public List<string> SongTitlesInGenre { get; set; } = new List<string>();
    }
}
