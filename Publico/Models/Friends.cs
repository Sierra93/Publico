using Publico.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publico.Models {
    // Класс друзей пользователя
    public class Friends {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FriendLogin { get; set; }
        public List<MultepleContextTable> MultepleContextTables { get; set; }
        public Friends() { 
            MultepleContextTables = new List<MultepleContextTable>();
        }
    }
}
