namespace POSAndOrderSystem.Entities
{
	public class MenuItem : MainEntity
	{
		public string MenuItemName { get; set; }
		public required float Price { get; set; }
		public required int MenuTypeID { get; set; }
	}
}
