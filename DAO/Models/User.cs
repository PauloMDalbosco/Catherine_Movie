using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100, ErrorMessage = "O campo precisa ter mais de 1 ou menos que 100 caracteres.", MinimumLength = 1)]
        [Required(ErrorMessage = "O Nome do usuário é obrigatório")]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        [StringLength(100, ErrorMessage = "O campo precisa ter mais de 3 ou menos que 100 caracteres.", MinimumLength = 3)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [Required(ErrorMessage = "A confirmação da senha é obrigatória")]
        [StringLength(100, ErrorMessage = "O campo precisa ter mais de 3 ou menos que 100 caracteres.", MinimumLength = 3)]
        [NotMapped]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmação da Senha")]
        [Compare("Password", ErrorMessage = "As senhas não coencidem.")]
        public string ConfirmPassword { get; set; }

        public Guid UserGuid { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail tem o formato inválido.")]
        [StringLength(100, ErrorMessage = "O campo precisa ter mais de 3 ou menos que 100 caracteres.", MinimumLength = 3)]
        [Required(ErrorMessage = "O E-mail do usuário é obrigatório")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }
}
