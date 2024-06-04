using trans_api.Models; // Importing the models namespace
using Microsoft.EntityFrameworkCore; // Importing Entity Framework Core

namespace trans_api.Data
{
    // Defining the application's DbContext
    public class ApplicationDbContext : DbContext
    {
        // Constructor to configure DbContext options
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // DbSet properties for Users, Transactions, and Roles tables
        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Role> Roles { get; set; }

        // Configuring the model with Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //seeding some data on my migration
            modelBuilder.Entity<Role>().HasData(
               new Role { Id = 1, Name = "Admin" },
               new Role { Id = 2, Name = "Agent" }
            );

            modelBuilder.Entity<User>().HasData(
               new User { Id = 1, Username = "aggrey", Password = "$2a$11$/R.a85vIxK9W2sr0LikHQuPAPSDLJoy/9wkXTgHdakDKYE.dTMx7u", Email = "aggreyyorris@gmail.com", RoleId = 1 }
            );

            // Configuring the relationship between User and Role
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role) // Each user has one role
                .WithMany() // Each role can have many users
                .HasForeignKey(u => u.RoleId) // Foreign key in User table
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete

            // Ensuring the Email field in User is unique
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Ensuring the Username field in User is unique
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            // Configuring the relationship between Transaction and User
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.User) // Each transaction is associated with one user
                .WithMany() // Each user can have many transactions
                .HasForeignKey(t => t.UserId) // Foreign key in Transaction table
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete

            // Ensuring the Name field in Role is unique
            modelBuilder.Entity<Role>()
                .HasIndex(r => r.Name)
                .IsUnique();

            // Setting default values for CreatedAt and UpdatedAt fields using SQL CURRENT_TIMESTAMP function
            modelBuilder.Entity<User>()
                .Property(u => u.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<User>()
                .Property(u => u.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Transaction>()
                .Property(t => t.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Transaction>()
                .Property(t => t.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Role>()
                .Property(r => r.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Role>()
                .Property(r => r.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
}