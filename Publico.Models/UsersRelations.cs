﻿using Publico.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Publico.Models {
    public class UsersRelations {
        [Key]
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? ToUserId { get; set; }
        public string Type { get; set; }
        public List<MultepleContextTable> MultepleContextTables { get; set; }
        public UsersRelations() {
            MultepleContextTables = new List<MultepleContextTable>();
        }
    }
}
