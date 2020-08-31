using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        [StringLength(200, ErrorMessage = "O campo precisa ter mais de 1 ou menos que 200 caracteres.", MinimumLength = 1)]
        [Required(ErrorMessage = "O Nome do filme é obrigatório")]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        public DateTime CreationDate { get; set; }

        public bool Active { get; set; }

        [ForeignKey("Genre")]
        public int GenreId { get; set; }
        public Genre Genre { get; set; }

        public bool Del { get; set; }
    }
}
