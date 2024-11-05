using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POSAndOrderSystem.Entities;

namespace POSAndOrderSystem.EntityMigrations
{
	public class UserEntityConfigration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			// User ID
			builder.ToTable("Users");
			builder.HasKey(x => x.ID);
			builder.Property(x => x.ID).UseIdentityColumn().IsRequired();

			// FisrtName, LastName
			builder.Property(x => x.FirstName).IsRequired().HasMaxLength(50);
			builder.Property(x => x.LastName).IsRequired().HasMaxLength(50);

			// Email
			builder.Property(x => x.Email).IsRequired().IsUnicode().HasMaxLength(30);
			builder.HasIndex(x => x.Email).IsUnique();

			// Phone
			builder.Property(u => u.Phone).IsRequired().HasMaxLength(15);
			builder.HasIndex(x => x.Phone).IsUnique();

			// Password
			builder.Property(u => u.Password).IsRequired().HasMaxLength(30);

			// Image
			builder.Property(u => u.UserImage)
			.HasMaxLength(255)
			.IsRequired(false);

			// CreationDate
			builder.Property(u => u.CreationDate)
			.IsRequired()
			.HasDefaultValueSql("GETDATE()");

			// IsDeleted
			builder.Property(u => u.IsActive)
		   .IsRequired()
		   .HasDefaultValue();
		}
	}
}
