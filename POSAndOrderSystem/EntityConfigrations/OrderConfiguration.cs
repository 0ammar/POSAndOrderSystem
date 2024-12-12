using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POSAndOrderSystem.Entities;
using POSAndOrderSystem.Enums;

namespace POSAndOrderSystem.EntityConfigurations
{
	public class OrderConfiguration : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			// ID
			builder.ToTable("Orders");
			builder.HasKey(x => x.ID);
			builder.Property(x => x.ID).IsRequired();

			// CreationDate
			builder.Property(u => u.CreationDate).IsRequired().HasDefaultValueSql("GETDATE()");

			// TotalAmount
			builder.Property(o => o.TotalAmount).IsRequired().HasDefaultValue(0);

			// OrderNotes
			builder.Property(o => o.OrderNotes).IsRequired(false).HasMaxLength(200).IsUnicode(false);

			// UserDetails 
			builder.Property(o => o.UserName).IsRequired().HasMaxLength(100);

			// UserID
			builder.Property(x => x.UserID).IsRequired();

			// OrderStatusID
			builder.Property(x => x.OrderStatusID).IsRequired().HasDefaultValue((int)OrderStatus.Pending);

			// PaymentMethod
			builder.Property(o => o.PaymentMethodID).IsRequired().HasDefaultValue((int)PaymentMethod.Cash);

			// PaymentStatus
			builder.Property(o => o.PaymentStatusID).IsRequired().HasDefaultValue((int)PaymentStatus.Unpaid);

			// PickUpType
			builder.Property(o => o.PickUpTypeID).IsRequired().HasDefaultValue((int)PickUpType.Delivery);

			//RelationShips
			builder.HasMany<OrderItem>().WithOne().HasForeignKey(x => x.OrderID);
		}
	}
}
