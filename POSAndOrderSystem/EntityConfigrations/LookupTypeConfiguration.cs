using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POSAndOrderSystem.Entities;

namespace POSAndOrderSystem.EntityConfigrations
{
	public class LookupTypeConfiguration : IEntityTypeConfiguration<LookupType>
	{
		public void Configure(EntityTypeBuilder<LookupType> builder)
		{
			// ID
			builder.ToTable("LookupTypes");
			builder.HasKey(x => x.ID);
			builder.Property(x => x.ID).IsRequired();

			// CreationDate
			builder.Property(u => u.CreationDate).IsRequired().HasDefaultValueSql("GETDATE()");

			// Check 
			builder.ToTable(x => x.HasCheckConstraint("CH_Name_Length", "Len(Name) >= 3"));

			//RelationShips
			builder.HasMany<LookupItem>().WithOne().HasForeignKey(x => x.LookupTypeId);
		}
	}
}
