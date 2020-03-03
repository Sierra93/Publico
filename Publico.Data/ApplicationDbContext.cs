using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Publico.Models; 

namespace Publico.Data {
    public class ApplicationDbContext : DbContext {
        public DbSet<User> Users { get; set; }
        public DbSet<Info> Info { get; set; }
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

            //modelBuilder.Entity<MultepleContextTable>()
            //    .HasOne(sc => sc.Dialogs)
            //    .WithMany(c => c.MultepleContextTables);
            //    //.HasForeignKey(sc => sc.UserId);
            //modelBuilder.Entity<MultepleContextTable>()
            //    .HasOne(sc => sc.Friend)
            //    .WithMany(c => c.MultepleContextTables);
            ////.HasForeignKey(sc => sc.FriendId);
            //modelBuilder.Entity<MultepleContextTable>()
            //.HasOne(sc => sc.Answers)
            //.WithMany(c => c.MultepleContextTables);
            ////.HasForeignKey(sc => sc.UserId);
        }
    }
}
