using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Publico.Models;

namespace Publico.Data {
    public class ApplicationDbContext : DbContext {
        //public DbSet<Register> UserRegister { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserMessages> Messages { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) {
            Database.EnsureCreated();   
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<MultepleContextTable>()
            .HasKey(t => new { t.UserId, t.MessageId });

            modelBuilder.Entity<MultepleContextTable>()
                .HasOne(sc => sc.User)
                .WithMany(s => s.MultepleContextTables)
                .HasForeignKey(sc => sc.UserId);

            modelBuilder.Entity<MultepleContextTable>()
                .HasOne(sc => sc.Message)
                .WithMany(c => c.MultepleContextTables)
                .HasForeignKey(sc => sc.MessageId);
        }
    }
}
