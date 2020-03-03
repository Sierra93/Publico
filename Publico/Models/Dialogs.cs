using Publico.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Publico.Models {
    // Класс диалогов пользователей 
    public class Dialogs {
        [Key]
        public int UserId { get; set; }
        //[ForeignKey("UserId")]
        public string UserOne { get; set; }   // Внешний ключ к User.UserId
        //[ForeignKey("UserId")]
        public string UserTwo { get; set; }   // Внешний ключ к User.UserId
        public List<MultepleContextTable> MultepleContextTables { get; set; }
        public Dialogs() { 
            MultepleContextTables = new List<MultepleContextTable>();
        }
    }
}
