using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Movies.Models
{
    public class MovieImage
    {
         public int MovieImageID { get; set; }

         [Required()]
         [StringLength(1024,MinimumLength =1)]
         public string ImageUrl { get; set; }

       /*
        * For  one to one relation
        *  [Key]
        [ForeignKey("Movie")]
        [Column("Movie_ID")]
        public int MovieID { get; set; }*/

        public virtual Movie Movie { get; set; }
       

    }
}