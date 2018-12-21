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
        //public ABCTestDBConext() : base()
        // {
        //}

        
        public ABCTestDBConext(DbContextOptions<ABCTestDBConext> options) : base(options)
        {
            //Database.SetInitializer(new SchoolDBInitializer());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)  
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasIndex(b => b.Login)
                .IsUnique()
                .HasName("UniqueKey_Login");

            modelBuilder.Entity<User>()
                .HasIndex(b => b.EMail)
                .IsUnique()
                .HasName("UniqueKey_EMail");

            modelBuilder.Entity<User>()
                .HasMany(e => e.Messages)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(e => e.AuthorMessages)
                .WithOne(e => e.Author)
                .HasForeignKey(e => e.AuthorID)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder
            //    .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ABCSolutionsTest;Trusted_Connection=True;", providerOptions => providerOptions.CommandTimeout(60))
            //    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            base.OnConfiguring(optionsBuilder);

        }
    }
}
