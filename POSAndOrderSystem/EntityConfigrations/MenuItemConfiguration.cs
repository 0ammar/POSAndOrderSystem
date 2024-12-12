using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POSAndOrderSystem.Entities;

namespace POSAndOrderSystem.EntityConfigrations
{
	public class MenuItemConfiguration : IEntityTypeConfiguration<MenuItem>
	{
		public void Configure(EntityTypeBuilder<MenuItem> builder)
		{
			// ID
			builder.ToTable("MenuItems");
			builder.HasKey(x => x.ID);
			builder.Property(x => x.ID).IsRequired();

			// CreationDate
			builder.Property(u => u.CreationDate).IsRequired().HasDefaultValueSql("GETDATE()");

			// MenuITemName
			builder.Property(mi => mi.MenuItemName).IsRequired().HasMaxLength(100);
			builder.HasIndex(x => x.ID).IsUnique();
			builder.ToTable(x => x.HasCheckConstraint("CH-MenuItemName_Length", "LEN( MenuItemName ) >= 3"));

			// MenuItemPrice
			builder.Property(mi => mi.Price).IsRequired().HasDefaultValue(0);
			builder.ToTable(x => x.HasCheckConstraint("CH_Price_Positive", "[Price] >= 0"));

			// MenuTypeID
			builder.Property(x => x.MenuTypeID).IsRequired();
		}
	}
}
