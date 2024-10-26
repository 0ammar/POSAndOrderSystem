using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POSAndOrderSystem.Entities;

namespace POSAndOrderSystem.EntityMigrations
{
	public class UserEntityConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			// User ID
			builder.ToTable("Users");
			builder.HasKey(x => x.ID);
			builder.Property(x => x.ID).UseIdentityColumn().IsRequired();

			// CreationDate
			builder.Property(u => u.CreationDate).IsRequired().HasDefaultValueSql("GETDATE()");

			// IsDeleted
			builder.Property(u => u.IsActive).IsRequired().HasDefaultValue(true);

			// FisrtName, LastName
			builder.Property(x => x.FirstName).IsRequired().HasMaxLength(50).IsUnicode(false);
			builder.Property(x => x.LastName).IsRequired().HasMaxLength(50).IsUnicode(false);

			// Email
			builder.Property(x => x.Email).IsRequired().IsUnicode().HasMaxLength(250);
			builder.HasIndex(x => x.Email).IsUnique();

			// Password
			builder.Property(u => u.Password).IsRequired().HasMaxLength(250);

			// Phone
			builder.Property(u => u.Phone).IsRequired(false).HasMaxLength(250);
			builder.HasIndex(x => x.Phone).IsUnique();

			// Image
			builder.Property(u => u.UserImage).IsUnicode(false).IsRequired(false).HasMaxLength(255);

			//Address
			builder.Property(u => u.Address).IsRequired(false).HasMaxLength(255);
		}
	}
}
