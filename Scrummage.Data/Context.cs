using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using Scrummage.Data.Models;

namespace Scrummage.Data
{
    public class Context : DbContext
    {
        public Context()
            : base("ScrummageDB")
        {
            //Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Avatar> Avatars { get; set; }

        //NOTE:
        //Using Fluent API to configure tables in an attempt to keep poco model classes clean and un cluttered.

        /// <summary>
        ///     On Model Create configure Table properties using Fluent API
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            ConfigureRole(modelBuilder);
            ConfigureUser(modelBuilder);
            ConfigureAvatar(modelBuilder);
        }

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
                .Property(role => role.Title)
                .HasMaxLength(180);

            //Many to many relationship between User and Roles
            modelBuilder.Entity<Role>()
                .HasMany(role => role.Users)
                .WithMany(user => user.Roles);
        }

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

            //ShortName is Required
            modelBuilder.Entity<User>()
                .Property(user => user.ShortName)
                .IsRequired()
                .HasMaxLength(3);

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

            //One to One relationship between User and Avatar where both are required, and Member is the principle
            modelBuilder.Entity<User>()
                .HasRequired(user => user.Avatar)
                .WithRequiredPrincipal(avatar => avatar.User)
                .WillCascadeOnDelete(true);
        }

        private void ConfigureAvatar(DbModelBuilder modelBuilder)
        {
            //Id is Primary Key
            modelBuilder.Entity<Avatar>()
                .HasKey(avatar => avatar.Id);

            //One to One relationship between User and Avatar where both are required, and Avatar is the dependent
            modelBuilder.Entity<Avatar>()
                .HasRequired(avatar => avatar.User)
                .WithRequiredDependent(user => user.Avatar);
        }
    }
}