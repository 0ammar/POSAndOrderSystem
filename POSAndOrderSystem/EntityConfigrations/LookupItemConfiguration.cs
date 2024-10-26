using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POSAndOrderSystem.Entities;

namespace POSAndOrderSystem.EntityConfigrations
{
	public class LookupItemConfiguration : IEntityTypeConfiguration<LookupItem>
	{
		public void Configure(EntityTypeBuilder<LookupItem> builder)
		{
			builder.ToTable("LookupItems");
			//Set Primary Key 
			builder.HasKey(x => x.ID);

			// Creation date and is deleted defaults
			builder.Property(x => x.IsActive).HasDefaultValue(true);
			builder.Property(x => x.CreationDate).HasDefaultValueSql("getdate()");

			//Check 
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

