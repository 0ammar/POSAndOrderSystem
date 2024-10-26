namespace POSAndOrderSystem.Entities
{
	public class OrderItem : MainEntity
	{
		public int OrderID { get; set; }
		public int MenuItemID { get; set; }
		public float Quantity { get; set; }
		public float Price { get; set; }
		public string? OrderItemNotes { get; set; }
		public required string MenuItemName { get; set; }
		public string Notes { get; internal set; }
	}
}
