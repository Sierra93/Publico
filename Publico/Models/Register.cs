using Publico.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Publico.Models {
    // Класс регистрации юзера
    public class Register {
        public int Id { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Email { get; set; } 
        [Required]
        public string Password { get; set; }
        public List<MultepleContextTable> MultepleContextTable { get; set; }
        public Register() {
            MultepleContextTable = new List<MultepleContextTable>();
        }
    }
}
