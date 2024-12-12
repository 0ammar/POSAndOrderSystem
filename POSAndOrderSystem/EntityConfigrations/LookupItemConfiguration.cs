using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POSAndOrderSystem.Entities;

namespace POSAndOrderSystem.EntityConfigrations
{
	public class LookupItemConfiguration : IEntityTypeConfiguration<LookupItem>
	{
		public void Configure(EntityTypeBuilder<LookupItem> builder)
		{
			// ID
			builder.ToTable("LookupItems");
			builder.HasKey(x => x.ID);
			builder.Property(x => x.ID).IsRequired();

			// CreationDate
			builder.Property(u => u.CreationDate).IsRequired().HasDefaultValueSql("GETDATE()");

			// Check 
			builder.ToTable(x => x.HasCheckConstraint("CH_Name_Length", "Len(Name) >= 3"));

			//RelationShips
			builder.HasMany<User>().WithOne().HasForeignKey(x => x.RoleId).OnDelete(DeleteBehavior.NoAction);
			builder.HasMany<Order>().WithOne().HasForeignKey(x => x.OrderStatusID).OnDelete(DeleteBehavior.NoAction);
			builder.HasMany<Order>().WithOne().HasForeignKey(x => x.PaymentMethodID).OnDelete(DeleteBehavior.NoAction);
			builder.HasMany<Order>().WithOne().HasForeignKey(x => x.PaymentStatusID).OnDelete(DeleteBehavior.NoAction);
			builder.HasMany<Order>().WithOne().HasForeignKey(x => x.PickUpTypeID).OnDelete(DeleteBehavior.NoAction);
		}
	}
}

