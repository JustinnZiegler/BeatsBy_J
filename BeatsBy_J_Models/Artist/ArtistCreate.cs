using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatsBy_J_Models.Artist
{
    public class ArtistCreate
    {
        public int ArtistId { get; set; }

        [Required]
        public string ArtistName { get; set; }

        [Required]
        public DateTime Birthdate { get; set; }
    }
}
