using System.ComponentModel.DataAnnotations.Schema;
using PieceOfCake.Data.Models;

namespace PieceOfCake.Data
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

        /// <summary>
        ///     Table spicific configurations for Avatar model
        /// </summary>
        /// <param name="modelBuilder"></param>
        private void ConfigureAvatar(DbModelBuilder modelBuilder)
        {
            //Id is Primary Key
            modelBuilder.Entity<Avatar>()
                .HasKey(avatar => avatar.Id);

            //Image is Required
            modelBuilder.Entity<Avatar>()
                .Property(avatar => avatar.Image)
                .IsRequired();

            //One to One relationship between User and Avatar where both are required, and Avatar is the dependent
            modelBuilder.Entity<Avatar>()
                .HasRequired(avatar => avatar.User)
                .WithRequiredDependent(user => user.Avatar);
        }
    }
}