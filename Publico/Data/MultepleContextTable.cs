using Publico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publico.Data {
    public class MultepleContextTable {
        public int Id { get; set; }
        public string UserLoginId { get; set; }
        public Login LogUsers { get; set; } 
        public string UserRegisterId { get; set; }
        public Register RegUsers { get; set; }
    }
}
