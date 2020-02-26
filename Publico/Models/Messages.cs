using Publico.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publico.Models {
    // Класс сообщения пользователя 
    public class Messages {
        public int Id { get; set; }
        public string MessageUserId { get; set; }   // ID юзера, который написал сообщение
        public string MessageBody { get; set; }     // Тело сообщения
        public string ChatId { get; set; }      // ID чата
        public List<MultepleContextTable> MultepleContextTables { get; set; }
        public Messages() {
            MultepleContextTables = new List<MultepleContextTable>();
        }
    }
}
