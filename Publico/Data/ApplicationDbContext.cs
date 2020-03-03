using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Publico.Models; 

namespace Publico.Data {
    public class ApplicationDbContext : DbContext {
        public DbSet<User> Users { get; set; }
        public DbSet<Info> Info { get; set; }
        public DbSet<Messages> Messages { get; set; }
        public DbSet<MessagesTranslate> MessageTranslate { get; set; } 
        public DbSet<UsersRelations> UsersRelations { get; set; } 
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<MultepleContextTable>()
            .HasKey(t => new { t.UserId });

            modelBuilder.Entity<MultepleContextTable>()
                .HasOne(sc => sc.User)
                .WithMany(s => s.MultepleContextTables);
            //.HasForeignKey(sc => sc.UserId);
            modelBuilder.Entity<MultepleContextTable>()
                .HasOne(sc => sc.Info)
                .WithMany(s => s.MultepleContextTables);
            //.HasForeignKey(sc => sc.UserId);
            modelBuilder.Entity<MultepleContextTable>()
                .HasOne(sc => sc.Message)
                .WithMany(s => s.MultepleContextTables);
            //.HasForeignKey(sc => sc.UserId);
            modelBuilder.Entity<MultepleContextTable>()
               .HasOne(sc => sc.MessageTran)
               .WithMany(s => s.MultepleContextTables);
            //.HasForeignKey(sc => sc.UserId);
            modelBuilder.Entity<MultepleContextTable>()
               .HasOne(sc => sc.UsersRel)
               .WithMany(s => s.MultepleContextTables);
            //.HasForeignKey(sc => sc.UserId);
        }
    }
}
