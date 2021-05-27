using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatsBy_J_Models.Genre
{
    public class GenreCreate
    {
        [Required]
        [MinLength(2, ErrorMessage = "Please enter more than 2 characters.")]
        [MaxLength(50, ErrorMessage = "Please use 50 characters or less in this field.")]
        public string GenreName { get; set; }
    }
}
