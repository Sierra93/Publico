using Publico.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Publico.Models {
    // Класс пользователей
    public class User { 
        public int Id { get; set; }
        [Required]
        public string Login { get; set; }
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public List<MultepleContextTable> MultepleContextTables { get; set; }  
        public User() {
            MultepleContextTables = new List<MultepleContextTable>(); 
        }
    }
}
