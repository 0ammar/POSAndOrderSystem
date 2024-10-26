using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POSAndOrderSystem.Entities;

namespace POSAndOrderSystem.EntityConfigurations
{
	public class OrderConfiguration : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			// ID
			builder.ToTable("Orders");
			builder.HasKey(x => x.ID);
			builder.Property(x => x.ID).UseIdentityColumn().IsRequired();

			// CreationDate
			builder.Property(u => u.CreationDate).IsRequired().HasDefaultValueSql("GETDATE()");

			// IsDeleted
			builder.Property(u => u.IsActive).IsRequired().HasDefaultValue(true);

			// PaymentMethod
			builder.Property(o => o.PaymentMethodID)
				.HasDefaultValue(8)
				.IsRequired();

			// PaymentStatus
			builder.Property(o => o.PaymentStatusID)
				.HasDefaultValue(12)
				.IsRequired();

			// PickUpType
			builder.Property(o => o.PickUpTypeID)
				.HasDefaultValue(14)
				.IsRequired();

			// OrdereDate
			builder.Property(o => o.OrderDate)
				.IsRequired()
				.HasDefaultValueSql("GETDATE()");

			// PickupTime
			builder.Property(o => o.PickupTime)
				.IsRequired(false);

			// EstimatedDeliveryTime
			builder.Property(o => o.EstimatedDeliveryTime)
				.IsRequired();

			// TotalAmount
			builder.
				Property(o => o.TotalAmount)
				.IsRequired()
				.HasDefaultValue(0);

			// OrderNotes
			builder.Property(o => o.OrderNotes)
				.IsRequired(false)
				.HasMaxLength(200)
				.IsUnicode();

			// UserDetails 
			builder.Property(o => o.UserFirstName).IsRequired().HasMaxLength(100);
			builder.Property(o => o.UserLastName).IsRequired().HasMaxLength(100);
			builder.Property(o => o.UserAddress).IsRequired().HasMaxLength(255);

			//RelationShips
			builder.HasMany<OrderItem>().WithOne().HasForeignKey(x => x.OrderID);
		}
	}
}
