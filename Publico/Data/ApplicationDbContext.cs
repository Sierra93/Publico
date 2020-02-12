using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Publico.Models;

namespace Publico.Data {
    public class ApplicationDbContext : DbContext {
        public DbSet<Register> UserRegister { get; set; }
        public DbSet<Login> UserLogin { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) {
            Database.EnsureCreated();   
        }
    }
}
