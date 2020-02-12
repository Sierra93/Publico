using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Publico.Models;
using Microsoft.EntityFrameworkCore;
using Publico.Data;

namespace Publico.Controllers {
    // Авторизация и регистрация юзеров
    [ApiController, Route("api/odata/auth")]
    public class AuthController : ControllerBase {
        private ApplicationDbContext _context;
        public AuthController(ApplicationDbContext context) {
            _context = context;
        }
    }
}