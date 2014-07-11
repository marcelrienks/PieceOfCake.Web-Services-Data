using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using Scrummage.DataAccess.Models;

namespace Scrummage.DataAccess
{
    public class Context : DbContext
    {
		public Context()
            : base("ScrummageDB")
		{
		    //Configuration.ProxyCreationEnabled = false;
		}

		public DbSet<Role> Roles { get; set; }
		public DbSet<Member> Members { get; set; }
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
			ConfigureMember(modelBuilder);
			ConfigureAvatar(modelBuilder);
		}

        private void ConfigureRole(DbModelBuilder modelBuilder)
        {
			//RoleId is Primary Key
			modelBuilder.Entity<Role>()
			            .HasKey(role => role.RoleId);

			//RoleId is Identity
			modelBuilder.Entity<Role>()
			            .Property(role => role.RoleId)
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

			//Many to many relationship between Member and Roles
			modelBuilder.Entity<Role>()
									.HasMany(role => role.Members)
									.WithMany(member => member.Roles);
		}

        private void ConfigureMember(DbModelBuilder modelBuilder)
        {
			//MemberId is Primary Key
			modelBuilder.Entity<Member>()
									.HasKey(member => member.MemberId);

			//MemberId is Identity
			modelBuilder.Entity<Member>()
									.Property(member => member.MemberId)
									.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			//Name is Required
			modelBuilder.Entity<Member>()
									.Property(member => member.Name)
									.IsRequired()
									.HasMaxLength(30);

			//ShortName is Required
			modelBuilder.Entity<Member>()
									.Property(member => member.ShortName)
									.IsRequired()
									.HasMaxLength(3);

			//UserName is Required
			modelBuilder.Entity<Member>()
									.Property(member => member.Username)
									.IsRequired()
									.HasMaxLength(30)
									.HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute { IsUnique = true } ));

			//Password is Required
			modelBuilder.Entity<Member>()
									.Property(member => member.Password)
									.IsRequired();

			//Email is Required
			modelBuilder.Entity<Member>()
									.Property(member => member.Email)
									.IsRequired();

			//Many to many relationship between Member and Roles
			modelBuilder.Entity<Member>()
									.HasMany(member => member.Roles)
									.WithMany(role => role.Members);

			//One to One relationship between Member and Avatar where both are required, and Member is the principle
			modelBuilder.Entity<Member>()
									.HasRequired(member => member.Avatar)
									.WithRequiredPrincipal(avatar => avatar.Member)
									.WillCascadeOnDelete(true);
		}

        private void ConfigureAvatar(DbModelBuilder modelBuilder)
        {
			//MemberId is Primary Key
			modelBuilder.Entity<Avatar>()
									.HasKey(avatar => avatar.MemberId);

			//One to One relationship between Member and Avatar where both are required, and Avatar is the dependent
			modelBuilder.Entity<Avatar>()
									.HasRequired(avatar => avatar.Member)
									.WithRequiredDependent(member => member.Avatar);
		}
	}
}
