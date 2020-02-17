using Microsoft.AspNetCore.Mvc;
using System;

namespace Publico.Models {
    // Класс с методами для обработки ошибок
    public class ErrorViewModel {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
