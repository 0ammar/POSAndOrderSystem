using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POSAndOrderSystem.Entities;

namespace POSAndOrderSystem.EntityConfigrations
{
	public class LookupTypeConfiguration : IEntityTypeConfiguration<LookupType>
	{
		public void Configure(EntityTypeBuilder<LookupType> builder)
		{
			builder.ToTable("LookupTypes");
			//Set Primary Key 
			builder.HasKey(x => x.ID);

			// Creation date and is deleted defaults
			builder.Property(x => x.IsActive).HasDefaultValue(true);
			builder.Property(x => x.CreationDate).HasDefaultValueSql("getdate()");

			//Check 
			builder.ToTable(x => x.HasCheckConstraint("CH_Name_Length", "Len(Name) >= 3"));

			//RelationShips
			builder.HasMany<LookupItem>().WithOne().HasForeignKey(x => x.LookupTypeId);
		}
	}
}
