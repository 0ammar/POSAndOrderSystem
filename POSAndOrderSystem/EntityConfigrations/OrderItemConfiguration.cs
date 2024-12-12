using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POSAndOrderSystem.Entities;

namespace POSAndOrderSystem.EntityConfigurations
{
	public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
	{
		public void Configure(EntityTypeBuilder<OrderItem> builder)
		{
			// ID
			builder.ToTable("Orders");
			builder.HasKey(x => x.ID);
			builder.Property(x => x.ID).UseIdentityColumn().IsRequired();

			// CreationDate
			builder.Property(u => u.CreationDate).IsRequired().HasDefaultValueSql("GETDATE()");

			// MenuItemID
			builder.Property(o => o.OrderID).IsRequired();

			// MenuItemID
			builder.Property(o => o.MenuItemID).IsRequired();

			// OrderItemName
			builder.Property(mi => mi.OrderItemName).IsRequired().HasMaxLength(100);
			builder.HasIndex(x => x.ID).IsUnique();
			builder.ToTable(x => x.HasCheckConstraint("CH-MenuItemName_Length", "LEN( MenuItemName ) >= 3"));

			// MenuItemPrice
			builder.Property(o => o.OrderItemPrice).IsRequired().HasDefaultValue(0);
			builder.ToTable(x => x.HasCheckConstraint("CH_Price_Positive", "[Price] >= 0"));

			// Quantity
			builder.Property(o => o.Quantity).IsRequired().HasDefaultValue(1);

			// OrderNotes
			builder.Property(o => o.OrderItemNotes).IsRequired(false).HasMaxLength(500);

			// Relationships
			builder.HasOne<MenuItem>().WithMany().HasForeignKey(oi => oi.MenuItemID);
			builder.HasOne<MenuItem>().WithMany().HasForeignKey(oi => oi.OrderItemName);
			builder.HasOne<MenuItem>().WithMany().HasForeignKey(oi => oi.OrderItemPrice);
		}
	}
}
