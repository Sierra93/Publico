﻿using Publico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publico.Data {
    public class MultepleContextTable {
        public int UserId { get; set; } 
        public User User { get; set; }
        public int DialogId { get; set; } 
        public Dialogs Dialogs { get; set; } 
        public int FriendId { get; set; }
        public Friends Friend { get; set; }
        public int AnswerId { get; set; }
        public Answers Answers { get; set; }
    }
}
