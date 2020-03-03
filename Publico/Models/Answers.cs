using Publico.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Publico.Models {
    // Класс ответов
    public class Answers {
        [Key]
        public int Id { get; set; }
        public string Answer { get; set; }  // Ответ        
        //[ForeignKey("Id")]
        public string UserIdFk { get; set; }  // Внешний ключ к полю User.Id
        //[ForeignKey("UserId")]
        public string AIdFk { get; set; } // Внешний ключ к Dialogs.UserId
        public List<MultepleContextTable> MultepleContextTables { get; set; }
        public Answers() { 
            MultepleContextTables = new List<MultepleContextTable>();
        }
    }
}
