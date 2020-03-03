using Publico.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Publico.Models {
    public class MessagesTranslate {
        [Key]
        public int Id { get; set; }
        public int MessageId { get; set; }
        public string Text { get; set; }
        public DateTime Time { get; set; }
        public List<MultepleContextTable> MultepleContextTables { get; set; }
        public MessagesTranslate() {
            MultepleContextTables = new List<MultepleContextTable>();
        }
    }
}
