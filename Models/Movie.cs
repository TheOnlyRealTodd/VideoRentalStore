using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.ModelBinding;

namespace Vidly.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        
        public Genre Genre { get; set; }
        [Required]
        [Display(Name = "Genre")]
        public byte GenreId { get; set; }
        [Display(Name ="Release Date")]
        public DateTime ReleaseDate { get; set; }
        public DateTime DateAdded { get; set; }
        [Display(Name = "Number in Stock")]
        [Required(ErrorMessage = "You must enter the inventory!")]
        [Range(0, 20)]
        public int NumberInStock { get; set; }

        public int NumberAvailable { get; set; }

    }
}