using Microsoft.AspNetCore.Mvc;
using System;

namespace Publico.Models {
    // Класс с методами для обработки ошибок
    public class ErrorViewModel {
        // Если модель пришла не корректная
        public static IActionResult Error() { throw new Exception("Проблемы с моделью."); }
        // Если у пользователя нет токена
        public static IActionResult ErrorToken() { throw new Exception("Пользователь не верифицирован. В доступе к токену отказано."); }
        // Если пользователя нет в БД
        public static IActionResult NotFoundUser() { throw new Exception("Пользователя не существует."); }
        // Если искомый пользователь не пришел с фронта
        public static IActionResult IsEmptyUser() { throw new Exception("Искомый пользователь не заполнен."); }
        // Если искомая почта не заполнена
        public static IActionResult IsEmptyEmail() { throw new Exception("Искомая почта не заполнена."); }
        // Если логин уже существует
        public static IActionResult LoginNotEmpty() { throw new Exception("Указанный логин уже существует."); }
        // Если почта уже существует
        public static IActionResult EmailNotEmpty() { throw new Exception("Указанный email уже существует."); }
        // Если не удалось удалить
        public static IActionResult RemoveError() { throw new Exception("Ошибка удаления."); }
        // Если не удалось изменить пароль
        public static IActionResult ErrorChangePassword() { throw new Exception("Ошибка изменения пароля"); }
    }
}
