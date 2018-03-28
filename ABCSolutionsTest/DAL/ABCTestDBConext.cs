using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABCSolutionsTest.Models;
using Microsoft.EntityFrameworkCore;

namespace ABCSolutionsTest.DAL
{
    public class ABCTestDBConext : DbContext
    {
        public ABCTestDBConext() : base()
        {
        }

        //public ABCTestDBConext(DbContextOptions<ABCTestDBConext> options) : base(options)
        //{
        //}

        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)  
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                        .HasAlternateKey(c => c.Login)
                        .HasName("AlternateKey_Login");

            modelBuilder.Entity<User>()
                        .HasAlternateKey(c => c.EMail)
                        .HasName("AlternateKey_EMail");

            modelBuilder.Entity<User>()
                .HasMany(e => e.Messages)
                .WithOne(e => e.User)
                .OnDelete(DeleteBehavior.Cascade);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ABCSolutionsTest;Trusted_Connection=True;", providerOptions => providerOptions.CommandTimeout(60))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            base.OnConfiguring(optionsBuilder);

        }
    }
}
