using Publico.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Publico.Models {
    // Класс авторизации юзера
    public class Login {
        public int Id { get; set; }
        [Required]
        public string LoginOrEmail { get; set; } 
        [Required]
        public string Password { get; set; }
        public List<MultepleContextTable> MultepleContextTable { get; set; }
        public Login() {
            MultepleContextTable = new List<MultepleContextTable>();
        }
    }
}
