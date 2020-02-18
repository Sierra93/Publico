using Microsoft.AspNetCore.Mvc;
using System;

namespace Publico.Models {
    // Класс с методами для обработки ошибок
    public class ErrorViewModel {
        public static IActionResult Error() { throw new Exception("Проблемы с моделью."); }
        public static IActionResult ErrorToken() { throw new Exception("Пользователь не верифицирован. В доступе к токену отказано."); }
    }
}
