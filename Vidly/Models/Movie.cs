using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Vidly.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name="Release Date")]
        public DateTime ReleasedDate { get; set; }

        public DateTime AddDate { get; set; }

        public Genre Genre { get; set; }

        [Required]
        [Display(Name = "Genre")]
        public int GenreId { get; set; }

        [Required]
        [Range(1,20)]
        [Display(Name="Number in Stock")]
        public int InStock { get; set; }

        [Range(1,20)]
        public int Avalibility { get; set; }
    }
}