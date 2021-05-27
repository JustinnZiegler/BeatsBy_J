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
        [Required]
        public string ArtistName { get; set; }

        [Required]
        public DateTime Birthdate { get; set; }
    }
}
