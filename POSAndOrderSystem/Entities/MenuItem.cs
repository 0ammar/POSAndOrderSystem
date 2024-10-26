namespace POSAndOrderSystem.Entities
{
	public class MenuItem : MainEntity
	{
		public required string MenuItemName { get; set; }
		public required float Price { get; set; }
		public int MenuTypeID { get; set; }
	}
}
