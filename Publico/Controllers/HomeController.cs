using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Publico.Controllers {
    // В этом контроллере описываются все главные роуты приложения
    public class HomeController : Controller {
        public IActionResult Index() { return View(); }
        // Перейти к чату
        public IActionResult GoToChat() { return View(); }
        // Переход к методу регистрации
        public IActionResult GoToRegister() { return View(); }
        // Переход к методу авторизации
        public IActionResult GoToLogin() { return View(); }
        // Переход к странице успешного подтверждения почты
        public IActionResult SuccessEmail() { return View(); }
        // Переход на страницу, если забыли пароль
        public IActionResult RememberPassword() { return View(); }
    }
}
