using Microsoft.EntityFrameworkCore;
using POSAndOrderSystem.Entities;
using POSAndOrderSystem.EntityConfigrations;
using POSAndOrderSystem.EntityMigrations;

namespace POSAndOrderSystem.DbContexts
{
	public class POSAndOrderContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<LookupType> LookupTypes { get; set; }
		public DbSet<LookupItem> LookupItems { get; set; }
		public DbSet<MenuType> MenuTypes { get; set; }
		public DbSet<MenuItem> MenuItems { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderItem> OrderItems { get; set; }

		public POSAndOrderContext(DbContextOptions options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

			modelBuilder.Entity<LookupType>().HasData(
				new LookupType { ID = 1, Name = "User Role" },
				new LookupType { ID = 2, Name = "Order Status" },
				new LookupType { ID = 3, Name = "Payment Method" },
				new LookupType { ID = 4, Name = "Payment Status" },
				new LookupType { ID = 5, Name = "Pickup Type" }
				);

			modelBuilder.Entity<LookupItem>().HasData(
				new LookupItem { LookupTypeId = 1, ID = 1, Name = "Admin", Description = "Admin" },
				new LookupItem { LookupTypeId = 1, ID = 2, Name = "Cashier", Description = "Cashier" },
				new LookupItem { LookupTypeId = 1, ID = 3, Name = "Customer", Description = "Customer" },
				new LookupItem { LookupTypeId = 1, ID = 4, Name = "Delivery", Description = "Delivery" },
				new LookupItem { LookupTypeId = 2, ID = 5, Name = "Pending", Description = "Pending" },
				new LookupItem { LookupTypeId = 2, ID = 6, Name = "In Progress", Description = "In Progress" },
				new LookupItem { LookupTypeId = 2, ID = 7, Name = "Completed", Description = "Completed" },
				new LookupItem { LookupTypeId = 2, ID = 8, Name = "Canceld", Description = "Canceld" },
				new LookupItem { LookupTypeId = 3, ID = 9, Name = "Cash", Description = "Cash" },
				new LookupItem { LookupTypeId = 3, ID = 10, Name = "Credit Card", Description = "Credit Card" },
				new LookupItem { LookupTypeId = 3, ID = 11, Name = "Online Wallet", Description = "Online Wallet" },
				new LookupItem { LookupTypeId = 4, ID = 12, Name = "Paid", Description = "Paid" },
				new LookupItem { LookupTypeId = 4, ID = 13, Name = "Unpaid", Description = "Unpaid" },
				new LookupItem { LookupTypeId = 5, ID = 14, Name = "Pickup At Restaurant", Description = "Pickup At Restaurant" },
				new LookupItem { LookupTypeId = 5, ID = 15, Name = "Deliver To Customer", Description = "Deliver To Customer" }
				);

			modelBuilder.Entity<MenuType>().HasData(
				new MenuType { ID = 1, MenuTypeName = "Shawarma" },
				new MenuType { ID = 2, MenuTypeName = "Roasted Chicken" },
				new MenuType { ID = 3, MenuTypeName = "Chicken On Charcoal" },
				new MenuType { ID = 4, MenuTypeName = "Prorated Chicken" },
				new MenuType { ID = 5, MenuTypeName = "Roasted Chicken With Rice" },
				new MenuType { ID = 6, MenuTypeName = "Chicken On Charcoal With Rice" },
				new MenuType { ID = 7, MenuTypeName = "Snacks" },
				new MenuType { ID = 8, MenuTypeName = "Appetizers" },
				new MenuType { ID = 9, MenuTypeName = "Drinks" }
				);

			modelBuilder.Entity<MenuItem>().HasData(
				new MenuItem { MenuTypeID = 1, ID = 1, MenuItemName = "Chicken Shawarma", Price = 3 },
				new MenuItem { MenuTypeID = 1, ID = 2, MenuItemName = "Beef Shawarma Plate", Price = 3 },
				new MenuItem { MenuTypeID = 1, ID = 3, MenuItemName = "Spicy Shawarma Wrap", Price = 3 },
				new MenuItem { MenuTypeID = 2, ID = 4, MenuItemName = "Half Roasted Chicken", Price = 3 },
				new MenuItem { MenuTypeID = 2, ID = 5, MenuItemName = "BBQ Glazed Roasted Chicken", Price = 3 },
				new MenuItem { MenuTypeID = 2, ID = 6, MenuItemName = "Lemon Pepper Roasted Chicken", Price = 3 },
				new MenuItem { MenuTypeID = 3, ID = 7, MenuItemName = "Charcoal-Grilled Whole Chicken", Price = 3 },
				new MenuItem { MenuTypeID = 3, ID = 8, MenuItemName = "Charcoal-Grilled Chicken Skewers", Price = 3 },
				new MenuItem { MenuTypeID = 3, ID = 9, MenuItemName = "Spicy Peri-Peri Charcoal Chicken", Price = 3 },
				new MenuItem { MenuTypeID = 4, ID = 10, MenuItemName = "Chicken Thighs with Garlic Butter", Price = 3 },
				new MenuItem { MenuTypeID = 4, ID = 11, MenuItemName = "Boneless Chicken Breast Fillets", Price = 3 },
				new MenuItem { MenuTypeID = 4, ID = 12, MenuItemName = "Drumsticks in Spicy Marinade", Price = 3 },
				new MenuItem { MenuTypeID = 5, ID = 13, MenuItemName = "Herb Roasted Chicken with Basmati Rice", Price = 3 },
				new MenuItem { MenuTypeID = 5, ID = 14, MenuItemName = "Roasted Chicken with Pilaf and Saffron", Price = 3 },
				new MenuItem { MenuTypeID = 5, ID = 15, MenuItemName = "BBQ Roasted Chicken over Brown Rice", Price = 3 },
				new MenuItem { MenuTypeID = 6, ID = 16, MenuItemName = "Charcoal Chicken with Turmeric Rice", Price = 3 },
				new MenuItem { MenuTypeID = 6, ID = 17, MenuItemName = "Grilled Chicken with Vegetable Rice", Price = 3 },
				new MenuItem { MenuTypeID = 6, ID = 18, MenuItemName = "Smoky Charcoal Chicken with Fried Rice", Price = 3 },
				new MenuItem { MenuTypeID = 7, ID = 19, MenuItemName = "French Fries with Dipping Sauces", Price = 3 },
				new MenuItem { MenuTypeID = 7, ID = 20, MenuItemName = "Mozzarella Sticks with Marinara", Price = 3 },
				new MenuItem { MenuTypeID = 7, ID = 21, MenuItemName = "Chicken Nuggets with Honey Mustard", Price = 3 },
				new MenuItem { MenuTypeID = 8, ID = 22, MenuItemName = "Hummus with Pita Bread", Price = 3 },
				new MenuItem { MenuTypeID = 8, ID = 23, MenuItemName = "Stuffed Grape Leaves", Price = 3 },
				new MenuItem { MenuTypeID = 8, ID = 24, MenuItemName = "Garlic Bread with Cheese", Price = 3 },
				new MenuItem { MenuTypeID = 9, ID = 25, MenuItemName = "Fresh Lemon Mint Cooler", Price = 3 },
				new MenuItem { MenuTypeID = 9, ID = 26, MenuItemName = "Iced Hibiscus Tea", Price = 3 },
				new MenuItem { MenuTypeID = 9, ID = 27, MenuItemName = "Sparkling Berry Lemonade", Price = 3 }
				);

			modelBuilder.Entity<User>().HasData(
				new User
				{
					ID = 1,
					FirstName = "New",
					LastName = "User",
					Email = "0ammararab0@gmail.com",
					Password = "AAAzzz111!",
					Phone = "0788482930",
					Address = "Jordan",
					UserImage = "qazwsxedcrfvtgbtgbyhnujm",
					RoleId = 1,
				});

			modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
			modelBuilder.ApplyConfiguration(new LookupTypeConfiguration());
			modelBuilder.ApplyConfiguration(new LookupItemConfiguration());
			modelBuilder.ApplyConfiguration(new MenuTypeConfiguration());
			modelBuilder.ApplyConfiguration(new MenuItemConfiguration());
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfiguration(new UserEntityConfigration());
		}
	}
}
