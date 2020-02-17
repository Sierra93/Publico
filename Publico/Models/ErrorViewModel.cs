using Microsoft.AspNetCore.Mvc;
using System;

namespace Publico.Models {
    public class ErrorViewModel {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        public static IActionResult Error() {
            throw new Exception("Проблемы с моделью");
        }
    }
}
