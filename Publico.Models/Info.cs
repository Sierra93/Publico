using Publico.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Publico.Models {
    public class Info {
        [Key]
        public int InfoId { get; set; }
        public int UserIdTo { get; set; }
        public int UserIdFrom { get; set; }
        public List<MultepleContextTable> MultepleContextTables { get; set; }
        public Info() {
            MultepleContextTables = new List<MultepleContextTable>();
        }
    }
}
