using Publico.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Publico.Models {
    // Класс сообщения пользователя
    public class UserMessages {
        public int Id { get; set; } 
        [Required]  
        public string Name { get; set; }
        [Required]
        public string Message { get; set; } 
        public List<MultepleContextTable> MultepleContextTables { get; set; } 
        public UserMessages() {
            MultepleContextTables = new List<MultepleContextTable>();  
        }
    }
}
