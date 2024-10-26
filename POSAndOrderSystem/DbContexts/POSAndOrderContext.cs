using Microsoft.EntityFrameworkCore;
using POSAndOrderSystem.Entities;
using POSAndOrderSystem.EntityMigrations;

namespace POSAndOrderSystem.DbContexts
{
	public class POSAndOrderContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public POSAndOrderContext(DbContextOptions options) : base(options)
		{
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfiguration(new UserEntityConfigration());
		}
	}
}
