using Publico.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Publico.Models {
    public class Messages {
        [Key]
        public int Id { get; set; }
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        public string Message { get; set; }
        public DateTime Time { get; set; }
        public List<MultepleContextTable> MultepleContextTables { get; set; }
        public Messages() {
            MultepleContextTables = new List<MultepleContextTable>();
        }
    }
}
