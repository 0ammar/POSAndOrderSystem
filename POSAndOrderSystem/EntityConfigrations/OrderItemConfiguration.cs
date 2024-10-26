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

			// IsDeleted
			builder.Property(u => u.IsActive).IsRequired().HasDefaultValue(true);

			// MenuItemID
			builder.Property(o => o.MenuItemID)
				.IsRequired();

			// Quantity
			builder.Property(o => o.Quantity)
				.IsRequired()
				.HasDefaultValue(1);

			// OrderPrice
			builder.Property(o => o.Price)
			.IsRequired()
			.HasDefaultValue(0);
			builder.ToTable(x => x.HasCheckConstraint("CH_Price_Positive", "[Price] >= 0"));

			// OrderNotes
			builder.Property(o => o.OrderItemNotes)
			.IsRequired(false)
			.HasMaxLength(500);


			builder.HasOne<MenuItem>().WithMany().HasForeignKey(oi => oi.MenuItemID);
			builder.HasOne<MenuItem>().WithMany().HasForeignKey(oi => oi.MenuItemName);

		}
	}
}
