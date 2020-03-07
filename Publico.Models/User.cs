using Publico.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Publico.Models {
    // Класс пользователей
    public class User { 
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Не указан логин")]
        public string Login { get; set; }
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        [Required(ErrorMessage = "Не указана почта")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Не указан пароль")]
        public string Password { get; set; }
        public string AvatarUrl { get; set; }
        public List<MultepleContextTable> MultepleContextTables { get; set; }  
        public User() {
            MultepleContextTables = new List<MultepleContextTable>(); 
        }
    }
}
