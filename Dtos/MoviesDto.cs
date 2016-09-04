using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.Dtos
{
    public class MoviesDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public GenreDto Genre { get; set; }
        [Required]
        public byte GenreId { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime DateAdded { get; set; }
        [Required(ErrorMessage = "You must enter the inventory!")]
        [Range(0, 20)]
        public int NumberInStock { get; set; }
    }
}