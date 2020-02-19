using Publico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publico.Data {
    public class MultepleContextTable {
        public int UserId { get; set; } 
        public User User { get; set; }
        public int MessageId { get; set; }
        public Messages Message { get; set; }
        public int FriendId { get; set; }
        public Friends Friend { get; set; }
    }
}
