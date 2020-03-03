using Publico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publico.Data {
    public class MultepleContextTable {
        public int UserId { get; set; } 
        public User User { get; set; }
        public int InfoId { get; set; }
        public Info Info { get; set; }
        public int MessageId { get; set; }
        public Messages Message { get; set; }
        public int MessageTranId { get; set; }
        public MessagesTranslate MessageTran { get; set; }
        public int UsersRelId { get; set; }
        public UsersRelations UsersRel { get; set; }
    }
}
