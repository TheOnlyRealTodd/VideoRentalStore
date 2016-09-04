using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class MovieFormViewModel
    {
        //These are all nullable and not using a direct Movie property because without this setup, the form would
        //automatically populate the DateTime field and Number In Stock field. To get around this, we set all properties here
        //to nullable even though they truly arent, but that's ok because the form validation will catch any null fields,
        //This allows us to avoid creating a new Movie object when the New action is called because we manually set an id here
        //And we use constructors to populate the model which is cleaner in the controller as well.
        //This model is simply there to interact with the form and is not the actual Movie model.
        //Because of this, we can ge away with making these properties null and even removing ones that we aren't setting
        //inside the form such as Genre and DateAdded, which are set by the controller.
        public IEnumerable<Genre> Genres { get; set; }
        public int? Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        
        [Required]
        [Display(Name = "Genre")]
        public byte? GenreId { get; set; }
        [Display(Name = "Release Date")]
        [Required]
        public DateTime? ReleaseDate { get; set; }
        
        [Display(Name = "Number in Stock")]
        [Required(ErrorMessage = "You must enter the inventory!")]
        [Range(1, 20)]
        public int? NumberInStock { get; set; }
        public bool IsNew { get; set; }

        public MovieFormViewModel()
        {
            Id = 0;
        }

        public MovieFormViewModel(Movie movie)
        {
            Id = movie.Id;
            Name = movie.Name;
            ReleaseDate = movie.ReleaseDate;
            NumberInStock = movie.NumberInStock;
            GenreId = movie.GenreId;
            IsNew = false;
        }
    }
}