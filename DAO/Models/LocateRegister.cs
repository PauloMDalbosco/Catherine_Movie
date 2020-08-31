using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Models
{
    public class LocateRegister
    {
        [Key]
        public int Id { get; set; }
        public int IdMovie { get; set; }
        public int IdLocation { get; set; }
    }
}
