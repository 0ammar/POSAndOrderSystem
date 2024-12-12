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

			// Name
			builder.Property(x => x.Name).IsRequired().HasMaxLength(50).IsUnicode(false);
			builder.HasIndex(x => x.Name).IsUnique();

			// Password
			builder.Property(u => u.Password).IsRequired().HasMaxLength(255);

			// RoleID
			builder.Property(x => x.RoleId).IsRequired();
		}
	}
}
