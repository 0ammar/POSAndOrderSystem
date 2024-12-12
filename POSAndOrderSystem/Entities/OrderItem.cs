namespace POSAndOrderSystem.Entities
{
	public class OrderItem : MainEntity
	{
		public required int OrderID { get; set; }
		public required int MenuItemID { get; set; }
		public string OrderItemName { get; set; }
		public required float OrderItemPrice { get; set; }
		public required float Quantity { get; set; }
		public string OrderItemNotes { get; set; }
	}
}
