using Microsoft.EntityFrameworkCore;
using CustomerEngagementPlatform.Api.src;
using CustomerEngagementPlatform.Api.src.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace CustomerEngagementPlatform.Api.src.Data
{
    public class CustomerEngagementContext(DbContextOptions<CustomerEngagementContext> options) : DbContext(options)
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the primary key for each entity
            modelBuilder.Entity<Customer>()
                .HasKey(c => c.CustomerId);
            modelBuilder.Entity<Activity>()
                .HasKey(a => a.ActivityId);
            modelBuilder.Entity<Message>()
                .HasKey(m => m.MessageId);
            modelBuilder.Entity<Notification>()
                .HasKey(n => n.NotificationId);

            // Configure the relationships
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Activities)
                .WithOne(a => a.Customer)
                .HasForeignKey(a => a.CustomerId);

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Messages)
                .WithOne(m => m.Customer)
                .HasForeignKey(m => m.CustomerId);

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Notifications)
                .WithOne(n => n.Customer)
                .HasForeignKey(n => n.CustomerId);

            // Additional configurations can be added here
        }
    }
}

