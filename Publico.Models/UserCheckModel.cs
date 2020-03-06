using System;
using System.Collections.Generic;
using System.Text;

namespace Publico.Models {
    // Модель для поиска пользователя
    public class UserCheckModel {
        public int Id { get; set; }
        public string UserNameTo { get; set; }
        public int? UserIdFrom { get; set; }
        public int? UserIdTo { get; set; }
    }
}
