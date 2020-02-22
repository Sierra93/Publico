using Publico.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Publico.Models {
    // Класс сообщения пользователя
    public class Messages {
        public int Id { get; set; } 
        public string MessageFrom { get; set; }     // От кого сообщение
        public string Message { get; set; }     // Тело сообщения
        public string MessageTo { get; set; }   // Кому сообщение
        public List<MultepleContextTable> MultepleContextTables { get; set; } 
        public Messages() { 
            MultepleContextTables = new List<MultepleContextTable>();  
        }
    }
}
