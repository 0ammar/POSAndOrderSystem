using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POSAndOrderSystem.Entities;

namespace POSAndOrderSystem.EntityConfigrations
{
	public class MenuTypeConfiguration : IEntityTypeConfiguration<MenuType>
	{
		public void Configure(EntityTypeBuilder<MenuType> builder)
		{
			// User ID
			builder.ToTable("MenuTypes");
			builder.HasKey(x => x.ID);

			// CreationDate
			builder.Property(u => u.CreationDate)
			.IsRequired()
			.HasDefaultValueSql("GETDATE()");

			// IsDeleted
			builder.Property(u => u.IsActive)
		   .IsRequired()
		   .HasDefaultValue(true);

			// MenuTypeName
			builder.Property("MenuTypeName").IsRequired().HasMaxLength(30);
			builder.HasIndex(x => x.ID).IsUnique();

			// MenuTypeImage
			builder.Property("Image").IsRequired(false).HasMaxLength(255);

			//RelationShips
			builder.HasMany<MenuItem>().WithOne().HasForeignKey(x => x.MenuTypeID);
		}
	}
}
