using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using PieceOfCake.Data.Models;

namespace PieceOfCake.Data
{
    public class Context : DbContext
    {
        public Context()
            : base("PieceOfCakeDB")
        {
            //Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        /// <summary>
        ///     On Model Create configure Table properties using Fluent API
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            ConfigureRole(modelBuilder);
            ConfigureUser(modelBuilder);
        }

        /// <summary>
        ///     Table spicific configurations for Role model
        /// </summary>
        /// <param name="modelBuilder"></param>
        private void ConfigureRole(DbModelBuilder modelBuilder)
        {
            //Id is Primary Key
            modelBuilder.Entity<Role>()
                .HasKey(role => role.Id);

            //Id is Identity
            modelBuilder.Entity<Role>()
                .Property(role => role.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //Title is Required, and has a max length
            modelBuilder.Entity<Role>()
                .Property(role => role.Title)
                .IsRequired()
                .HasMaxLength(30);

            //Description has a max length
            modelBuilder.Entity<Role>()
                .Property(role => role.Description)
                .HasMaxLength(180);

            //Many to many relationship between User and Roles
            modelBuilder.Entity<Role>()
                .HasMany(role => role.Users)
                .WithMany(user => user.Roles);
        }

        /// <summary>
        ///     Table spicific configurations for User model
        /// </summary>
        /// <param name="modelBuilder"></param>
        private void ConfigureUser(DbModelBuilder modelBuilder)
        {
            //Id is Primary Key
            modelBuilder.Entity<User>()
                .HasKey(user => user.Id);

            //Id is Identity
            modelBuilder.Entity<User>()
                .Property(user => user.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //Name is Required
            modelBuilder.Entity<User>()
                .Property(user => user.Name)
                .IsRequired()
                .HasMaxLength(30);

            //UserName is Required
            modelBuilder.Entity<User>()
                .Property(user => user.Username)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute {IsUnique = true}));

            //Password is Required
            modelBuilder.Entity<User>()
                .Property(user => user.Password)
                .IsRequired();

            //Email is Required
            modelBuilder.Entity<User>()
                .Property(user => user.Email)
                .IsRequired();

            //Many to many relationship between User and Roles
            modelBuilder.Entity<User>()
                .HasMany(user => user.Roles)
                .WithMany(role => role.Users);
        }
    }
}