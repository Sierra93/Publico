using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Publico.Controllers {
    public class HomeController : Controller {
        public IActionResult Index() { return View(); }
        // Перейти к чату
        public IActionResult GoToChat() { return View(); }
        // Переход к методу регистрации
        public IActionResult GoToRegister() { return View(); }
        // Переход к методу авторизации
        public IActionResult GoToLogin() { return View(); }
    }
}
