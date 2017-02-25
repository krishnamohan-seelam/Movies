using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Movies.Models
{
    public class Movie
    {
        public int ID { get; set; }
        [StringLength (60 , MinimumLength =1)]
        [Required]
        public string Title { get; set; }
        [Display(Name="Released On")]
        [DataType(DataType.Date)]
        [DisplayFormat (DataFormatString ="{0:yyyy-MM-dd}", ApplyFormatInEditMode =true)]
        public DateTime ReleaseDate { get; set; }
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        [Required]
        [StringLength(30)]
        public string Genre { get; set; }

        [Range(1, 10)]
        public decimal Rating { get; set; }

        public virtual  IList<MovieImage> MovieImages { get; set; }
    }

    public class MovieDBContext :DbContext
    {
        
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieImage> MovieImages { get; set; }

    }
}