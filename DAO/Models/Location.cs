using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DAO.Models
{
    public class Location
    {     

        [Key]
        public int Id { get; set; }

        //public Location()
        //{
        //    Movie m = new Movie();
        //    Movies = Query.MovieQuery.GetMovies(m);
        //}

        //public virtual List<Movie> Movies { get; set; }


        [StringLength(14, ErrorMessage = "O campo precisa ter 14 caracteres.")]
        [Required(ErrorMessage = "O CPF é obrigatório")]
        [Display(Name = "CPF")]
        public string CPF { get; set; }

        [Required]
        public DateTime LocationDate { get; set; }
    }
}
