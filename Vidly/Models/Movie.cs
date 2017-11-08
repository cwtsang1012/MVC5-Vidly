using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ReleasedDate { get; set; }
        public DateTime AddDate { get; set; }
        public Genre Genre { get; set; }
        public int GenreId { get; set; }
        public int InStock { get; set; }
    }
}