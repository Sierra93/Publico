using System;
using System.Collections.Generic;
using System.Text;

namespace Publico.Models {
    // Модель для удаления пользователя или друга
    public class UserDelete {
        public int Id { get; set; }
        public int? UserFrom { get; set; }
        public string UserTo { get; set; }
    }
}
